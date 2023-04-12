using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.RolesManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.UserRoleManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.Doctor.Exceptions;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;
using Server.Models.UtilityModel;
using Server.Models.WorkDoctor;
using Server.Services.Foundation.JwtService;
using Server.Services.Foundation.MailService;
using System.Runtime.CompilerServices;
using static Server.Utility.Utility;
using static Server.Services.Foundation.PlanningAppoimentService.PlanningAppoimentMapperService;
using Server.Managers.Storages.SecretaryManager;

namespace Server.Services.Foundation.PlanningAppoimentService
{
    public partial class PlanningAppoimentService : IPlanningAppoimentService
    {
        public IMailService mailService { get; set; }
        public readonly IUserRoleManager userRoleManager;
        public readonly UserManager<User> _userManager;
        public readonly IUserManager userManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public readonly IDoctorManager doctorManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public readonly IPlanningAppoimentManager planningAppoimentManager;
        public readonly ISecretaryManager secretaryManager;
        public PlanningAppoimentService(ISecretaryManager secretaryManager, IUserManager userManager, IMailService mailService, UserManager<User> _userManager, ICabinetMedicalManager cabinetMedicalManager, IWorkDoctorManager workDoctorManager, IDoctorManager doctorManager, ISpecialitiesManager specialitiesManager, IPlanningAppoimentManager planningAppoimentManager)
        {
            this.doctorManager = doctorManager;
            this.mailService = mailService;
            this.userManager = userManager;
            this._userManager = _userManager;
            this.workDoctorManager = workDoctorManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.specialitiesManager = specialitiesManager;
            this.planningAppoimentManager = planningAppoimentManager;
            this.secretaryManager = secretaryManager;
        }
        public async Task<List<AppointmentInformationDto>> PostNewPlanningAppoimentMedical(string Email, KeysReservationMedicalDto keysReservationMedicalDto) =>
            await TryCatch(async () =>
            {
                List<AppointmentInformationDto> ListappointmentInformationDtos = new List<AppointmentInformationDto>();
                ValidateEntryOnPostNewAppoimentPlanning(Email, keysReservationMedicalDto);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                ValidateStatusUser(User);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(DecryptGuid(keysReservationMedicalDto.IdUserDoctor).ToString());
                ValidationDoctorIsNull(Doctor);
                ValidateStatusDocotor(Doctor);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(DecryptGuid(keysReservationMedicalDto.IdCabinet));
                ValidateCabinetMedicalIsNull(Cabinet);
                var Job = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(Doctor.Id, Cabinet.Id);
                ValidateWorkDoctorIsNull(Job);
                // var PlaningAppoiments = await this.planningAppoimentManager.SelectMedicalPlanningByIdDoctorIdCabinet(Doctor.Id, Cabinet.Id, DateTime.Now);
                // ValidateUserIsNotInListAppoiment(User.Id, PlaningAppoiments);
                var PlanningInformationModel = await GetDateReservation(Job, User);
                var MedicalPlanning = mapperToMedicalPlanning(PlanningInformationModel, Doctor, Cabinet, User.Id);
                await this.planningAppoimentManager.InsertMedicalPlanning(MedicalPlanning);

                var ListAllPlanningMedicalUser = await this.planningAppoimentManager.SelectMedicalPlanningByIdUser(User.Id);
                foreach (var ItemPlanning in ListAllPlanningMedicalUser)
                {
                    var JobPlanning = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(ItemPlanning.IdDoctor, ItemPlanning.IdCabinet);
                    var CabinetPlanning = await this.cabinetMedicalManager.SelectCabinetMedicalById(ItemPlanning.IdCabinet);
                    var ListSpecilities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(ItemPlanning.IdDoctor);
                    var ListStringSpecialities = ListSpecilities.Select(e => e.NameSpecialite).ToList();
                    var UserDoctor = await this.userManager.SelectUserByIdDoctor(ItemPlanning.IdDoctor);
                    //  ValidateUserIsNull(UserDoctor);
                    //  ValidateWorkDoctorIsNull(JobPlanning);
                    if (JobPlanning != null && UserDoctor != null && CabinetPlanning != null)
                    {
                        var DoctorAppoimentInformation = mapperToDoctorInformationAppointmentDto(UserDoctor, ListStringSpecialities, JobPlanning);
                        var CabinetAppoimentInformation = MapperToCabinetInformationAppointmentDto(CabinetPlanning);
                        var result = MapperToAppointmentInformationDto(DoctorAppoimentInformation, CabinetAppoimentInformation, ItemPlanning);
                        ListappointmentInformationDtos.Add(result);
                    }
                }

                return ListappointmentInformationDtos;
            });

        public async Task<List<AppointmentInformationDto>> GetListPlanningAppoimentMedical(string Email) =>
         await TryCatch(async () =>
            {
                List<AppointmentInformationDto> ListappointmentInformationDtos = new List<AppointmentInformationDto>();
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var ListAllPlanningMedicalUser = await this.planningAppoimentManager.SelectMedicalPlanningByIdUser(User.Id);
                foreach (var ItemPlanning in ListAllPlanningMedicalUser)
                {
                    var JobPlanning = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(ItemPlanning.IdDoctor, ItemPlanning.IdCabinet);
                    var CabinetPlanning = await this.cabinetMedicalManager.SelectCabinetMedicalById(ItemPlanning.IdCabinet);
                    var ListSpecilities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(ItemPlanning.IdDoctor);
                    var ListStringSpecialities = ListSpecilities.Select(e => e.NameSpecialite).ToList();
                    var UserDoctor = await this.userManager.SelectUserByIdDoctor(ItemPlanning.IdDoctor);
                    // ValidateUserIsNull(UserDoctor);
                    if (JobPlanning != null && UserDoctor != null && CabinetPlanning != null)
                    {
                        var DoctorAppoimentInformation = mapperToDoctorInformationAppointmentDto(UserDoctor, ListStringSpecialities, JobPlanning);
                        var CabinetAppoimentInformation = MapperToCabinetInformationAppointmentDto(CabinetPlanning);
                        var result = MapperToAppointmentInformationDto(DoctorAppoimentInformation, CabinetAppoimentInformation, ItemPlanning);
                        ListappointmentInformationDtos.Add(result);
                    }

                }

                return ListappointmentInformationDtos;
            });
        private async Task<PlanningInformationModel> GetDateReservation(WorkDoctors workDoctors, User user)
        {
            DateTime today = DateTime.Today;
            TimeSpan EndTime = workDoctors.EndTime.TimeOfDay;
            DateTime DateTimeEndTime = today.Add(EndTime);
            if (DateTime.Today > DateTimeEndTime)
            {
                var ListPlanningMedicalConfirmed = await this.planningAppoimentManager.SelectMedicalPlanningByIdDoctorIdCabinet(workDoctors.IdCabinet, workDoctors.IdDoctor, DateTime.Now.AddDays(1));

                if (ListPlanningMedicalConfirmed.Count() < workDoctors.NbPatientAvailble)
                {
                    ValidateUserIsNotInListAppoiment(user.Id, ListPlanningMedicalConfirmed);
                    return new PlanningInformationModel { DateAppoiment = DateTime.Now.AddDays(1), CountOfPatient = ListPlanningMedicalConfirmed.Count() + 1 };
                }
                else
                {
                    throw new NotFoundException("Date Reservation");
                }
            }
            else
            {
                var ListPlanningMedicalConfirmed = await this.planningAppoimentManager.SelectMedicalPlanningByIdDoctorIdCabinet(workDoctors.IdCabinet, workDoctors.IdDoctor, DateTime.Now);

                if (ListPlanningMedicalConfirmed.Count() < workDoctors.NbPatientAvailble)
                {
                    ValidateUserIsNotInListAppoiment(user.Id, ListPlanningMedicalConfirmed);
                    return new PlanningInformationModel { DateAppoiment = DateTime.Now, CountOfPatient = ListPlanningMedicalConfirmed.Count() + 1 };
                }
                else
                {
                    throw new NotFoundException("Date Reservation");
                }
            }

        }

        public async Task DeleteMedicalPlanningAppoiment(string Email, string IdPlanning) =>
            await TryCatch_(async () =>
            {
                List<string> ListEmail = new List<string>();
                ValidateEntryOnDelete(Email, IdPlanning);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var PlanningMedical = await this.planningAppoimentManager.SelectPalnningMedicalByIdPlanningIdUser(DecryptGuid(IdPlanning), User.Id);
                ValidatePlanningIsNull(PlanningMedical);
                await this.planningAppoimentManager.DeletePlanningMedical(PlanningMedical);
                var ListPlanningMedical = await this.planningAppoimentManager.SelectMedicalPlanningByIdDoctorIdCabinet(PlanningMedical.IdDoctor, PlanningMedical.IdCabinet);
                ListPlanningMedical = ListPlanningMedical.OrderBy(o => o.AppointmentCount).ToList();
                int k = 1;
                foreach (var Item in ListPlanningMedical)
                {
                    var UserPatient = await this._userManager.FindByIdAsync(Item.IdUser);
                    ValidateUserIsNull(UserPatient);
                    var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(Item.IdDoctor);
                    ValidateUserIsNull(UserAccountDoctor);
                    var newPlanningMedical = MapperToNewMedicalPlanning(Item, k);
                    await this.planningAppoimentManager.UpdatePlanningMedical(newPlanningMedical);
                    var MailRequest = MapperMailRequestDeleteMedicalAppoiment(UserPatient, UserAccountDoctor, k);
                    await this.mailService.SendEmailNotification(MailRequest);
                    k = k + 1;


                }
            });

        public async Task<List<PlanningDto>> GetPatientAppoimentMedicalSecretary(string Email, KeysAppoimentInformationSecretary keysAppoimentInformationSecretary) =>
            await _TryCatch(async () =>
            {
                List<PlanningDto> listAppoiments = new List<PlanningDto>();
                ValidateEntryOnGetAllAppoimentPatientSecretary(Email, keysAppoimentInformationSecretary);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                ValidateStatusUser(User);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(DecryptGuid(keysAppoimentInformationSecretary.IdDoctor).ToString());
                ValidationDoctorIsNull(Doctor);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(DecryptGuid(keysAppoimentInformationSecretary.CabinetId));
                ValidateCabinetMedicalIsNull(Cabinet);
                var workDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(Doctor.Id, Cabinet.Id);
                ValidateWorkDoctorIsNull(workDoctor);
                var AppoimentsMedical = await this.planningAppoimentManager.SelectMedicalPlanningByIdDoctorIdCabinet(Doctor.Id, Cabinet.Id);
                foreach (var item in AppoimentsMedical)
                {
                    var UserAppoiment = await this._userManager.FindByIdAsync(item.IdUser);
                    if (UserAppoiment != null)
                    {
                        var PatientInformation = MppperToPatientInformationDto(UserAppoiment);
                        var AppoimentInformation = MapperToPatientAppoimentInformationDto(item);
                        var AppoimentMedical = MapperToPlanningDto(PatientInformation, AppoimentInformation);
                        listAppoiments.Add(AppoimentMedical);
                    }
                }
                return listAppoiments;
            });

        public async Task<List<PlanningDto>> GetPatientAppoimentMedicalDoctor(string Email, KeysAppoimentInformationDoctor keysAppoimentInformationDoctor) =>
            await _TryCatch(async () =>
            {
                List<PlanningDto> listAppoiments = new List<PlanningDto>();
                ValidateEntryOnGetAllAppoimentPatientDoctor(Email, keysAppoimentInformationDoctor);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                ValidateStatusUser(User);
           
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(User.Id);
                ValidationDoctorIsNull(Doctor);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(DecryptGuid(keysAppoimentInformationDoctor.CabinetId));
                ValidateCabinetMedicalIsNull(Cabinet);
                var workDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(Doctor.Id, Cabinet.Id);
                ValidateWorkDoctorIsNull(workDoctor);
                var AppoimentsMedical = await this.planningAppoimentManager.SelectMedicalPlanningByIdDoctorIdCabinet(Cabinet.Id, Doctor.Id,keysAppoimentInformationDoctor.DateAppoiment);
                foreach (var item in AppoimentsMedical)
                {
                    var UserAppoiment = await this._userManager.FindByIdAsync(item.IdUser);
                    if (UserAppoiment != null)
                    {
                        var PatientInformation = MppperToPatientInformationDto(UserAppoiment);
                        var AppoimentInformation = MapperToPatientAppoimentInformationDto(item);
                        var AppoimentMedical = MapperToPlanningDto(PatientInformation, AppoimentInformation);
                        listAppoiments.Add(AppoimentMedical);
                    }
                }
                return listAppoiments;
            });

        public async Task UpdateStatusAppoimentMedical(string Email, UpdateStatusAppoimentDto updateStatusAppoiment,string Role) =>
            await _TryCatch_(async () =>
            {
                ValidateEntryOnUpdateStatusAppoiment(Email, updateStatusAppoiment);
                var UserAccount = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccount);
                
                if (Role == "SECRITAIRE")
                {
                    ValidateStatusSecretaryOnUpdate(updateStatusAppoiment);
                    
                }
                else if(Role == "MEDECIN")
                {
                    var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAccount.Id);
                    ValidationDoctorIsNull(Doctor);
                }
                var Appoiment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid(updateStatusAppoiment.Id));
                if(Appoiment != null)
                {
                    var newAppoiment =MapperToNewMedicalPlanning(updateStatusAppoiment, Appoiment);
                    await this.planningAppoimentManager.UpdatePlanningMedical(newAppoiment);

                }
                if (updateStatusAppoiment.statusPlaningDto == StatusPlaningDto.passed)
                {  
                    var appoimentsCabinetDoctor = await this.planningAppoimentManager.SelectMedicalPlanningByIdDoctorIdCabinet(Appoiment.IdCabinet,Appoiment.IdDoctor,DateTime.Now);
                     appoimentsCabinetDoctor = appoimentsCabinetDoctor.Where(e => e.Status == StatusPlaning.Still && e.IdUser != UserAccount.Id).OrderBy(e=>e.AppointmentCount).ToList();
                    int k = 1;
                    foreach (var appoimentPatient in appoimentsCabinetDoctor)
                    {
                        var UserAppoiment = await this._userManager.FindByIdAsync(appoimentPatient.IdUser);
                        var UserdoctorAppoiment = await this.userManager.SelectUserByIdDoctor(appoimentPatient.IdDoctor);
                        if(UserAppoiment != null && UserdoctorAppoiment != null)
                        {
                            appoimentPatient.AppointmentCount = k;
                            await this.planningAppoimentManager.UpdatePlanningMedical(appoimentPatient);
                            var mailRequest = MapperMailRequestDeleteMedicalAppoiment(UserAppoiment, UserdoctorAppoiment, k);
                            await this.mailService.SendEmailNotification(mailRequest);

                        }
                        k++;
                    }
                }
               else  if(updateStatusAppoiment.statusPlaningDto == StatusPlaningDto.absent)
                {
                    var UserOfAppoiment = await this._userManager.FindByIdAsync(Appoiment.IdUser);
                    var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(Appoiment.IdDoctor);
                    if(UserOfAppoiment != null && UserAccountDoctor != null)
                    {
                        var mailRequest = MapperMailRequestUpdateStatusAppoiment(updateStatusAppoiment, UserOfAppoiment, UserAccountDoctor);
                        await this.mailService.SendEmailNotification(mailRequest);

                    }
                }
            });

    }
}
