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

        public PrescriptionService(IPharmacistManager pharmacistManager,IFileMedicalManager fileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, UserManager<User> _userManager, IOrdreMedicalManager ordreMedicalManager, IPrescriptionManager prescriptionManager, IFileChronicDiseasesManager fileChronicDiseasesManager, IChronicDiseasesManager chronicDiseasesManager, ISpecialitiesManager specialitiesManager, ILinePrescriptionMedicalManager linePrescriptionMedicalManager)
        {
            this.pharmacistManager = pharmacistManager;
            FileMedicalManager = fileMedicalManager;
            _UserManager = _userManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this.prescriptionManager = prescriptionManager;
            this.fileChronicDiseasesManager = fileChronicDiseasesManager;
            this.chronicDiseasesManager = chronicDiseasesManager;
            this.specialitiesManager = specialitiesManager;
            this.linePrescriptionMedicalManager = linePrescriptionMedicalManager;
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
            var Prescription = await this.prescriptionManager.SelectPrescriptionByCode(DecryptString(Code, "AJFNJjfjJZFJNdzj=="));
            ValidatePrescriptionIsNull(Prescription);
            var ordreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Prescription.IdMedicalOrdre);
            ValidateOrdreMedicalIsNull(ordreMedical);
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
           


        
    }
}
