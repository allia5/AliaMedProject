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
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.WorkDoctorManager;

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
        public readonly ILineRadioMedicalManager lineRadioMedicalManager;
        public readonly IRadioResultManager radioResultManager;
        public readonly IRadiologyManager radiologyManager;
        public readonly IMailService mailService;
        public readonly IPlanningAppoimentManager planningAppoimentManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public ResultRadioService(IWorkDoctorManager workDoctorManager,IPlanningAppoimentManager planningAppoimentManager,IFileMedicalManager FileMedicalManager,IMailService mailService,IRadiologyManager radiologyManager,IRadioResultManager radioResultManager,ILineRadioMedicalManager lineRadioMedicalManager,IDoctorManager doctorManager,IUserManager userManager, UserManager<User> _UserManager, IOrdreMedicalManager ordreMedicalManager, IRadioManager radioManager)
        {
            this.workDoctorManager = workDoctorManager;
            this.planningAppoimentManager = planningAppoimentManager;
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
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Radio.IdOrdreMedical);
                ValidateOrdreMedicalIsNull(OrdreMedical);
                var FileMedical = await this.FileMedicalManager.SelectFileMedicalByIdOrdreMedicalAsync(OrdreMedical.Id);
                ValidateFileMedicalIsNull(FileMedical);
                var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
                validationPatientIsNull(UserAccountPatient);
                var TypeFileUpload =GetFileType(RadioResultToAddDto.FileUpload);
                var RadioResultMapper = MapperToResultRadio(LineRadio.Id, TypeFileUpload, RadioResultToAddDto);
               var Radioinserted= await this.radioResultManager.InserRadioResult(RadioResultMapper);
                var NewLineRadio = MapperToLineRadioMedical(LineRadio, Radiology);
                await this.lineRadioMedicalManager.UpdateLineRadioMedical(NewLineRadio);
                var MailRequest = MapperToMailRequestAddRadioResult(UserAccountPatient, UserAccountRadiology, LineRadio);
               await this.mailService.SendEmailNotification(MailRequest);
            });

        public async Task<FileResultDto> GetFileResultRadio(string Email, string AppointmentId, string LineRadioId) =>
            await TryCatch_(async () =>
            {
                ValidateEntryOnGetFileResult(Email, AppointmentId, LineRadioId);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid(AppointmentId));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment, Doctor);
                var WorkDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Doctor.Id, Appointment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctor);
                /****/
                var LineRadio = await this.lineRadioMedicalManager.SelectLineRadioById(DecryptGuid(LineRadioId));
                ValidateLineRadio(LineRadio);
                var Radio = await this.radioManager.SelectRadioByIdAsync(LineRadio.IdRadio);
                ValidateRadioIsNull(Radio);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Radio.IdOrdreMedical);
                ValidateOrdreMedical(OrdreMedical);
                var ResultLineRadio = await this.radioResultManager.SelectRadioResultByIdLineRadio(LineRadio.Id);
                ValidateResultLineMedical(ResultLineRadio);
                return MapperToFileResultDto(ResultLineRadio);
            });

        public async Task<FileResultDto> GetFileResultRadioPatient(string Email, string LineRadioId) =>
            await TryCatch_(async () =>
            {
                ValidateEntryOnGetFileResulPatient(Email,LineRadioId);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
               /* var Doctor = await this.doctorManager.SelectDoctorByIdUser(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid(AppointmentId));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment, Doctor);
                var WorkDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Doctor.Id, Appointment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctor);*/
                /****/
                var LineRadio = await this.lineRadioMedicalManager.SelectLineRadioById(DecryptGuid(LineRadioId));
                ValidateLineRadio(LineRadio);
                var Radio = await this.radioManager.SelectRadioByIdAsync(LineRadio.IdRadio);
                ValidateRadioIsNull(Radio);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Radio.IdOrdreMedical);
                ValidateOrdreMedical(OrdreMedical);
                var ResultLineRadio = await this.radioResultManager.SelectRadioResultByIdLineRadio(LineRadio.Id);
                ValidateResultLineMedical(ResultLineRadio);
                return MapperToFileResultDto(ResultLineRadio);
            });
    }
}
