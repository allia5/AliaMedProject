using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.Net.Http.Json;
using System.Net;

namespace Client.Services.Foundations.LineAnalyseResultService
{
    
    public class LineAnalyseResultService : ILineAnalyseResultService
    {
        public HttpClient HttpClient { get; set; }
        public ILocalStorageServices LocalStorageService { get; set; }
        public LineAnalyseResultService(HttpClient HttpClient, ILocalStorageServices LocalStorageService)
        {
           this.HttpClient = HttpClient;
            this.LocalStorageService = LocalStorageService;
        }
        public async Task<FileResultDto> GetFileResultAnalyse(string IdAppointment, string IdLineAnalyse)
        {
            IdAppointment = IdAppointment.Replace("/", "-");
            IdLineAnalyse = IdLineAnalyse.Replace("/", "-");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/ResultLineAnalyse/GetResultFileAnalyse/{IdAppointment}/{IdLineAnalyse}");
            var JwtBearer = await this.LocalStorageService.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<FileResultDto>();
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
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Ressource Not Found");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async  Task<FileResultDto> GetFileResultAnalysePatient(string IdLineAnalyse)
        {
          
            IdLineAnalyse = IdLineAnalyse.Replace("/", "-");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/ResultLineAnalyse/GetResultFileAnalysePatient/{IdLineAnalyse}");
            var JwtBearer = await this.LocalStorageService.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<FileResultDto>();
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
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Ressource Not Found");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }
    }
}
