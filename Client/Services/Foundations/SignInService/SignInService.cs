using Client.Services.Exceptions;
using Client.Services.Foundations.Utility;
using DTO;
using System.Net;
using System.Net.Http.Json;

namespace Client.Services.Foundations.SignInService
{
    public class SignInService : ISignInService
    {
        public readonly HttpClient httpClient;
        public SignInService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<MessageResultDto> SignInAsync(RegistreAccountDto registreAccountDto)
        {

            var result = await this.httpClient.PostAsJsonAsync<RegistreAccountDto>("/api/UserAccount/CreateUserAccount", registreAccountDto);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<MessageResultDto>();
                }
                else
                {
                    return null;
                }

            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Invalid Request");
            }
            else if (result.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ConflictException("User Already Exist in System ");

            }
            else
            {
                throw new Exception("he Is Problem Intern");
            }

        }

        public async Task<MessageResultDto> ValidateAccountService(string id, string Token)
        {
            Token = Token.ConvertToken();
            var result = await this.httpClient.GetAsync($"/api/UserAccount/ValidatAccount?uid={id}&token={Token}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<MessageResultDto>();
                }
                else
                {
                    throw new NullException("Empty Result");
                }
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Token Is Not Valide");

            }
            else
            {
                throw new Exception("Existing Error");
            }
        }
    }
}
