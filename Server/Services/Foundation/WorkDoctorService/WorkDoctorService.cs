using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.Admin;
using Server.Models.CabinetMedicals;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;
using Server.Services.Foundation.MailService;
using static Server.Services.Foundation.WorkDoctorService.WorkDoctorServiceMapper;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.WorkDoctorService
{
    public partial class WorkDoctorService : IWorkDoctorService
    {
        public readonly UserManager<User> _userManager;
        public readonly IDoctorManager doctorManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public readonly IMailService mailService;
        public readonly ISpecialitiesManager specialitiesManager;
        public readonly IUserManager userManager;
        public WorkDoctorService(UserManager<User> _userManager, IDoctorManager doctorManager, ICabinetMedicalManager cabinetMedicalManager, IWorkDoctorManager workDoctorManager, IMailService mailService, ISpecialitiesManager specialitiesManager, IUserManager userManager)
        {
            this._userManager = _userManager;
            this.doctorManager = doctorManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.workDoctorManager = workDoctorManager;
            this.mailService = mailService;
            this.specialitiesManager = specialitiesManager;
            this.userManager = userManager;
        }

        public async Task<List<InvitationsDoctorDto>> GetInvitationDoctor(string Email) =>
      await _TryCatch(async () =>
      {
          List<InvitationsDoctorDto> invitationsDoctors = new List<InvitationsDoctorDto>();
          ValidateEmailIsNull(Email);
          var UserAccount = await this._userManager.FindByEmailAsync(Email);
          ValidateUserIsNull(UserAccount);
          var Doctor = await this.doctorManager.SelectDoctorByIdUser(UserAccount.Id);
          ValidationDoctorIsNull(Doctor);
          var ListWorkDoctor = await this.workDoctorManager.SelectWorksDoctorByIdDoctor(Doctor.Id);
          //  ValidateListInvitationWorkDoctor(ListWorkDoctor);
          foreach (var item in ListWorkDoctor)
          {
              var cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalById(item.IdCabinet);
              ValidateCabinetMedicalIsNull(cabinet);
              var invitation = MapperToInvitationsDoctorDto(cabinet, item);
              invitationsDoctors.Add(invitation);


          }
          return invitationsDoctors;



      });

        public async Task PostNewInvitationWorkDoctor(string Email, string UserId) =>
            await TryCatch(async () =>
            {
                ValidateOnPostInvitationWorkDoctor(Email, UserId);
                var UserAdmin = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAdmin);
                var UserAccountDoctor = await this._userManager.FindByIdAsync(UserId);
                ValidateUserIsNull(UserAccountDoctor);
                var UserDoctor = await this.doctorManager.SelectDoctorByIdUser(UserId);
                ValidationDoctorIsNull(UserDoctor);
                var CabinetMedicalOfAdmin = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(UserAdmin.Id);
                ValidateCabinetMedicalIsNull(CabinetMedicalOfAdmin);
                var WorkDoctor = MapperToWorkDoctor(CabinetMedicalOfAdmin.Id, UserDoctor.Id);
                var WorkDoctorResultInsert = await this.workDoctorManager.InsertWorkDoctor(WorkDoctor);
                ValidateWorkDoctorIsNull(WorkDoctorResultInsert);
                var mailRequest = MapperMailRequest(UserAccountDoctor, CabinetMedicalOfAdmin);
                await this.mailService.SendEmailNotification(mailRequest);



            });

        public async Task UpdateStatusServiceWorkDoctor(string Email, UpdateStatusWorkDoctorDto updateStatusWorkDoctorDto) =>
            await TryCatch(async () =>
            {
                ValidateOnUpdateStatusWorkDoctoter(Email, updateStatusWorkDoctorDto);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(User.Id);
                ValidationDoctorIsNull(Doctor);
                var WorkDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorWithIdWorkDoctor(DecryptGuid(updateStatusWorkDoctorDto.WorkId), Doctor.Id);
                ValidateWorkDoctorIsNull(WorkDoctor);
                var Admin = await this.userManager.SelectUserByIdCabinet(WorkDoctor.IdCabinet);
                ValidateUserIsNull(Admin);
                var newWorkDoctor = MapperToNewWorkDoctorStatusService(WorkDoctor, updateStatusWorkDoctorDto);
                var result = await this.workDoctorManager.UpdateWorkDoctor(WorkDoctor);
                var mailrequest = MapperMailRequestUpdateStatJobDoctor(Admin, result);
                await this.mailService.SendEmailNotification(mailrequest);



            });

        public async Task<List<JobsDoctorDto>> GetListJobsDoctorService(string Email) =>
            await TryCatch_(async () =>
            {
                List<JobsDoctorDto> jobsDoctorDtos = new List<JobsDoctorDto>();
                ValidateEmailIsNull(Email);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(User.Id);
                ValidationDoctorIsNull(Doctor);
                var ListJobsDoctor = await this.workDoctorManager.SelectWorksDoctorActiveByIdDoctor(Doctor.Id);
                foreach (var item in ListJobsDoctor)
                {
                    var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalById(item.IdCabinet);
                    ValidateCabinetMedicalIsNull(Cabinet);
                    var JobDoctorDto = MapperToJobsDoctorDto(item, Cabinet);
                    jobsDoctorDtos.Add(JobDoctorDto);
                }
                return jobsDoctorDtos;

            });

        public async Task<JobSettingDto> GetJobDoctorById(string Email, string IdJob) =>
            await _TryCatch_(async () =>
            {
                ValidateOnPostInvitationWorkDoctor(Email, IdJob);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(User.Id);
                ValidationDoctorIsNull(Doctor);
                var JobDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorWithIdWorkDoctor(DecryptGuid(IdJob), Doctor.Id);
                ValidateWorkDoctorIsNull(JobDoctor);
                return MapperToJobSetting(JobDoctor);



            });

        public async Task UpdateSettingJobDoctor(JobSettingDto jobSettingDto, string Email) =>
            await TryCatch(async () =>
            {
                ValidateOnUpdateSettingJob(jobSettingDto);
                ValidateEmailIsNull(Email);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(User.Id);
                ValidationDoctorIsNull(Doctor);
                var job = await this.workDoctorManager.SelectWorkDoctorByIdDoctorWithIdWorkDoctor(DecryptGuid(jobSettingDto.IdJob), Doctor.Id);
                ValidateWorkDoctorIsNull(job);
                job = job.MapperToJobDoctorSetting(jobSettingDto);
                await this.workDoctorManager.UpdateWorkDoctor(job);

            });

        public async Task<List<DoctorCabinetDto>> GetDoctorInformationFromCabinet(string Email) =>
            await _TryCatch_1(async () =>
            {
                List<DoctorCabinetDto> ListResult = new List<DoctorCabinetDto>();
                ValidateEmailIsNull(Email);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(User.Id);
                ValidateCabinetMedicalIsNull(Cabinet);
                var ListJobByCabinet = await this.workDoctorManager.SelectWorksDoctorByIdCabinet(Cabinet.Id);
                foreach (var item in ListJobByCabinet)
                {
                    var Doctor = await this.doctorManager.SelectDoctorById(item.IdDoctor);
                    var Specialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(Doctor.Id);
                    ValidationDoctorIsNull(Doctor);
                    var user = await this.userManager.SelectUserByIdDoctor(Doctor.Id);
                    ValidateUserIsNull(user);
                    var jobsetting = MapperToJobSetting(item);
                    var result = MapperToDoctorCabinetDto(Specialities, Doctor, jobsetting, user, item);
                    ListResult.Add(result);
                }
                return ListResult;

            });

        public async Task DeleteWorkDoctorByAdmin(string Email, string IdJob) =>
            await TryCatch(async () =>
            {
                ValidateEmailIsNull(Email);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(User.Id);
                ValidateCabinetMedicalIsNull(Cabinet);
                var Job = await this.workDoctorManager.SelectWorkDoctorByIdAndIdCabinet(DecryptGuid(IdJob), Cabinet.Id);
                ValidateWorkDoctorIsNull(Job);
                var Doctor = await this.doctorManager.SelectDoctorById(Job.IdDoctor);
                var userDoctor = await this.userManager.SelectUserByIdDoctor(Job.IdDoctor);
                await this.workDoctorManager.DeleteWorkDoctor(Job);
                var mailRequest = MapperMailRequestDeleteUser(userDoctor, Cabinet);
                await this.mailService.SendEmailNotification(mailRequest);
            });

        public async Task DeleteInvitationWorkDoctorByDoctor(string Email, string jobId) =>
            await TryCatch(async () =>
            {
                ValidateOnPostInvitationWorkDoctor(Email, jobId);
                var UserDoctor = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(UserDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var JobInvitation = await this.workDoctorManager.SelectWorkDoctorByIdDoctorWithIdWorkDoctor(DecryptGuid(jobId), Doctor.Id);
                ValidateWorkDoctorIsNull(JobInvitation);
                var UsersAdmin = await this.userManager.SelectUsersAdminByIdCabinet(JobInvitation.IdCabinet);
                ValidateListUsers(UsersAdmin);
                await this.workDoctorManager.DeleteWorkDoctor(JobInvitation);
                foreach (var item in UsersAdmin)
                {
                    var mailRequest = MapperToMailRequestWhenDeleteInvitationFromMedecin(UserDoctor, item);
                    await this.mailService.SendEmailNotification(mailRequest);
                }
            });


    }
}
