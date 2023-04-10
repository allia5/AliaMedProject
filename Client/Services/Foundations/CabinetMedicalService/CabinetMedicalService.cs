using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace Client.Services.Foundations.CabinetMedicalService
{
    public class CabinetMedicalService : ICabinetMedicalService
    {
        public HttpClient HttpClient { get; set; }
        public ILocalStorageServices localStorageServices { get; set; }
        public CabinetMedicalService(HttpClient HttpClient, ILocalStorageServices localStorageServices)
        {
            this.HttpClient = HttpClient;
            this.localStorageServices = localStorageServices;
        }
        public async Task<CabinetMedicalDto> GetInformationFromCabinetMedical()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/CabinetMedical/GetCabinetMedicalInformation");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<CabinetMedicalDto>();
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
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Data Not Found");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("User Not Authorized in This Action");
            }
            else
            {
                throw new ProblemException("Erro Intern");
            }

        }

        public async Task<CabinetMedicalDto> UpdateInformationCabinetMedical(CabinetMedicalDto cabinetMedicalDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "/api/CabinetMedical/UpdateCabinetMedical");
            var JsCabinetMedical = JsonSerializer.Serialize(cabinetMedicalDto);
            var byteObject = System.Text.Encoding.UTF8.GetBytes(JsCabinetMedical);
            request.Content = new StringContent(JsCabinetMedical, Encoding.UTF8, "application/json");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result?.Content.ReadFromJsonAsync<CabinetMedicalDto>();
                }
                else
                {
                    throw new NullException("Data Is Empty");
                }
            }
            else if (result.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("Data Not Availble");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("User Not Authorized in This Action");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ProblemException("Error Intern");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Data Error");
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
