using DTO;

using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.RolesManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.UserRoleManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using Server.Services.Foundation.JwtService;
using Server.Services.Foundation.MailService;
using static Server.Services.UserService.UserMapperService;

namespace Server.Services.UserService
{
    public partial class UserService : IUserService
    {
        public readonly UserManager<User> _userManager;
        public IMailService mailService { get; set; }
        public readonly IUserRoleManager userRoleManager;
        public readonly IRolesManager rolesManager;
        public readonly IJwtService jwtService;
        public readonly IUserManager userManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public readonly IDoctorManager doctorManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public UserService(UserManager<User> _userManager, IMailService mailService, IUserRoleManager userRoleManager, IRolesManager rolesManager, IJwtService jwtService, IUserManager userManager, ICabinetMedicalManager cabinetMedicalManager, IWorkDoctorManager workDoctorManager, IDoctorManager doctorManager, ISpecialitiesManager specialitiesManager)
        {
            this._userManager = _userManager;
            this.mailService = mailService;
            this.userRoleManager = userRoleManager;
            this.rolesManager = rolesManager;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.workDoctorManager = workDoctorManager;
            this.doctorManager = doctorManager;
            this.specialitiesManager = specialitiesManager;
        }
        public async Task<MessageResultDto> RegistreAccountAsync(RegistreAccountDto registreAccountDto) =>
            await TryCatch(async () =>
                 {
                     await ValidateUsenOnCreate(registreAccountDto);
                     var User = registreAccountDto.MaperToUser();
                     var identityResult = await this._userManager.CreateAsync(User, registreAccountDto.Password);

                     if (identityResult != null)
                     {
                         if (identityResult.Succeeded)
                         {
                             var UserAccount = await this._userManager.FindByEmailAsync(User.Email);
                             return await this.mailService.SendValidationMailToClient(UserAccount);
                         }
                         else
                         {
                             throw new FailedCreateUserException(nameof(User));
                         }
                     }
                     else
                     {
                         throw new FailedCreateUserException(nameof(User));
                     }


                 });
        private async Task ValidateUserAccountIsNotInSystem(string Email)
        {
            var UserAccount = await this._userManager.FindByEmailAsync(Email);
            var Exception = new Exception();

            if (!CheckUserIsNull(UserAccount))
            {
                throw new AlreadyExistsException(nameof(UserAccount));
            }
        }
        private async Task<IdentityResult> ValidateCompteUserService(User user, string Token)
        {
            try
            {
                return await this._userManager.ConfirmEmailAsync(user, Token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<MessageResultDto> ValidateAccountUserAsync(string Id, string Token)
        =>
            await TryCatch(async () =>
            {
                ValidateEntryConfirmeEmail(Id, Token);
                var User = await this._userManager.FindByIdAsync(Id);
                ValidateUserIsNull(User);
                var IdentityResult = await ValidateCompteUserService(User, Token);
                ValidateIdentityToken(IdentityResult);
                User.Status = UserStatus.Activated;
                await this.userManager.UpdateUser(User);
                var userRole = MaperToUserRole(User.Id, Guid.Parse("2B102F8F-079C-4AE1-B093-487BA70CF183"));
                await this.userRoleManager.InsertUserRoleAsync(userRole);
                return new MessageResultDto
                {
                    EmailAdress = User.Email,
                    Message = "Your Account has been Created Succeffely please retry Login "
                };






            });

        public async Task<JwtDto> AuthenticationAccountAsync(LoginAccountDto loginAccountDto)
        =>
           await TryCatch(async () =>
           {
               ValidateEntryOnLogin(loginAccountDto);
               var User = await this._userManager.FindByEmailAsync(loginAccountDto.Email);
               ValidateUserIsNull(User);
               var resultAuthentification = await this._userManager.CheckPasswordAsync(User, loginAccountDto.Password);
               ValidateAuthentificationPassword(resultAuthentification);
               var UserRoles = await this.userRoleManager.GetUserRolesById(User.Id);
               ValidateListUserRolesIsNull(UserRoles);
               var list = UserRoles.Cast<UserRole>().ToList();
               List<Role> listItem = new List<Role>();
               foreach (var Item in list)
               {
                   var role = await this.rolesManager.GetRolesById(Item.RoleId);
                   listItem.Add(role);
               }
               var Token = this.jwtService.Generitedtoken(User, listItem);
               var refreshToken = this.jwtService.GenerateTokenRefresh();
               DateTime DateExpire = DateTime.Now.AddDays(7);
               var NewUser = AppendRfToken(User, refreshToken, DateExpire);
               var IdentityResult = await this._userManager.UpdateAsync(NewUser);
               ValidateIdentityToken(IdentityResult);
               return MapperToJwtResult(Token, refreshToken);

           }
           );

        public async Task<List<DoctorSearchDto>> GetDoctorsAvailble()
        {

            List<DoctorSearchDto> ListdoctorSearchDtos = new List<DoctorSearchDto>();
            List<CabinetSearchDto> ListcabinetSearchDtos = new List<CabinetSearchDto>();
            List<string> ListSpecialities = new List<string>();
            var ListUsersDoctor = await this.userManager.SelectAllUsersDoctor();
            foreach (var userDoctor in ListUsersDoctor)
            {

                var Doctor = await this.doctorManager.SelectDoctorByIdUser(userDoctor.Id);
                //  ValidationDoctorIsNull(Doctor);
                if (Doctor != null)
                {
                    var ListJobsUser = await this.workDoctorManager.SelectWorksDoctorByIdDoctorActive(Doctor.Id);
                    foreach (var job in ListJobsUser)
                    {
                        var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(job.IdCabinet);
                        // ValidateCabinetMedicalIsNull(Cabinet);
                        if (Cabinet != null)
                        {
                            var JobSearchDto = MapperToJobSearchDto(job);
                            var CabinetSearchDto = MapperToCabinetSearch(JobSearchDto, Cabinet);
                            ListcabinetSearchDtos.Add(CabinetSearchDto);
                        }


                    }
                }

                if (ListcabinetSearchDtos.Count() != 0)
                {
                    var Sepecialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(Doctor.Id);
                    ListSpecialities = Sepecialities.Select(e => e.NameSpecialite).ToList();
                    var DoctorSearchDto = MapperToDoctorSearchDto(userDoctor, ListcabinetSearchDtos, ListSpecialities);
                    ListdoctorSearchDtos.Add(DoctorSearchDto);

                }

                ListcabinetSearchDtos = new List<CabinetSearchDto>();
                ListSpecialities = new List<string>();

            }
            return ListdoctorSearchDtos;
        }


    }


}
