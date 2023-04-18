using Client.Services.Exceptions;
using DTO;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Client.Services.Foundations.LocalStorageService;
using System.Net.Http.Json;

namespace Client.Services.Foundations.OrdreMedicalService
{
    public class OrdreMedicalService : IOrdreMedicalService
    {
        public HttpClient HttpClient { get; set; }
        public ILocalStorageServices LocalStorageServices { get; set; } 
        public OrdreMedicalService(ILocalStorageServices LocalStorageServices, HttpClient HttpClient)
        {
            this.HttpClient=HttpClient;
            this.LocalStorageServices=LocalStorageServices;
        }
        public async Task<OrdreMedicalDto> PostOrdreMedicalPatient(OrderMedicalToAddDro orderMedicalToAddDro)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/OrdreMedical/PostNewOrdreMedical");
            var keysReservation = JsonSerializer.Serialize(orderMedicalToAddDro);

            request.Content = new StringContent(keysReservation, Encoding.UTF8, "application/json");
            var JwtBearer = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<OrdreMedicalDto>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }

            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Data Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("Condition User denied");
            }

            else
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
