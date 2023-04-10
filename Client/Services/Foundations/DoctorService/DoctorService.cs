using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.Net;
using System.Net.Http.Json;

namespace Client.Services.Foundations.DoctorService
{
    public class DoctorService : IDoctorService
    {
        public HttpClient httpClient { get; set; }
        public readonly ILocalStorageServices LocalStorageServices;
        public DoctorService(HttpClient httpClient, ILocalStorageServices LocalStorageServices)
        {
            this.httpClient = httpClient;
            this.LocalStorageServices = LocalStorageServices;
        }
        public async Task<List<DoctorInformationDto>> GetListInformationDoctors()
        {
            var result = new HttpRequestMessage(HttpMethod.Get, "/api/Doctor/GetInformationDoctor");
            var Jwt = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            result.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt.Token);
            var Reponse = await this.httpClient.SendAsync(result);
            if (Reponse.StatusCode == HttpStatusCode.OK)
            {
                if (Reponse.Content.Headers.ContentLength != 0)
                {
                    return await Reponse.Content.ReadFromJsonAsync<List<DoctorInformationDto>>();
                }
                else
                {
                    throw new NullException("Not Have Any Data");
                }
            }
            else if (Reponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("List Doctor Is Empty");
            }
            else if (Reponse.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (Reponse.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("Can't specified Cabinet Information");
            }
            else if (Reponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("you are not authorize in this action");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }

        }
    }
}
