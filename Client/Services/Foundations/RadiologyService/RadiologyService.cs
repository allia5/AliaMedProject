using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.Net;
using System.Net.Http.Json;

namespace Client.Services.Foundations.RadiologyService
{
    public class RadiologyService : IRadiologyService
    {
        public readonly HttpClient httpClient;
        public readonly ILocalStorageServices localStorageServices;
        public RadiologyService(HttpClient httpClient, ILocalStorageServices localStorageServices)
        {
            this.httpClient = httpClient;
            this.localStorageServices = localStorageServices;
        }
        public async Task<InformationRadioResultDto> GetInformationRadioResultAsync(string CodeQr)
        {
           CodeQr= CodeQr.Replace("/", "-");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Radiology/GetRadioInformation/{CodeQr}");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<InformationRadioResultDto>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("Denied User Account");
            }else if (result.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("Data Has Been Canfirmed By Auther Radiology");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
