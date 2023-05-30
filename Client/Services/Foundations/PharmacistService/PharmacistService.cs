using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.Net.Http.Json;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace Client.Services.Foundations.PharmacistService
{
    public class PharmacistService :IPharmacistService
    {
        public HttpClient httpClient { get; set; }
        public ILocalStorageServices localStorageServices { get; set; }
        public PharmacistService(HttpClient httpClient, ILocalStorageServices localStorageServices)
        {
            this.httpClient = httpClient;
            this.localStorageServices = localStorageServices;
        }

        public async Task<InformationPrescriptionResultDto> GetPrescriptionInformation(string CodeQr)
        {
            CodeQr = CodeQr.Replace("/", "-");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Pharmacist/GetInformationPrescription/{CodeQr}");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<InformationPrescriptionResultDto>();
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
            }
            else if (result.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("Error requesting the document");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task UpdateStatusPrescriptionLine(string PrescriptionLineId)
        {
            var HttpRequest = new HttpRequestMessage(HttpMethod.Patch, "/api/Pharmacist/PatchStatusPrescription");
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            HttpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            var json = JsonConvert.SerializeObject(PrescriptionLineId);
            HttpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await this.httpClient.SendAsync(HttpRequest);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
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
            }
            else if (result.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("Data Has Been Canfirmed By Auther Radiology");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
