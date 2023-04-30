using Client.Services.Exceptions;
using DTO;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Client.Services.Foundations.LocalStorageService;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

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

        public async Task UpdateStatusOrdreMedicalBySecritary(UpdateOrdreMedicalDto updateOrdreMedicalDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "/api/OrdreMedical/PatchMedicalOrdre");
            var JsCabinetMedical = System.Text.Json.JsonSerializer.Serialize(updateOrdreMedicalDto);

            request.Content = new StringContent(JsCabinetMedical, Encoding.UTF8, "application/json");
            var JwtBearer = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
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
            }else if (result.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreconditionFailedException("User denied Action");
            }
        }



        public async Task<List<InformationOrderMedicalSecritary>> GetAllOrdreMedicalSecritary(KeysAppoimentInformationSecretary keysAppoimentInformationSecretary)
        {
            var DateAppoiment = System.Web.HttpUtility.UrlEncode(keysAppoimentInformationSecretary.DateAppoiment.Date.ToString());
            var DoctorId = System.Web.HttpUtility.UrlEncode(keysAppoimentInformationSecretary.IdDoctor);
            var CabinetId = System.Web.HttpUtility.UrlEncode(keysAppoimentInformationSecretary.CabinetId);
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/OrdreMedical/GetAllMedicalOrdreSecritary/{CabinetId}/{DoctorId}/{DateAppoiment}");
            var JwtBearer = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<InformationOrderMedicalSecritary>>();
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
                throw new BadRequestException("Your Role Secretary Is Denied");
            }
            else
            {
                throw new ProblemException("Error Intern");
            }
        }





        public async Task PostOrdreMedicalPatient(OrderMedicalToAddDro orderMedicalToAddDro)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/OrdreMedical");
            var prescriptionObject = new object();
            var RadioObject = new object();
            var AnalyseObject = new object();
            if (orderMedicalToAddDro.Prescription != null)
            {
                var FilePrescriptionbase64String = Convert.ToBase64String(orderMedicalToAddDro.Prescription.PrescriptionFile);
                prescriptionObject = new
                {
                    prescriptionFile = FilePrescriptionbase64String,
                    instruction = orderMedicalToAddDro.Prescription.Instruction,
                    prescriptionLines = orderMedicalToAddDro.Prescription.prescriptionLines
                };
            }
            if(orderMedicalToAddDro.RadioToAdd != null)
            {
                var FileRadiobase64String = Convert.ToBase64String(orderMedicalToAddDro.RadioToAdd.FileMedicalRadio);
                RadioObject = new
                {
                    fileMedicalRadio = FileRadiobase64String,
                    LineRadioMedicals=orderMedicalToAddDro.RadioToAdd.LineRadioMedicals
                };
            }
            if (orderMedicalToAddDro.AnalyseToAdd != null)
            {
                var FileAnalysebase64String = Convert.ToBase64String(orderMedicalToAddDro.AnalyseToAdd.FileMedicalAnalyse);
                AnalyseObject = new
                {
                    fileMedicalAnalyse = FileAnalysebase64String,
                    LineAnalyseMedicals=orderMedicalToAddDro.AnalyseToAdd.LineAnalyseMedicals
                };
            }


           

            var jsonObject = new
            {
                appointmentId = orderMedicalToAddDro.AppointmentId,
                fileId = orderMedicalToAddDro.FileId,
                summary = orderMedicalToAddDro.Summary,
                visibility = orderMedicalToAddDro.Visibility,
                analyseToAdd = orderMedicalToAddDro.AnalyseToAdd !=null ?AnalyseObject:null,
                radioToAdd = orderMedicalToAddDro.RadioToAdd !=null ?RadioObject:null,
                prescription = orderMedicalToAddDro.Prescription != null ? prescriptionObject : null
            };

            var json = JsonConvert.SerializeObject(jsonObject);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var jwtBearer = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtBearer.Token);
            var result = await HttpClient.SendAsync(request);

            

             if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Bad Request Check Field");
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

        public async Task<MedicalFileArchiveDto> GetMedicalFileArchive(string FileId, string AppointmentId)
        {

            FileId = FileId.Replace("/", "-");
            AppointmentId = AppointmentId.Replace("/", "-");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/OrdreMedical/GetAllOrdreArchiveFileMedcial/{AppointmentId}/{FileId}");
            var JwtBearer = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<MedicalFileArchiveDto>();
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

        public async Task<MedicalFileArchiveDto> GetMedicalFileArchivePatient(string FileId)
        {
            FileId = FileId.Replace("/", "-");
          
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/OrdreMedical/GetAllOrdreArchiveFileMedcialPatient/{FileId}");
            var JwtBearer = await this.LocalStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtBearer.Token);
            var result = await HttpClient.SendAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<MedicalFileArchiveDto>();
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
    }
}
