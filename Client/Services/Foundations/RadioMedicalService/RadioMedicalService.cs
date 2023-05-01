using Client.Services.Exceptions;
using Client.Services.Foundations.LocalStorageService;
using System.Net;

namespace Client.Services.Foundations.RadioMedicalService
{
    public class RadioMedicalService :IRadioMedicalService
    {
        public HttpClient httpClient { get; set; }
        public ILocalStorageServices localStorageServices { get; set; }
        public RadioMedicalService(ILocalStorageServices localStorageServices, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.localStorageServices = localStorageServices;
            }
    public async Task<Stream> GetMedicalFileRadio(string OrdreId)
        {
            OrdreId = OrdreId.Replace("/", "-");
            var result = await this.httpClient.GetAsync($"/api/RadioMedical/DownloadFileRadio/{OrdreId}");
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
