using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
using static Server.Services.Foundation.ResultRadioService.ResultRadioMapperService;
using Server.Managers.Storages.LineRadioMedicalManager;
using Server.Managers.Storages.RadioResultManager;
using Server.Managers.Storages.RadiologyManager;
using Server.Services.Foundation.MailService;

namespace Server.Services.Foundation.ResultRadioService
{
    public partial class ResultRadioService : IResultRadioService
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
        public readonly ILineRadioMedicalManager lineRadioMedicalManager;
        public readonly IRadioResultManager radioResultManager;
        public readonly IRadiologyManager radiologyManager;
        public readonly IMailService mailService;
        public ResultRadioService(IFileMedicalManager FileMedicalManager,IMailService mailService,IRadiologyManager radiologyManager,IRadioResultManager radioResultManager,ILineRadioMedicalManager lineRadioMedicalManager,IDoctorManager doctorManager,IUserManager userManager, UserManager<User> _UserManager, IOrdreMedicalManager ordreMedicalManager, IRadioManager radioManager)
        {
            this.mailService = mailService;
            this.radiologyManager = radiologyManager;
            this.lineRadioMedicalManager = lineRadioMedicalManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this._UserManager = _UserManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this.radioManager = radioManager;
            this.radioResultManager=radioResultManager;
            this.FileMedicalManager = FileMedicalManager;
        }
        public async Task AddRadioResultService(string Email, RadioResultToAddDto RadioResultToAddDto) =>
            await TryCatch(async () =>
            {
                ValidateResultRadioOnAdd(Email, RadioResultToAddDto);
                var UserAccountRadiology = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountRadiology);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAccountRadiology.Id);
                ValidationDoctorIsNull(Doctor);
                var Radiology = await this.radiologyManager.SelectRadiologyByIdDoctor(Doctor.Id);
                ValidateRadiologyIsNull(Radiology);
                var LineRadio = await this.lineRadioMedicalManager.SelectLineRadioById(DecryptGuid(RadioResultToAddDto.IdLineRadio));
                ValidateLineRadioIsNull(LineRadio);
                var Radio = await this.radioManager.SelectRadioByIdAsync(LineRadio.IdRadio);
                ValidateRadioIsNull(Radio);
                var FileMedical = await this.FileMedicalManager.SelectFileMedicalByIdOrdreMedicalAsync(Radio.IdOrdreMedical);
                ValidateFileMedicalIsNull(FileMedical);
                var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
                validationPatientIsNull(UserAccountPatient);
                var TypeFileUpload =GetFileType(RadioResultToAddDto.FileUpload);
                var RadioResultMapper = MapperToResultRadio(LineRadio.Id, TypeFileUpload, RadioResultToAddDto);
                await this.radioResultManager.InserRadioResult(RadioResultMapper);
                var NewLineRadio = MapperToLineRadioMedical(LineRadio, Radiology);
                await this.lineRadioMedicalManager.UpdateLineRadioMedical(NewLineRadio);
                var MailRequest = MapperToMailRequestAddRadioResult(UserAccountPatient, UserAccountRadiology, LineRadio);
                await this.mailService.SendEmailNotification(MailRequest);




            });
    }
}
