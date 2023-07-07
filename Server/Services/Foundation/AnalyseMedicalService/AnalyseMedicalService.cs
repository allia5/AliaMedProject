using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.AnalyseManager;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.LineAnalyseMedicalManager;
using Server.Managers.Storages.LineRadioMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.SpecialisteAnalyseManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
using static Server.Services.Foundation.PlanningAppoimentService.PlanningAppoimentMapperService;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;
using static Server.Services.Foundation.AnalyseMedicalService.AnalyseMedicalMapperService;
using Server.Models.FileChronicDisease;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.SecretaryManager;

namespace Server.Services.Foundation.AnalyseMedicalService
{
    public partial class AnalyseMedicalService : IAnalyseMedicalService
    {
        public readonly IConfiguration configuration;
        public readonly IFileMedicalManager FileMedicalManager;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly UserManager<User> _UserManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly IAnalyseManager AnalyseManager;
        public readonly IFileChronicDiseasesManager fileChronicDiseasesManager;
        public readonly IChronicDiseasesManager chronicDiseasesManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public readonly ILineAnalyseMedicalManager lineAnalyseMedicalManager;
        public readonly ISpecialisteAnalyseManager specialisteAnalyseManager;
        public readonly ISecretaryManager secretaryManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;

        public AnalyseMedicalService(IConfiguration configuration,ICabinetMedicalManager cabinetMedicalManager,ISecretaryManager secretaryManager,ISpecialisteAnalyseManager specialisteAnalyseManager,ILineAnalyseMedicalManager lineAnalyseMedicalManager, ISpecialitiesManager specialitiesManager, IFileChronicDiseasesManager fileChronicDiseasesManager, IChronicDiseasesManager chronicDiseasesManager, UserManager<User> _UserManager, IFileMedicalManager FileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, IPlanningAppoimentManager planningAppoimentManager, IOrdreMedicalManager ordreMedicalManager, IAnalyseManager AnalyseManager)
        {
            this.configuration = configuration;
            this.specialisteAnalyseManager = specialisteAnalyseManager;
            this.lineAnalyseMedicalManager = lineAnalyseMedicalManager;
            this.specialitiesManager = specialitiesManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this._UserManager = _UserManager;
            this.FileMedicalManager = FileMedicalManager;
            this.AnalyseManager = AnalyseManager;
            this.chronicDiseasesManager = chronicDiseasesManager;
            this.fileChronicDiseasesManager = fileChronicDiseasesManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.secretaryManager = secretaryManager;


        }
        public async Task<InformationAnalyseResultDto> GetAllAnalyseResultByCode(string Email, string codeQr) =>
            await TryCatch(async () =>
            {
                List<string> ListChronicDeasses = new List<string>();
                List<string> ListSpecialitiesDoctor = new List<string>();
                List<LinesAnalyseDto> LinesAnalyse = new List<LinesAnalyseDto>();
                ValidateEntryOnGetAnalyseInformation(Email, codeQr);
                var UserAccountSpecialisteAnalyse = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountSpecialisteAnalyse);
                var SpecialistAnalyse = await this.specialisteAnalyseManager.SelectSpecialisteAnalyseByIdUser(UserAccountSpecialisteAnalyse.Id);
                ValidateSpecialisteAnalyseIsNull(SpecialistAnalyse);
                var Analyse = await this.AnalyseManager.SelectAnalyseByCodeAsync(DecryptString(codeQr, configuration["KeysQrCod:KeyOrdreMedical"]));
                ValidateAnalyseIsNull(Analyse);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Analyse.IdOrdreMedical);
                ValidateOrdreMedical(OrdreMedical);
                var FileMedical = await this.FileMedicalManager.SelectFileMedicalByIdOrdreMedicalAsync(OrdreMedical.Id);
                ValidateFileMedicalIsNull(FileMedical);
                var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
                validationPatientIsNull(UserAccountPatient);
                var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(OrdreMedical.IdDoctor);
                validationPatientIsNull(UserAccountDoctor);
                var specialitiesDoctor = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(OrdreMedical.IdDoctor);
                var ChronicDeasses = await this.chronicDiseasesManager.SelectChronicDiseasesByIdMedicalFileAsync(FileMedical.Id);
                
                foreach (var Item in ChronicDeasses)
                {
                    var chronicDeasses = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(Item.IdChronicDisease);
                    ListChronicDeasses.Add(chronicDeasses.NameChronicDiseases);
                }
                var LineAnalyse = await this.lineAnalyseMedicalManager.SelectLinesMedicalByIdAnalyseAsync(Analyse.Id);
                LineAnalyse = LineAnalyse.Where(e => e.Status == Models.Analyse.StatusAnalyse.notValidate).ToList();
               foreach (var ItemAnalyse in LineAnalyse)
                {
                    LinesAnalyse.Add(new LinesAnalyseDto { Description = ItemAnalyse.description, Instruction = ItemAnalyse.Instruction, IdLineAnalyse =EncryptGuid (ItemAnalyse.Id) });
                }
                var FileInformation = MapperToFileInformationDto(FileMedical, ListChronicDeasses);
                var DoctorInformation = MapperToDoctorInformationDto(specialitiesDoctor, UserAccountDoctor);
                var PatientInformation = MppperToPatientInformationDto(UserAccountPatient);
                var InformationAnalyse = MapperToAnalyseInformation(Analyse, LinesAnalyse,OrdreMedical);
                var resultInformationAnalyse = MapperToInformationAnalyseResult(InformationAnalyse, FileInformation, PatientInformation, DoctorInformation);
                return resultInformationAnalyse;




            });

        public async Task<byte[]> PatientGetFileAnalyseByIdOrdreMedical(string Email, string OrdreMedicalId) =>
          await TryCatch(async () =>
          {
              ValidateEntryOnGetFileAnalyseByPatient(Email, OrdreMedicalId);
              var UserAccount = await this._UserManager.FindByEmailAsync(Email);
              ValidateUserIsNull(UserAccount);
              var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
              ValidateOrdreMedical(OrdreMedical);
              var DoctorAccount = await this.userManager.SelectUserByIdDoctor(OrdreMedical.IdDoctor);
              ValidateUserIsNull(DoctorAccount);
              var Doctor = await this.doctorManager.SelectDoctorByIdUser(DoctorAccount.Id);
              ValidationDoctorIsNull(Doctor);
              var FileAnalyse = await this.AnalyseManager.SelectAnalyseByOrdreMedicalId(DecryptGuid(OrdreMedicalId));
              ValidateAnalyseIsNull(FileAnalyse);
              return DecryptFile( FileAnalyse.FileAnalyse);
          });

        public async Task<byte[]> SecritaryGetFileAnalyseByIdOrdreMedical(string Email,string OrdreMedicalId,string CabinetId) =>
          await TryCatch(async () =>
          {
              ValidateEntryOnGetFileAnalyse(Email, OrdreMedicalId, CabinetId);
              var UserAccountSecritary = await this._UserManager.FindByEmailAsync(Email);
              ValidateUserIsNull(UserAccountSecritary);
              var Secritary = await this.secretaryManager.SelectSecretaryByIdUserIdCabinet(UserAccountSecritary.Id, DecryptGuid(CabinetId));
              ValidateSecritary(Secritary);
              var CabinetMedical = await this.cabinetMedicalManager.SelectCabinetMedicalById(Secritary.IdCabinetMedical);
              ValidateCabinetMedical(CabinetMedical);
              var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
              ValidateOrdreMedicalIsNull(OrdreMedical);
              var FileAnalyse = await this.AnalyseManager.SelectAnalyseByOrdreMedicalId(DecryptGuid(OrdreMedicalId));
              ValidateAnalyseIsNull(FileAnalyse);
              return DecryptFile( FileAnalyse.FileAnalyse);
          });
    }
}
