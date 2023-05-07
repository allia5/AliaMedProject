using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.AnalyseMedicalService;
using System.Security.Claims;

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
      

       

        [HttpGet("SecritaryDownloadFileAnalyse/{OrdreMedicalId}/{CabinetId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE")]
        public async Task<IActionResult> GetFileAnalyse(string OrdreMedicalId,string CabinetId)
        {
            CabinetId = CabinetId.Replace("-", "/");
            OrdreMedicalId = OrdreMedicalId.Replace("-", "/");
            var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var FileAnalyse = await this.AnalyseMedicalService.SecritaryGetFileAnalyseByIdOrdreMedical(Email,OrdreMedicalId,CabinetId);
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
