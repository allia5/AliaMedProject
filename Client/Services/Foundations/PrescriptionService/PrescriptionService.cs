using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Client.Services.Foundations.PrescriptionService
{
    public class PrescriptionService : IPrescriptionService
    {
        public HttpClient httpClient { get; set; }
        public ILocalStorageServices localStorageServices { get; set; }
        public PrescriptionService(ILocalStorageServices localStorageServices, HttpClient httpClient)
        {
            this.localStorageServices = localStorageServices;
            this.httpClient = httpClient;
            }
        public async Task<Stream> SecritaryGetMedicalFilePrescription(string OrdreId,string CabinetId)
        {
            //OrdreId = System.Web.HttpUtility.UrlEncode(OrdreId);
            OrdreId = OrdreId.Replace("/", "-");
            CabinetId = CabinetId.Replace("/", "-");
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var requestHttp = new HttpRequestMessage(HttpMethod.Get, $"/api/Prescription/SecritaryDownloadFilePrescription/{OrdreId}/{CabinetId}");
            requestHttp.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = await this.httpClient.SendAsync(requestHttp);
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

        public async Task<Stream> PatientGetMedicalFilePrescription(string OrdreId)
        {
            OrdreId = OrdreId.Replace("/", "-");
           
            var jwt = await this.localStorageServices.GetItemAsync<JwtDto>("JwtLocalStorage");
            var requestHttp = new HttpRequestMessage(HttpMethod.Get, $"/api/Prescription/DownloadFilePrescription/{OrdreId}");
            requestHttp.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = await this.httpClient.SendAsync(requestHttp);
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
    }
}
