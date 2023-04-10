using DTO;

namespace Client.Services.Foundations.SignInService
{
    public interface ISignInService
    {
        public Task<MessageResultDto> SignInAsync(RegistreAccountDto registreAccountDto);
        public Task<MessageResultDto> ValidateAccountService(string id, string Token);
    }
}
