using DTO;

namespace Client.Services.Foundations.LoginService
{
    public interface ILoginService
    {
        public Task AuthentificationAccount(LoginAccountDto loginAccountDto);
        public Task AuthentificationState(JwtDto jwtDto);
        public Task<JwtDto> CorrectEntryToken(JwtDto jwtDto);



    }
}
