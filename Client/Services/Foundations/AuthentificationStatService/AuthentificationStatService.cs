using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using Client.Services.Foundations.LoginService;
using DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Services.Foundations.AuthentificationStatService
{

    public partial class AuthentificationStatService : AuthenticationStateProvider
    {
        public ILocalStorageServices localStorageServices { get; set; }
        public readonly ILoginService loginService;

        public AuthentificationStatService(ILoginService loginService, ILocalStorageServices localStorageServices)
        {
            this.loginService = loginService;
            this.localStorageServices = localStorageServices;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymousIdentity = new ClaimsIdentity();

            var JwtLocalStorage = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            try
            {
                ValidationLocalStorageIsNull(JwtLocalStorage);
                await this.loginService.AuthentificationState(JwtLocalStorage);
                var claims = GetClaimsFromToken(JwtLocalStorage.Token);
                var Identity = new ClaimsIdentity(claims, "testAuthType");
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(Identity)));

            }
            catch (UnauthorizedException Ex)
            {
                try
                {
                    var newJwt = await this.loginService.CorrectEntryToken(JwtLocalStorage);
                    await this.localStorageServices.SetItemAsync<JwtDto>("JwtLocalStorage", newJwt);
                    var claims = GetClaimsFromToken(newJwt.Token);
                    var Identity = new ClaimsIdentity(claims, "testAuthType");
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(Identity)));
                }
                catch (UnauthorizedException ExAuth)
                {
                    return new AuthenticationState(new ClaimsPrincipal(anonymousIdentity));
                }
                catch (Exception Exx)
                {
                    return new AuthenticationState(new ClaimsPrincipal(anonymousIdentity));
                }
            }
            catch (NullException Ex)
            {
                return new AuthenticationState(new ClaimsPrincipal(anonymousIdentity));
            }
        }
        public List<Claim> GetClaimsFromToken(string token)
        {


            var jwtHandler = new JwtSecurityTokenHandler();

            var jwtToken = jwtHandler.ReadJwtToken(token);
            var ClaimsListe = jwtToken.Claims.ToList();

            return ClaimsListe;



        }
    }
}
