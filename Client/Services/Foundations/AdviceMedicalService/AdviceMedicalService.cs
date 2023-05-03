using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.Net.Http.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Client.Services.Foundations.AdviceMedicalService
{
    public class AdviceMedicalService : IAdviceMedicalService
    {
        public HttpClient HttpClient { get; set; }
        public ILocalStorageServices LocalStorageService { get; set; }  
        public AdviceMedicalService(HttpClient httpClient , ILocalStorageServices LocalStorageService)
        {
            HttpClient = httpClient;
            this.LocalStorageService = LocalStorageService;
        }
        public async  Task<List<AdviceMedicalDto>> GetAdvicesMedical(string OrdreMedicalId)
        {
            OrdreMedicalId = OrdreMedicalId.Replace("/", "-");

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/AdviceMedical/GetAdviceMedicalMessages/{OrdreMedicalId}");
            var JwtBearer = await this.LocalStorageService.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<AdviceMedicalDto>>();
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
                throw new BadRequestException("Request Denied");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task PatientPostNewAdviceMedicalPatient(MedicalAdviceToAddDto medicalAdviceToAddDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/AdviceMedical/PatientPostNewAdviceMedical");
            var MedicalAdviceToAdd = JsonSerializer.Serialize(medicalAdviceToAddDto);
            request.Content = new StringContent(MedicalAdviceToAdd, Encoding.UTF8, "application/json");
            var JwtBearer = await this.LocalStorageService.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
           if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("Condition User denied");
            }

            else if(result.StatusCode == HttpStatusCode.InternalServerError) 
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
