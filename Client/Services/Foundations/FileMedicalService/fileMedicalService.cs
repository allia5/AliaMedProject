using Client.Services.Exceptions;
using DTO;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Client.Services.Foundations.LocalStorageService;
using System.Net.Http.Json;

namespace Client.Services.Foundations.FileMedicalService
{
    public class fileMedicalService : IfileMedicalService
    {
        public HttpClient httpClient { get; set; }
        public ILocalStorageServices localStorageServices { get; set; }
        public fileMedicalService(HttpClient httpClient, ILocalStorageServices localStorageServices)
        {
            this.httpClient = httpClient;
            this.localStorageServices = localStorageServices;
        }

    








    public async Task<FileMedicalPatientDto> PostFileMedicalPatientAsync(FileMedicalToAddDto fileMedicalToAdd)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/FileMedical/PostNewFileMedical");
            var keysReservation = JsonSerializer.Serialize(fileMedicalToAdd);
            
            request.Content = new StringContent(keysReservation, Encoding.UTF8, "application/json");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<FileMedicalPatientDto>();
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
            }else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("Condition User denied");
            }
           
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task<FileMedicalMainPatientDto> GetAllFileMedicalMainPatient(string IdAppointment)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/FileMedical/GetFilesPatient/{IdAppointment}");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<FileMedicalMainPatientDto>();
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
                throw new PreconditionFailedException("Condition User denied");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task UpdateFileMedicalPatient(UpdateFileMedicalDto updateFileMedicalDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "/api/FileMedical/PatchFileMedical");
            var keysReservation = JsonSerializer.Serialize(updateFileMedicalDto);

            request.Content = new StringContent(keysReservation, Encoding.UTF8, "application/json");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);

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

        public async Task<Stream> GetMedicalFilePrescription(string OrdreId)
        {
            //OrdreId = System.Web.HttpUtility.UrlEncode(OrdreId);
            OrdreId = OrdreId.Replace("/", "-");
            var result = await this.httpClient.GetAsync($"/api/FileMedical/DownloadFilePrescription/{OrdreId}");
            if(result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadAsStreamAsync();
            }else if(result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("File Not Found");
            }else if(result.StatusCode==HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Unauthorize DownloadFile");
            }else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Bade Request");
            }
            else 
            {
                throw new ProblemException("intern Exception");
            }
        }

        public async Task<Stream> GetMedicalFileRadio(string OrdreId)
        {
            OrdreId = OrdreId.Replace("/", "-");
            var result = await this.httpClient.GetAsync($"/api/FileMedical/DownloadFileRadio/{OrdreId}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadAsStreamAsync();
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("File Not Found");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Unauthorize DownloadFile");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Bade Request");
            }
            else
            {
                throw new ProblemException("intern Exception");
            }
        }

        public async  Task<Stream> GetMedicalFileAnalyse(string OrdreId)
        {
            OrdreId = OrdreId.Replace("/", "-");
           var result = await this.httpClient.GetAsync($"/api/FileMedical/DownloadFileAnalyse/{OrdreId}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadAsStreamAsync();
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("File Not Found");
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Unauthorize DownloadFile");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Bade Request");
            }
            else
            {
                throw new ProblemException("intern Exception");
            }
        }

        public async Task<List<FileMedicalPatientDto>> GetFilePatient()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/FileMedical/GetMedicalFilePatient");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<FileMedicalPatientDto>>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("failed Precodition Accec to ressource");
            }
            else
            {
                throw new ProblemException("Error intern");
            }
        }
    }
}
