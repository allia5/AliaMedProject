using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.LinePrescriptionMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.PharmacistManager;
using Server.Managers.Storages.PrescriptionManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using Server.Services.Foundation.MailService;
using static Server.Services.Foundation.PrescriptionLineService.PrescriptionLineMapperService;
using static Server.Utility.Utility;
namespace Server.Services.Foundation.PrescriptionLineService
{
    public partial class PrescriptionLineService : IPrescriptionLineService
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
        public readonly IMailService mailService;

        public PrescriptionLineService(IMailService mailService,IPharmacistManager pharmacistManager, IFileMedicalManager fileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, UserManager<User> _userManager, IOrdreMedicalManager ordreMedicalManager, IPrescriptionManager prescriptionManager, IFileChronicDiseasesManager fileChronicDiseasesManager, IChronicDiseasesManager chronicDiseasesManager, ISpecialitiesManager specialitiesManager, ILinePrescriptionMedicalManager linePrescriptionMedicalManager)
        {
            this.mailService = mailService;
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
        public async Task UpdateStatusPrescriptionLine(string Email, string PrecriptionLineId) =>
            await TryCatch(async () =>
            {
                ValidateEntryOnUpdatePrescriptionLine(Email, PrecriptionLineId);
                var UserAccountPharmacist = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountPharmacist);
                var Pharmaciste = await this.pharmacistManager.SelectPharmacistByIdUser(UserAccountPharmacist.Id);
                ValidatePharmacist(Pharmaciste);
                var PrescriptionLine = await this.linePrescriptionMedicalManager.SelectLinePrescriptionById(DecryptGuid(PrecriptionLineId));
                ValidatePrescriptionLine(PrescriptionLine);
                var Prescription = await this.prescriptionManager.SelectPrescriptioById(PrescriptionLine.IdPrescription);
                ValidatePrescriptionIsNull(Prescription);
                var FileMedical = await this.FileMedicalManager.SelectFileMedicalByIdOrdreMedicalAsync(Prescription.IdMedicalOrdre);
                ValidateFileMedicalIsNull(FileMedical);
                var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
                validationPatientIsNull(UserAccountPatient);
                var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(FileMedical.IdDoctor);
                validationPatientIsNull(UserAccountDoctor);
                var NewPrescriptionLine = MapperToPrescriptionLines(PrescriptionLine, Pharmaciste.Id);
                await this.linePrescriptionMedicalManager.UpdatePrescriptionLine(NewPrescriptionLine);
                var MailRequest = MapperToMailRequestOnUpdateStatusPrescriptionLine(NewPrescriptionLine, Pharmaciste, UserAccountPatient);
                await this.mailService.SendEmailNotification(MailRequest);
            });
        
           


        
    }
}
