using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.Text;
using System.Text.Json;
using System.Net;
using Client.Services.Exceptions;
using System.Net.Http.Json;

namespace Client.Services.Foundations.SecretaryService
{
    public class SercretaryService : ISercretaryService
    {
        public HttpClient HttpClient { get; set; }
        public ILocalStorageServices LocalStorageServices { get; set; }
        public SercretaryService(HttpClient HttpClient, ILocalStorageServices LocalStorageServices)
        {
            this.HttpClient = HttpClient;
            this.LocalStorageServices = LocalStorageServices;

        }

        public async Task<SecritaryDto> AddSecretary(string Email)
        {
            var Jwt = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Secretary/AddSecretary");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt.Token);
            var JsEmail = JsonSerializer.Serialize(Email);
            httpRequest.Content = new StringContent(JsEmail, Encoding.UTF8, "application/json");
            var result = await this.HttpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<SecritaryDto>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ConflictException("User Hase Been Role Secretary");
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("User Not Found");
            }
            else
            {
                throw new ProblemException("Intern Problem");
            }

        }

        public async Task<List<SecritaryDto>> GetAllSecretary()
        {
            var Jwt = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Secretary/GetAllSecretary");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt.Token);

            var result = await this.HttpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<SecritaryDto>>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }

        }

        public async Task UpdateStatusSecretary(UpdateStatusSecretaryDto updateStatusSecretaryDto)
        {
            var Jwt = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, "/api/Secretary/UpdateStatusSecretary");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt.Token);
            var JsEmail = JsonSerializer.Serialize(updateStatusSecretaryDto);
            httpRequest.Content = new StringContent(JsEmail, Encoding.UTF8, "application/json");
            var result = await this.HttpClient.SendAsync(httpRequest);


            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task<List<SecretaryCabinetInformationDto>> GetAllCabinetSecretaryInformation()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Secretary/GetInformationCabinetAppoiment");
            var JwtBearer = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<SecretaryCabinetInformationDto>>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }

            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are Not Authorize In this Action");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Request Is Invalide");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
