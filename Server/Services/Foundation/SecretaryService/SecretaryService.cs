using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.SecretaryManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.UserRoleManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.secretary;
using Server.Models.UserAccount;
using Server.Services.Foundation.MailService;
using static Server.Utility.Utility;
using static Server.Services.Foundation.SecretaryService.SecretaryMapperService;

namespace Server.Services.Foundation.SecretaryService
{
    public partial class SecretaryService : ISecretaryService
    {
        public readonly UserManager<User> _userManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IMailService mailService;
        public readonly IUserManager userManager;
        public readonly ISecretaryManager secretaryManager;
        public readonly IUserRoleManager userRoleManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public readonly ISpecialitiesManager specialitiesManager;

        public SecretaryService(IUserManager userManager, ISpecialitiesManager specialitiesManager, UserManager<User> _userManager, ICabinetMedicalManager cabinetMedicalManager, IMailService mailService, ISecretaryManager secretaryManager, IUserRoleManager userRoleManager, IWorkDoctorManager workDoctorManager)
        {
            this.userManager = userManager;
            this._userManager = _userManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.mailService = mailService;
            this.secretaryManager = secretaryManager;
            this.userRoleManager = userRoleManager;
            this.workDoctorManager = workDoctorManager;
            this.specialitiesManager = specialitiesManager;
        }






        public async Task<List<SecretaryCabinetInformationDto>> GetAllCabinetInformationAppoiment(string Email) =>
            await TrycCatchFour(async () =>
            {
                List<DoctorInformationAppointmentDto> ListdoctorInformation = new List<DoctorInformationAppointmentDto>();
                List<SecretaryCabinetInformationDto> ListsecretaryCabinetInformationDtos = new List<SecretaryCabinetInformationDto>();
                ValidateEntry(Email);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var ListSecritary = await this.secretaryManager.SelectSecretayByIdUser(User.Id);
                foreach (var secretary in ListSecritary)
                {
                    var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(secretary.IdCabinetMedical);
                    if (Cabinet != null)
                    {
                        var CabinetInformation = MapperToCabinetInformationAppointmentDto(Cabinet);
                        var Jobs = await this.workDoctorManager.SelectAllWorkDoctorWithStatusActiveByIdCabinet(Cabinet.Id);
                        foreach (var job in Jobs)
                        {
                            var UserDoctor = await this.userManager.SelectUserByIdDoctor(job.IdDoctor);
                            if (UserDoctor != null)
                            {
                                var ListResult = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(job.IdDoctor);
                                var ListSpecialities = ListResult.Select(e => e.NameSpecialite).ToList();
                                var DoctorInformation = mapperToDoctorInformationAppointmentDto(UserDoctor, ListSpecialities, job);
                                ListdoctorInformation.Add(DoctorInformation);
                            }
                        }
                        var SecretaryCabinetInformation = MapperToSecretaryCabinetInformationDto(ListdoctorInformation, CabinetInformation);
                        ListsecretaryCabinetInformationDtos.Add(SecretaryCabinetInformation);
                        ListdoctorInformation = new List<DoctorInformationAppointmentDto>();


                    }
                }
                return ListsecretaryCabinetInformationDtos;

            });








        public async Task<SecritaryDto> AddSecretaryService(string EmailSecretary, string EmailAdmin) =>
            await TryCatch(async () =>
            {
                ValidateEntry(EmailSecretary, EmailAdmin);
                var AdminUser = await this._userManager.FindByEmailAsync(EmailAdmin);
                ValidateUserIsNull(AdminUser);
                var SecretaryUser = await this._userManager.FindByEmailAsync(EmailSecretary);
                ValidateUserSecretaryIsNull(SecretaryUser);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(AdminUser.Id);
                ValidateCabinetMedicalIsNull(Cabinet);
                var Secretary = await this.secretaryManager.SelectSecretaryByIdUserIdCabinet(SecretaryUser.Id, Cabinet.Id);
                ValidateSecritay(Secretary);
                var Listsecritary = await this.secretaryManager.SelectSecretayByIdUser(SecretaryUser.Id);
                Secretarys newSecretary = new Secretarys();
                if (Secretary == null)
                {
                    var MaperSecretary = MapperToSecritary(Cabinet.Id, SecretaryUser.Id);
                    newSecretary = await this.secretaryManager.InsertSecretary(MaperSecretary);
                    if (Listsecritary.Count() == 0)
                    {
                        var UserRole = MaperToUserRole(SecretaryUser.Id, Guid.Parse("0D518584-64A4-424B-B011-7283083394B8"));
                        await this.userRoleManager.InsertUserRoleAsync(UserRole);

                    }
                }
                else
                {
                    newSecretary = MapperSecretary(Secretary);
                    await this.secretaryManager.UpdateSecretary(newSecretary);
                }
                var mailRequest = MapperToMailRequest(SecretaryUser, Cabinet);
                await this.mailService.SendEmailNotification(mailRequest);
                return MapperToSecretaryDto(SecretaryUser, newSecretary);

            });



        public async Task<List<SecritaryDto>> GetAllSecretary(string Email) =>
            await TryCatch_(async () =>
            {
                List<SecritaryDto> resultList = new List<SecritaryDto>();
                ValidateEntry(Email);
                var AdminUser = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(AdminUser);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(AdminUser.Id);
                ValidateCabinetMedicalIsNull(Cabinet);
                var ListSecretary = await this.secretaryManager.SelectSecretaryByIdCabinet(Cabinet.Id);
                foreach (var item in ListSecretary)
                {
                    var UserAccount = await this._userManager.FindByIdAsync(item.IdUser);
                    ValidateUserIsNull(UserAccount);
                    var secretary = MapperToSecretaryDto(UserAccount, item);
                    resultList.Add(secretary);
                }
                return resultList;

            });

        public async Task UpdateStatusSecretaryService(UpdateStatusSecretaryDto updateStatusSecretaryDto, string Email) =>
            await _TryCatch(async () =>
            {
                ValidateOnUpdateStatusSecretary(Email, updateStatusSecretaryDto);
                var User = await this._userManager.FindByEmailAsync(Email);
                ValidateUserIsNull(User);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(User.Id);
                var Secretary = await this.secretaryManager.SelectSecretaryByIdAndIdCabinet(DecryptGuid(updateStatusSecretaryDto.SecretaryId), Cabinet.Id);
                ValidateSecritayIsNull(Secretary);
                var SecretaryUser = await this._userManager.FindByIdAsync(Secretary.IdUser);
                ValidateUserIsNull(SecretaryUser);
                var newSecretary = MapperToNewSecretary(updateStatusSecretaryDto, Secretary);
                await this.secretaryManager.UpdateSecretary(newSecretary);
                var mailRequest = MapperToMailRequest(SecretaryUser, Cabinet);
                await this.mailService.SendEmailNotification(mailRequest);
            });


    }
}
