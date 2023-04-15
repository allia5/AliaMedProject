using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.UserManager;
using Server.Models.fileMedical;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
using static Server.Services.Foundation.FileMedicalService.FileMedicalMapperService;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.FileChronicDiseasesManager;

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
        public FileMedicalService(IFileChronicDiseasesManager fileChronicDiseasesManager,IChronicDiseasesManager chronicDiseasesManager,UserManager<User> _UserManager, IUserManager userManager, IDoctorManager doctorManager, IFileMedicalManager fileMedicalManager, IPlanningAppoimentManager planningAppoimentManager)
        {
            this.fileChronicDiseasesManager= fileChronicDiseasesManager;
            this.chronicDiseasesManager= chronicDiseasesManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this.fileMedicalManager = fileMedicalManager;
            this.planningAppoimentManager = planningAppoimentManager;
            this._UserManager = _UserManager;
        }
        public async Task<FileMedicalPatientDto> AddNewFileMedicalPatient(string Email, FileMedicalToAddDto fileMedicalToAdd) =>
            await TryCatch(async () =>
            {
                ValidateEntryOnAddFileMedical(Email, fileMedicalToAdd);
                var UserAccountDoctor =await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid( fileMedicalToAdd.IdAppointment));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment, Doctor);
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
               return MapperToFileMedicalPatient(UserAccountDoctor,newFileMedical,fileMedicalToAdd);


            });
       
    }
}
