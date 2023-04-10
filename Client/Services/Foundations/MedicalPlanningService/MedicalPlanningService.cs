using Client.Services.Exceptions;
using DTO;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Client.Services.Foundations.LocalStorageService;
using System.Net.Http.Json;

namespace Client.Services.Foundations.MedicalPlanningService
{


    public class MedicalPlanningService : IMedicalPlanningService
    {
        public readonly ILocalStorageServices localStorageServices;
        public HttpClient httpClient { get; set; }

        public MedicalPlanningService(HttpClient httpClient, ILocalStorageServices localStorageServices)
        {
            this.httpClient = httpClient;
            this.localStorageServices = localStorageServices;

        }
        public async Task<List<AppointmentInformationDto>> PostAppointmentInformationDto(KeysReservationMedicalDto keysReservationMedicalDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/MedicalPlanning");
            var keysReservation = JsonSerializer.Serialize(keysReservationMedicalDto);

            request.Content = new StringContent(keysReservation, Encoding.UTF8, "application/json");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<AppointmentInformationDto>>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Not Found Appoiment ");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("You Are not Authorize in this Action");
            }
            else if (result.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ProblemException("Youe information Not Authorize");
            }
            else if (result.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ProblemException("you have reservation in this cabinet");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }

        }

        public async Task<List<AppointmentInformationDto>> GetAppointmentInformationDto()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/MedicalPlanning");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<AppointmentInformationDto>>();
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
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task DeleteMedecalAppoiment(string IdMedicalAppoiment)
        {
            IdMedicalAppoiment = System.Web.HttpUtility.UrlEncode(IdMedicalAppoiment);
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/MedicalPlanning/DeleteMedicalAppoiment?IdMedicalAppoiment={IdMedicalAppoiment}");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = await this.httpClient.SendAsync(httpRequest);


            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new UnauthorizedException("precondition failed");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
