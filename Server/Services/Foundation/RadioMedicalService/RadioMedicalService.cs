using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.AnalyseManager;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.PrescriptionLineManager;
using Server.Managers.Storages.PrescriptionManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.SecretaryManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using Server.Services.Foundation.FileMedicalService;
using static Server.Utility.Utility;
using static Server.Services.Foundation.PlanningAppoimentService.PlanningAppoimentMapperService;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;
using static Server.Services.Foundation.RadioMedicalService.RadioMedicalMapperService;

namespace Server.Services.Foundation.RadioMedicalService
{
    public partial class RadioMedicalService : IRadioMedicalService
    {
        public readonly IFileMedicalManager FileMedicalManager;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly UserManager<User> _UserManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly IRadioManager radioManager;
        public readonly IFileChronicDiseasesManager fileChronicDiseasesManager;
        public readonly IChronicDiseasesManager chronicDiseasesManager;
        public readonly ISpecialitiesManager specialitiesManager;
        
        public RadioMedicalService(ISpecialitiesManager specialitiesManager,IFileChronicDiseasesManager fileChronicDiseasesManager,IChronicDiseasesManager chronicDiseasesManager,UserManager<User> _UserManager, IFileMedicalManager FileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, IPlanningAppoimentManager planningAppoimentManager, IOrdreMedicalManager ordreMedicalManager, IRadioManager radioManager)
        {
            this.specialitiesManager = specialitiesManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this.ordreMedicalManager=ordreMedicalManager;
            this._UserManager = _UserManager;
            this.FileMedicalManager = FileMedicalManager;
            this.radioManager = radioManager;
            this.chronicDiseasesManager = chronicDiseasesManager;
            this.fileChronicDiseasesManager= fileChronicDiseasesManager;

        }
        public async Task<InformationRadioResultDto> GetInformationRadioMedicalResult(string Email, string CodeQr) =>
            await TryCatch(async () =>
            {
                List<string> ListChronicDeasses = new List<string>();
                List<string> ListSpecialitiesDoctor = new List<string>();

                ValidateEntryOnGetRadioInformation(Email, CodeQr);
                var UserAcccountRadiology = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAcccountRadiology);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAcccountRadiology.Id);
                ValidationDoctorIsNull(Doctor);
                var Radio = await this.radioManager.SelectRadioByCodeAsync(DecryptString( CodeQr, "AJFNJjfjJZFJNdzj=="));
                ValidateRadioIsNull(Radio);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Radio.IdOrdreMedical);
                ValidateOrdreMedicalIsNull(OrdreMedical);
                var FileMedical = await this.FileMedicalManager.SelectFileMedicalByIdAsync(OrdreMedical.IdFileMedical);
                ValidateFileMedicalIsNull(FileMedical);
                var FileChronicDeases = await this.chronicDiseasesManager.SelectChronicDiseasesByIdMedicalFileAsync(FileMedical.Id);
                foreach(var Item in FileChronicDeases)
                {
                    var chronicDeasses = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(Item.IdChronicDisease);
                    ListChronicDeasses.Add(chronicDeasses.NameChronicDiseases);
                }
                var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
                validationPatientIsNull(UserAccountPatient);
                var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(FileMedical.IdDoctor);
                validationPatientIsNull(UserAccountDoctor);
                var specialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(FileMedical.IdDoctor);
                
                var InformationPatient = MppperToPatientInformationDto(UserAccountPatient);
                var informationDoctor = MapperToDoctorInformationDto(specialities,UserAccountDoctor);
                var FileInformation = MapperToFileInformationDto(FileMedical, ListChronicDeasses);
                var radioInformation = MapperToRadioInformation(Radio);
                var Result = MapperToInformationRadioResult(radioInformation, FileInformation,InformationPatient, informationDoctor);
                return Result;










            });
      
    }
}
