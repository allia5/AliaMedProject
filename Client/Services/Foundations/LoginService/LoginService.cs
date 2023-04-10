using DTO;
using System.Net.Http.Json;
using System.Net;
using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using System.Net.Http;

namespace Client.Services.Foundations.LoginService
{
    public class LoginService : ILoginService
    {
        private HttpClient HttpClient { get; set; }
        private ILocalStorageServices localStorageService { get; set; }
        public LoginService(HttpClient httpClient, ILocalStorageServices localStorageService)
        {
            this.HttpClient = httpClient;
            this.localStorageService = localStorageService;
        }
        public async Task AuthentificationAccount(LoginAccountDto loginAccountDto)
        {
            var result = await this.HttpClient.PostAsJsonAsync<LoginAccountDto>("/api/UserAccount/LoginAccount", loginAccountDto);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    var jwt = await result.Content.ReadFromJsonAsync<JwtDto>();
                    await this.localStorageService.SetItemAsync("JwtLocalStorage", jwt);
                }

            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Authentification Is Invalid");
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("User Not Found iN System");
            }
            else
            {

            }
        }

        public async Task AuthentificationState(JwtDto jwtDto)
        {
          
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/UserAccount/AuthenticatedState");

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtDto.Token);
                var result = await HttpClient.SendAsync(request);
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException("Account Not Authonticated in System");
                }
           


        }

        public async Task<JwtDto> CorrectEntryToken(JwtDto jwtDto)
        {
            var result = await this.HttpClient.PostAsJsonAsync<JwtDto>("/api/UserAccount/TokenAlive", jwtDto);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<JwtDto>();
                }
                else
                {
                    throw new NullException("Jwt Empty Result");
                }
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {

                throw new ProblemException("Error intern Server");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Token Is Not Correct");
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
