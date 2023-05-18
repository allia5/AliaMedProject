using DTO;
using Microsoft.AspNetCore.Identity;

namespace Server.Services.UserService
{
    public interface IUserService
    {
        public Task ForgotPasswordUserAccount(string Email);
        public Task ResetPasswordUserAccount(ResetPasswordUserAccountDto ResetPasswordUserAccountDto);
        public Task<MessageResultDto> RegistreAccountAsync(RegistreAccountDto registreAccountDto);
        public Task<MessageResultDto> ValidateAccountUserAsync(string Id, string Token);

        public Task<JwtDto> AuthenticationAccountAsync(LoginAccountDto loginAccountDto);
        public Task<List<DoctorSearchDto>> GetDoctorsAvailble();


    }
}
