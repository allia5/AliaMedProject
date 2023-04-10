using DTO;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Http.Json;
using Client.Services.Foundations.LocalStorageService;
using Client.Services.Exceptions;

namespace Client.Services.Foundations.WorkDoctorService
{
    public class WorkDoctorService : IWorkDoctorService
    {
        public readonly HttpClient httpClient;
        public readonly ILocalStorageServices localStorageServices;
        public WorkDoctorService(ILocalStorageServices localStorageServices, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.localStorageServices = localStorageServices;
        }

        public async Task DeleteJobDoctorByAdmin(string IdJob)
        {
            IdJob = System.Web.HttpUtility.UrlEncode(IdJob);
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/WorkDoctor/deleteJobDoctor?IdJob={IdJob}");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = await this.httpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NoContentException("No delete data");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
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

        public async Task DeleteJobDoctorByDoctor(string IdJob)
        {
            IdJob = System.Web.HttpUtility.UrlEncode(IdJob);
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/WorkDoctor/deleteInvitationJobByDoctor?IdJob={IdJob}");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = await this.httpClient.SendAsync(httpRequest);


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

        public async Task<List<JobsDoctorDto>> GetJobsDoctorService()
        {

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/WorkDoctor/GetJobsDoctor");
            var jwtDto = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtDto.Token);
            var result = await this.httpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<JobsDoctorDto>>();
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
                throw new UnauthorizedException("User Not Authorized ");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }


        }

        public async Task<JobSettingDto> GetJobSetting(string IdJob)
        {
            string encodedIdJob = System.Web.HttpUtility.UrlEncode(IdJob);
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/WorkDoctor/GetJobSetting?IdJob={encodedIdJob}");
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            // var JsIdJob = JsonSerializer.Serialize(IdJob);
            // httpRequest.Content = new StringContent(JsIdJob, Encoding.UTF8, "application/json");
            var result = await httpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<JobSettingDto>();
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

        public async Task<List<DoctorCabinetDto>> GetListDoctorsInformationCabinet()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/WorkDoctor/GetInformationDoctorCabinet");
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = await this.httpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<DoctorCabinetDto>>();
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

        public async Task<List<InvitationsDoctorDto>> invitationsDoctorService()
        {
            var result = new HttpRequestMessage(HttpMethod.Get, "/api/WorkDoctor/GetListInvitationDoctor");
            var Jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            result.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt.Token);
            var Reponse = await httpClient.SendAsync(result);
            //await this.httpClient.GetAsync("/api/WorkDoctor/GetListInvitationDoctor");
            if (Reponse.StatusCode == HttpStatusCode.OK)
            {
                if (Reponse.Content.Headers.ContentLength != 0)
                {
                    return await Reponse.Content.ReadFromJsonAsync<List<InvitationsDoctorDto>>();
                }
                else
                {
                    throw new NullException("Empty Data");
                }
            }
            else if (Reponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Authorization Error");
            }
            else if (Reponse.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Validation Error");
            }
            else if (Reponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Not Found Data");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task SendInvitationWorkToDoctot(string IdUser)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/WorkDoctor/PostInvitationWorkDoctor");
            var JsCabinetMedical = JsonSerializer.Serialize(IdUser);
            var byteObject = System.Text.Encoding.UTF8.GetBytes(JsCabinetMedical);
            request.Content = new StringContent(JsCabinetMedical, Encoding.UTF8, "application/json");
            var JwtBearer = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await httpClient.SendAsync(request);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Error Data Source ");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
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

        public async Task UpdateJobSetting(JobSettingDto jobSettingDto)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, "/api/WorkDoctor/UpdateJobSetting");
            var JwtDto = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtDto.Token);
            var JsWorkDoctorDto = JsonSerializer.Serialize(jobSettingDto);
            // var byteObject = System.Text.Encoding.UTF8.GetBytes(JsWorkDoctorDto);
            httpRequest.Content = new StringContent(JsWorkDoctorDto, Encoding.UTF8, "application/json");
            var result = await httpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Error Data Source ");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
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

        public async Task UpdateStatusServiceWorkDoctor(UpdateStatusWorkDoctorDto updateStatusWorkDoctorDto)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, "/api/WorkDoctor/PatchStatusServiceWorkDoctor");
            var JwtDto = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtDto.Token);
            var JsWorkDoctorDto = JsonSerializer.Serialize(updateStatusWorkDoctorDto);
            // var byteObject = System.Text.Encoding.UTF8.GetBytes(JsWorkDoctorDto);
            httpRequest.Content = new StringContent(JsWorkDoctorDto, Encoding.UTF8, "application/json");
            var result = await httpClient.SendAsync(httpRequest);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Error Data Source ");
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
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


    }
}
