using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.LinePrescriptionMedicalManager;
using Server.Managers.Storages.LineRadioMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.PrescriptionManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
using static Server.Services.Foundation.PrescriptionService.PrescriptionMapperService;
using static Server.Services.Foundation.PlanningAppoimentService.PlanningAppoimentMapperService;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;
using Server.Managers.Storages.PharmacistManager;
using Server.Models.Prescriptions;
using Microsoft.AspNetCore.Http.Features;
using Server.Models.Specialites;
using Server.Managers.Storages.SecretaryManager;
using Server.Managers.Storages.CabinetMedicalManager;

namespace Server.Services.Foundation.PrescriptionService
{
    public partial class PrescriptionService :IPrescriptionService
    {
        public readonly IFileMedicalManager FileMedicalManager;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly UserManager<User> _UserManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly IPrescriptionManager prescriptionManager;
        public readonly IFileChronicDiseasesManager fileChronicDiseasesManager;
        public readonly IChronicDiseasesManager chronicDiseasesManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public readonly ILinePrescriptionMedicalManager linePrescriptionMedicalManager;
        public readonly IPharmacistManager pharmacistManager;
        public readonly ISecretaryManager secretaryManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IConfiguration configuration;

        public PrescriptionService(IConfiguration configuration,ICabinetMedicalManager cabinetMedicalManager,ISecretaryManager secretaryManager,IPharmacistManager pharmacistManager,IFileMedicalManager fileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, UserManager<User> _userManager, IOrdreMedicalManager ordreMedicalManager, IPrescriptionManager prescriptionManager, IFileChronicDiseasesManager fileChronicDiseasesManager, IChronicDiseasesManager chronicDiseasesManager, ISpecialitiesManager specialitiesManager, ILinePrescriptionMedicalManager linePrescriptionMedicalManager)
        {
            this.configuration = configuration;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.pharmacistManager = pharmacistManager;
            this.FileMedicalManager = fileMedicalManager;
            this._UserManager = _userManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this.prescriptionManager = prescriptionManager;
            this.fileChronicDiseasesManager = fileChronicDiseasesManager;
            this.chronicDiseasesManager = chronicDiseasesManager;
            this.specialitiesManager = specialitiesManager;
            this.linePrescriptionMedicalManager = linePrescriptionMedicalManager;
            this.secretaryManager = secretaryManager;
        }

        public async Task<InformationPrescriptionResultDto> GetPrescriptionInformation(string Email, string Code) =>
        await TryCatch(async () =>
        {
            List<string> ListChronicDeasses = new List<string>();
            List<string> ListSpecialitiesDoctor = new List<string>();
            List<LinePrescriptionDto> LinesPrescriptionDto = new List<LinePrescriptionDto>();
            ValidateEntryOnGetPrescriptionInformation(Email, Code);
            var UserAcountPharmacist = await this._UserManager.FindByEmailAsync(Email);
            ValidateUserIsNull(UserAcountPharmacist);
            var Pharmacist = await this.pharmacistManager.SelectPharmacistByIdUser(UserAcountPharmacist.Id);
            ValidatePharmacist(Pharmacist);
            var Prescription = await this.prescriptionManager.SelectPrescriptionByCode(DecryptString(Code, configuration["KeysQrCod:KeyOrdreMedical"]));
            ValidatePrescriptionIsNull(Prescription);
            var ordreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Prescription.IdMedicalOrdre);
            ValidateOrdreMedical(ordreMedical);
            var FileMedical = await this.FileMedicalManager.SelectFileMedicalByIdOrdreMedicalAsync(ordreMedical.Id);
            ValidateFileMedicalIsNull(FileMedical);
            var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
            validationPatientIsNull(UserAccountPatient);
            var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(FileMedical.IdDoctor);
            validationPatientIsNull(UserAccountDoctor);
            var FileChronicDeasses = await this.chronicDiseasesManager.SelectChronicDiseasesByIdMedicalFileAsync(FileMedical.Id);
            foreach (var ItemChronicDeses in FileChronicDeasses)
            {
                var ChronicDeasses = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(ItemChronicDeses.IdChronicDisease);
                ListChronicDeasses.Add(ChronicDeasses.NameChronicDiseases);
            }
            var LinePrescriptionLine = await this.linePrescriptionMedicalManager.SelectLinePrescriptionByIdPrescription(Prescription.Id);
            LinePrescriptionLine = LinePrescriptionLine.Where(e=>e.StatusPrescriptionLine == Models.PrescriptionLine.StatusPrescriptionLine.NotValidate).ToList();
            foreach (var LinePresription in LinePrescriptionLine)
            {
                LinesPrescriptionDto.Add(new LinePrescriptionDto { Description = LinePresription.Description, IdLine = EncryptGuid(LinePresription.Id), MedicamentName = LinePresription.MedicamentName, Quantity = LinePresription.Dosage });
            }
            var SpecialitiesDoctor = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(FileMedical.IdDoctor);
            var InformationPatient = MppperToPatientInformationDto(UserAccountPatient);
            var informationDoctor = MapperToDoctorInformationDto(SpecialitiesDoctor, UserAccountDoctor);
            var FileInformation = MapperToFileInformationDto(FileMedical, ListChronicDeasses);
            var PrescriptionInformation = MapperToPrescriptionInformationDto(LinesPrescriptionDto, ordreMedical, Prescription);
            var InformationPrescriptionResult = MapperToInformationPrescriptionResultDto(FileInformation, InformationPatient, informationDoctor, PrescriptionInformation);
            return InformationPrescriptionResult;
        });

        public async Task<byte[]> GetFilePrescriptionByIdOrdreMedical(string Email, string OrdreMedicalId, string CabinetId) =>
         await TryCatch(async () =>
         {
             ValidateEntryOnGetFilePrescriptionBySecritary(Email,OrdreMedicalId,CabinetId);
             var UserAccountSecritary = await this._UserManager.FindByEmailAsync(Email);
             ValidateUserIsNull(UserAccountSecritary);
             var Secritary = await this.secretaryManager.SelectSecretaryByIdUserIdCabinet(UserAccountSecritary.Id,DecryptGuid(CabinetId));
             ValidateSecritary(Secritary);
             var CabinetMedical = await this.cabinetMedicalManager.SelectCabinetMedicalById(Secritary.IdCabinetMedical);
             ValidateCabinetMedical(CabinetMedical);
             var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
             ValidateOrdreMedicalIsNull(OrdreMedical);
             var FileMedicalPrescription = await this.prescriptionManager.SelectPrescriptionByIdMedicalOrdreAsync(OrdreMedical.Id);
             ValidatePrescriptionIsNull(FileMedicalPrescription);
             return FileMedicalPrescription.FilePrescription;


         });

        public async Task<byte[]> PatientGetFilePrescriptionByIdOrdreMedical(string Email, string OrdreMedicalId) =>
         await TryCatch(async () =>
         {
             ValidateEntryOnGetFilePrescriptionByPatient(Email, OrdreMedicalId);
             var UserAccount= await this._UserManager.FindByEmailAsync(Email);
             ValidateUserIsNull(UserAccount);
             var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
             ValidateOrdreMedical(OrdreMedical);
             var DoctorAccount = await this.userManager.SelectUserByIdDoctor(OrdreMedical.IdDoctor);
             ValidateUserIsNull(DoctorAccount);
             var Doctor = await this.doctorManager.SelectDoctorByIdUser(DoctorAccount.Id);
             ValidationDoctorIsNull(Doctor);
             var FileMedicalPrescription = await this.prescriptionManager.SelectPrescriptionByIdMedicalOrdreAsync(OrdreMedical.Id);
             ValidatePrescriptionIsNull(FileMedicalPrescription);
             return FileMedicalPrescription.FilePrescription;


         });
    }
}
