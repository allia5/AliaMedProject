using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Foundation.AnalyseMedicalService;
using Server.Services.Foundation.RadioMedicalService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RadioMedicalController : ControllerBase
    {
        public readonly IRadioMedicalService RadioMedicalService;
        public RadioMedicalController(IRadioMedicalService RadioMedicalService)
        {
            this.RadioMedicalService = RadioMedicalService;
        }
        [HttpGet("DownloadFileRadio/{Id}")]
        public async Task<IActionResult> GetFileRadio(string Id)
        {
            Id = Id.Replace("-", "/");
            var FileRadio = await this.RadioMedicalService.GetFileRadioByIdOrdreMedical(Id);
            using (MemoryStream pdfStream = new())
            {
                pdfStream.Write(FileRadio, 0, FileRadio.Length);
                pdfStream.Position = 0;
                var result = File(pdfStream.ToArray(), "application/pdf", "sample.pdf");
                return result;
            }
        }

    }
}
