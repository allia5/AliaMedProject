using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.AnalyseManager;
using Server.Managers.Storages.AnalyseResultManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.LineAnalyseMedicalManager;
using Server.Managers.Storages.LineRadioMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.RadiologyManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.RadioResultManager;
using Server.Managers.Storages.SpecialisteAnalyseManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using Server.Services.Foundation.MailService;
using static Server.Services.Foundation.ResultAnalyseService.ResultAnalyseMapperService;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.ResultAnalyseService
{
    public partial class ResultAnalyseService : IResultAnalyseService
    {
        public readonly IFileMedicalManager FileMedicalManager;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly UserManager<User> _UserManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly IAnalyseManager AnalyseManager;
        public readonly ILineAnalyseMedicalManager lineAnalyseMedicalManager;
        public readonly ISpecialisteAnalyseManager SpecialisteAnalyseManager;
        public readonly IMailService mailService;
        public readonly IAnalyseResultManager analyseResultManager;
        public readonly IPlanningAppoimentManager planningAppoimentManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public ResultAnalyseService(IWorkDoctorManager workDoctorManager,IPlanningAppoimentManager planningAppoimentManager,IAnalyseResultManager analyseResultManager,IMailService mailService,ISpecialisteAnalyseManager SpecialisteAnalyseManager, ILineAnalyseMedicalManager lineAnalyseMedicalManager,IAnalyseManager AnalyseManager,IOrdreMedicalManager ordreMedicalManager,IFileMedicalManager FileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, UserManager<User> _UserManager)
        { 
            this.workDoctorManager= workDoctorManager;
            this.planningAppoimentManager= planningAppoimentManager;
            this.analyseResultManager = analyseResultManager;
            this.mailService = mailService;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this._UserManager = _UserManager;
            this.lineAnalyseMedicalManager = lineAnalyseMedicalManager;
            this.SpecialisteAnalyseManager = SpecialisteAnalyseManager;
            this.FileMedicalManager = FileMedicalManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this.AnalyseManager= AnalyseManager;
        }

        public async Task<FileResultDto> GetFileResultAnalyse(string Email, string AppointmentId, string LineAnalyseId) =>
            await TryCatch_(async () =>
            {
                ValidateEntryOnGetFileResult(Email, AppointmentId, LineAnalyseId);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid(AppointmentId));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment, Doctor);
                var WorkDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Doctor.Id, Appointment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctor);
                /*-----*/
                var LineAnalyse = await this.lineAnalyseMedicalManager.SelectLineAnalyseById(DecryptGuid(LineAnalyseId));
                ValidateLineAnlayse(LineAnalyse);
                var Analyse = await this.AnalyseManager.SelectAnalyseByIdAsync(LineAnalyse.IdAnalyse);
                ValidateAnalyseIsNull(Analyse);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Analyse.IdOrdreMedical);
                ValidateOrdreMedical(OrdreMedical);
                var ResultLineAnalyse = await this.analyseResultManager.SelectResultAnalyseByIdLineMedcialAnalyse(LineAnalyse.Id);
                ValidateResultLineMedical(ResultLineAnalyse);
                return MapperToFileResultDto(ResultLineAnalyse);


            });

        public async Task<FileResultDto> GetFileResultAnalysePatient(string Email, string LineAnalyseId) =>
            await TryCatch_(async () =>
            {
                ValidateEntryOnGetFileResultPatient(Email,LineAnalyseId);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var LineAnalyse = await this.lineAnalyseMedicalManager.SelectLineAnalyseById(DecryptGuid(LineAnalyseId));
                ValidateLineAnlayse(LineAnalyse);
                var Analyse = await this.AnalyseManager.SelectAnalyseByIdAsync(LineAnalyse.IdAnalyse);
                ValidateAnalyseIsNull(Analyse);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Analyse.IdOrdreMedical);
                ValidateOrdreMedical(OrdreMedical);
                var ResultLineAnalyse = await this.analyseResultManager.SelectResultAnalyseByIdLineMedcialAnalyse(LineAnalyse.Id);
                ValidateResultLineMedical(ResultLineAnalyse);
                return MapperToFileResultDto(ResultLineAnalyse);


            });

        public async Task PostNewAnalyseResult(string Email, AnalyseResultToAdd analyseResultToAdd) =>
            await TryCatch(async () =>
            {
                ValidateEntryOnAddAnalyseResult(Email, analyseResultToAdd);
                var UserAccountSpecialisteAnalyse = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountSpecialisteAnalyse);
                var SpecialistAnalyse = await this.SpecialisteAnalyseManager.SelectSpecialisteAnalyseByIdUser(UserAccountSpecialisteAnalyse.Id);
                ValidateSpecialisteAnalyse(SpecialistAnalyse);
                var LignAnalyse = await this.lineAnalyseMedicalManager.SelectLineAnalyseById(DecryptGuid(analyseResultToAdd.IdLineAnalyse));
                ValidateLineAnalyseIsNull(LignAnalyse);
                var Analyse = await this.AnalyseManager.SelectAnalyseByIdAsync(LignAnalyse.IdAnalyse);
                ValidateAnalyseIsNull(Analyse);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Analyse.IdOrdreMedical);
                ValidateOrdreMedicalIsNull(OrdreMedical);
                var fileMedical = await this.FileMedicalManager.SelectFileMedicalByIdOrdreMedicalAsync(OrdreMedical.Id);
                ValidateFileMedicalIsNull(fileMedical);
                var PatientInformationAccount = await this._UserManager.FindByIdAsync(fileMedical.IdUser);
                validationPatientIsNull(PatientInformationAccount);
                var DoctorInformationAccount = await this.userManager.SelectUserByIdDoctor(OrdreMedical.IdDoctor);
                validationPatientIsNull(DoctorInformationAccount);
                var TypeFilUpload = GetFileType(analyseResultToAdd.FileUpload);
                var AnalyseResult = MapperToResultAnalyse(LignAnalyse.Id, TypeFilUpload, analyseResultToAdd.FileUpload);
                await this.analyseResultManager.InsertAnalyseResultAsync(AnalyseResult);
                var newLineAnalyseMedical = MapperToLineAnalyseMedicals(LignAnalyse, SpecialistAnalyse.Id);
                await this.lineAnalyseMedicalManager.UpdateLineAnalyseAsync(newLineAnalyseMedical);
                var mailRequest = MapperToMailRequestAddAnalyseResult(PatientInformationAccount, UserAccountSpecialisteAnalyse, newLineAnalyseMedical);
                await this.mailService.SendEmailNotification(mailRequest);
            });
        
           



        
    }
}
