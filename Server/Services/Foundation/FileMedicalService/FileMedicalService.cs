using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.UserManager;
using Server.Models.fileMedical;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
using static Server.Services.Foundation.PlanningAppoimentService.PlanningAppoimentMapperService;
using static Server.Services.Foundation.ChronicDiseasesService.ChronicDiseasesMapperService;
using static Server.Services.Foundation.FileMedicalService.FileMedicalMapperService;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.PrescriptionManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.AnalyseManager;
using Server.Models.SpecialtieDoctor;

namespace Server.Services.Foundation.FileMedicalService
{
    public partial class FileMedicalService : IFileMedicalService
    {
        public readonly UserManager<User> _UserManager;
        public readonly IChronicDiseasesManager chronicDiseasesManager;
        public readonly IFileChronicDiseasesManager fileChronicDiseasesManager;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly IFileMedicalManager fileMedicalManager ;
        public readonly IPlanningAppoimentManager planningAppoimentManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IPrescriptionManager prescriptionManager;
        public readonly IRadioManager radioManager;
        public readonly IAnalyseManager analyseManager;
        
        public FileMedicalService(IPrescriptionManager prescriptionManager,IRadioManager radioManager,IAnalyseManager analyseManager,ICabinetMedicalManager cabinetMedicalManager, ISpecialitiesManager specialitiesManager, IOrdreMedicalManager ordreMedicalManager, IWorkDoctorManager workDoctorManager, IFileChronicDiseasesManager fileChronicDiseasesManager,IChronicDiseasesManager chronicDiseasesManager,UserManager<User> _UserManager, IUserManager userManager, IDoctorManager doctorManager, IFileMedicalManager fileMedicalManager, IPlanningAppoimentManager planningAppoimentManager)
        {
            this.prescriptionManager = prescriptionManager;
            this.radioManager = radioManager;
            this.analyseManager = analyseManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.ordreMedicalManager=ordreMedicalManager;
            this.specialitiesManager = specialitiesManager;
            this.workDoctorManager = workDoctorManager;
            this.fileChronicDiseasesManager= fileChronicDiseasesManager;
            this.chronicDiseasesManager= chronicDiseasesManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this.fileMedicalManager = fileMedicalManager;
            this.planningAppoimentManager = planningAppoimentManager;
            this._UserManager = _UserManager;
        }

        public async Task<FileMedicalMainPatientDto> GetAllFileMedicalMainPatient(string Email, string IdAppointment) =>
            await TryCatch(async () =>
            {
                List<FileMedicalPatientDto> fileMedicalPatientDtos = new List<FileMedicalPatientDto>();
                List<chronicDiseasesDto> chronicDiseasesDtos = new List<chronicDiseasesDto>();
                
                ValidateEntryOnGetFilePatient(Email, IdAppointment);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid(IdAppointment));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment,Doctor);
                var WorkDoctorActive = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Appointment.IdDoctor,Appointment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctorActive);
                var UserAccountPatient = await this._UserManager.FindByIdAsync(Appointment.IdUser);
                ValidateUserIsNull(UserAccountPatient);
                var ListFilesMedicalPatient = await this.fileMedicalManager.SelectFilesMedicalByIdUser(UserAccountPatient.Id);
                foreach (var file in ListFilesMedicalPatient)
                {
                    var DoctorAccountCreatedFile = await this.userManager.SelectUserByIdDoctor(file.IdDoctor);
                    var specialitiesDoctor = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(file.IdDoctor);
                        var ListOrdreMedicalFileMeicalPatient = await this.ordreMedicalManager.SelectListOrdreMedicalByIdMedicalFile(file.Id);
                        var ListChronicDiseases = await this.chronicDiseasesManager.SelectChronicDiseasesByIdMedicalFileAsync(file.Id);
                        foreach (var ItemChronicDeases in ListChronicDiseases)
                        {
                            var ChronicDiseases = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(ItemChronicDeases.IdChronicDisease);
                             if(ChronicDiseases != null)
                             {
                                 var result = MapperTochronicDiseasesDto(ChronicDiseases);
                                 chronicDiseasesDtos.Add(result);
                             }
                        }
                if(DoctorAccountCreatedFile != null && chronicDiseasesDtos != null && specialitiesDoctor != null)
                    {
                        var resultMappingFileMedicalPatientDto = MapperTofileMedicalPatientDtos(ListOrdreMedicalFileMeicalPatient.Count(), chronicDiseasesDtos, DoctorAccountCreatedFile, file, specialitiesDoctor);
                        fileMedicalPatientDtos.Add(resultMappingFileMedicalPatientDto);
                    }
                        
                        chronicDiseasesDtos = new List<chronicDiseasesDto>();
                       




                }
                
                var ResultMappingUserInformationDto = MppperToPatientInformationDto(UserAccountPatient);
                return MapperToFileMedicalMainPatientDto(fileMedicalPatientDtos, ResultMappingUserInformationDto);


            });
        

        public async Task<FileMedicalPatientDto> AddNewFileMedicalPatient(string Email, FileMedicalToAddDto fileMedicalToAdd) =>
            await TryCatch(async () =>
            {
                ValidateEntryOnAddFileMedical(Email, fileMedicalToAdd);
                var UserAccountDoctor =await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var SpecialitiesDoctor = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(Doctor.Id);
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid( fileMedicalToAdd.IdAppointment));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment, Doctor);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(Appointment.IdCabinet);
                ValidateCabinetMedicalIsNull(Cabinet);
                var WorkDoctorActive = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Appointment.IdDoctor,Appointment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctorActive);
                var newFileMedical =MapperToFileMedical(fileMedicalToAdd, Appointment);
                await this.fileMedicalManager.InsertFileMedical(newFileMedical);
                foreach (var maladie in fileMedicalToAdd.chronicDiseases)
                {
                    var chroniqueDisease = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(maladie.id);
                    if(chroniqueDisease != null)
                    {
                        await this.fileChronicDiseasesManager.insertFileChronicDisease(new Models.FileChronicDisease.FileChronicDiseases { Id = Guid.NewGuid(), IdFile = newFileMedical.Id, IdChronicDisease = chroniqueDisease.Id });
                    }
                }
                
               return MapperToFileMedicalPatient(UserAccountDoctor,newFileMedical,fileMedicalToAdd,SpecialitiesDoctor);


            });

        public async Task UpdateFileMedicalService(string Email,UpdateFileMedicalDto UpdateFileMedical) =>
            await TryCatch(async () =>
            {
                ValidateEntryOnUpdateFileMedical(Email, UpdateFileMedical);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var Appoiment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid( UpdateFileMedical.AppointmentId));
                ValidatePlanningIsNull(Appoiment);
                ValidateAppointmentWithDoctor(Appoiment, Doctor);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(Appoiment.IdCabinet);
                ValidateCabinetMedicalIsNull(Cabinet);
                var WorkDoctorActive = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Appoiment.IdDoctor,Appoiment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctorActive);
                var FileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(DecryptGuid(UpdateFileMedical.FileId));
                validateeFileMedicalIsNull(FileMedical);
                await this.fileChronicDiseasesManager.DeleteFileFileChronicDiseaseByFileId(FileMedical.Id);
                foreach (var item in UpdateFileMedical.ChronicDiseases)
                {
                    var ChronicDiseases = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(item.id);
                    if(ChronicDiseases != null)
                    {
                        await this.fileChronicDiseasesManager.insertFileChronicDisease(new Models.FileChronicDisease.FileChronicDiseases { Id=Guid.NewGuid() , IdFile = FileMedical.Id ,IdChronicDisease=ChronicDiseases.Id});
                    }
                }
                var newIdentifieMedecal = GenerateID(UpdateFileMedical.FirstName, UpdateFileMedical.LastName, UpdateFileMedical.DateOfBirth);
                var newFile = MapperToFileMedicalUpdated(FileMedical, UpdateFileMedical);
                await this.fileMedicalManager.UpdateFileMedicalAsync(newFile);

            });



    
       



        public async Task<List<FileMedicalPatientDto>> GetFilesMedicalPatient(string Email) =>
            await TryCatch_(async () =>
            {
                List<FileMedicalPatientDto> fileMedicalPatientDtos = new List<FileMedicalPatientDto>();
                List<chronicDiseasesDto> chronicDiseasesDtos = new List<chronicDiseasesDto>();
                var UserAccount = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccount);
                var FilesUserAccount = await this.fileMedicalManager.SelectFilesMedicalByIdUser(UserAccount.Id);
                foreach (var File in FilesUserAccount)
                {
                    var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(File.IdDoctor);
                    if(UserAccountDoctor != null)
                    {
                        var Specialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(File.IdDoctor);
                       // var DoctorInformation = MapperToDoctorInformationDto(Specialities, UserAccountDoctor);
                        var ListOrdreMedicalFileMeicalPatient = await this.ordreMedicalManager.SelectListOrdreMedicalByIdMedicalFile(File.Id);
                        var ChronicDeasesFileMedical = await this.chronicDiseasesManager.SelectChronicDiseasesByIdMedicalFileAsync(File.Id);
                        foreach(var ItemChronicFile in ChronicDeasesFileMedical)
                        {
                            var ChrocinDisease = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(ItemChronicFile.IdChronicDisease);
                            if(ChrocinDisease != null)
                            {
                              var  chronicDiseasesDto = MapperTochronicDiseasesDto(ChrocinDisease);
                                chronicDiseasesDtos.Add(chronicDiseasesDto);
                            }
                        }
                        if (UserAccountDoctor != null && chronicDiseasesDtos != null && Specialities != null)
                        {
                            var resultMappingFileMedicalPatientDto = MapperTofileMedicalPatientDtos(ListOrdreMedicalFileMeicalPatient.Count(), chronicDiseasesDtos, UserAccountDoctor, File, Specialities);
                            fileMedicalPatientDtos.Add(resultMappingFileMedicalPatientDto);
                        }
                        chronicDiseasesDtos = new List<chronicDiseasesDto>();
                    }
                }
                return fileMedicalPatientDtos;
            });
        
    }
}
