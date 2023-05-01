using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
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
        public async Task<Stream> GetMedicalFilePrescription(string OrdreId)
        {
            //OrdreId = System.Web.HttpUtility.UrlEncode(OrdreId);
            OrdreId = OrdreId.Replace("/", "-");
            var result = await this.httpClient.GetAsync($"/api/Prescription/DownloadFilePrescription/{OrdreId}");
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
