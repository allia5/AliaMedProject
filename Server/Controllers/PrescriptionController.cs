using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.AnalyseMedicalService;
using Server.Services.Foundation.PrescriptionService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        public readonly IPrescriptionService PrescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            PrescriptionService = prescriptionService;
        }
        [HttpGet("DownloadFilePrescription/{Id}")]
        public async Task<IActionResult> GetFilePrescription(string Id)
        {
            try
            {
                Id = Id.Replace("-", "/");
                var FilePrescription = await this.PrescriptionService.GetFilePrescriptionByIdOrdreMedical(Id);
                using (MemoryStream pdfStream = new())
                {
                    pdfStream.Write(FilePrescription, 0, FilePrescription.Length);
                    pdfStream.Position = 0;
                    var result = File(pdfStream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "sample.docx");
                    return result;
                }
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.Message);
            }
            catch (ServiceException Ex)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }
    }
}
