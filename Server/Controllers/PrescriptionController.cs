using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.AnalyseMedicalService;
using Server.Services.Foundation.PrescriptionService;
using System.Security.Claims;

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
        [HttpGet("SecritaryDownloadFilePrescription/{OrdreMedicalId}/{CabinetId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE")]
        public async Task<IActionResult> SecritaryGetFilePrescription(string OrdreMedicalId,string CabinetId)
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                CabinetId = CabinetId.Replace("-", "/");
                OrdreMedicalId = OrdreMedicalId.Replace("-", "/");
                var FilePrescription = await this.PrescriptionService.GetFilePrescriptionByIdOrdreMedical(Email,OrdreMedicalId, CabinetId);
                using (MemoryStream pdfStream = new())
                {
                    pdfStream.Write(FilePrescription, 0, FilePrescription.Length);
                    pdfStream.Position = 0;
                    var result = File(pdfStream.ToArray(), "application/pdf", "PrescriptionFile.pdf");
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
        [HttpGet("DownloadFilePrescription/{OrdreMedicalId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFilePrescription(string OrdreMedicalId)
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                OrdreMedicalId = OrdreMedicalId.Replace("-", "/");
                var FilePrescription = await this.PrescriptionService.GetFilePrescriptionByIdOrdreMedical(Email, OrdreMedicalId);
                using (MemoryStream pdfStream = new())
                {
                    pdfStream.Write(FilePrescription, 0, FilePrescription.Length);
                    pdfStream.Position = 0;
                    var result = File(pdfStream.ToArray(), "application/pdf", "PrescriptionFile.pdf");
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
