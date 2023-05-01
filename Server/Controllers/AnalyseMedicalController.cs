using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.AnalyseMedicalService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyseMedicalController : ControllerBase
    {


        public readonly IAnalyseMedicalService AnalyseMedicalService;
        public AnalyseMedicalController(IAnalyseMedicalService AnalyseMedicalService)
        {
            this.AnalyseMedicalService = AnalyseMedicalService;
        }
      

       

        [HttpGet("DownloadFileAnalyse/{Id}")]
        public async Task<IActionResult> GetFileAnalyse(string Id)
        {
            Id = Id.Replace("-", "/");
            var FileAnalyse = await this.AnalyseMedicalService.GetFileAnalyseByIdOrdreMedical(Id);
            using (MemoryStream pdfStream = new())
            {
                pdfStream.Write(FileAnalyse, 0, FileAnalyse.Length);
                pdfStream.Position = 0;
                var result = File(pdfStream.ToArray(), "application/pdf", "sample.pdf");
                return result;
            }
        }
    }
}
