using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Foundation.AnalyseMedicalService;
using Server.Services.Foundation.RadioMedicalService;
using System.Security.Claims;

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
        [HttpGet("SecritaryDownloadFileRadio/{OrdreMedcialId}/{CabinetId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE")]
        public async Task<IActionResult> GetFileRadio(string OrdreMedcialId,string CabinetId)
        {
            CabinetId = CabinetId.Replace("-", "/");
            OrdreMedcialId = OrdreMedcialId.Replace("-", "/");
            var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var FileRadio = await this.RadioMedicalService.SecritaryGetFileRadioByIdOrdreMedical(Email,OrdreMedcialId,CabinetId);
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
