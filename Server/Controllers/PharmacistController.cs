using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.PrescriptionService;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistController : ControllerBase
    {
        public readonly IPrescriptionService prescriptionService;

       public PharmacistController(IPrescriptionService prescriptionService)
        {
            this.prescriptionService = prescriptionService;
        }
        [HttpGet("GetInformationPrescription/{CodeQr}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PHARMACIEN")]
        public async Task<ActionResult<InformationPrescriptionResultDto>> GetInformationPrescription(string CodeQr)
        {
            try
            {
                CodeQr = CodeQr.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.prescriptionService.GetPrescriptionInformation(Email, CodeQr);
            }
             catch (ValidationException Ex)
            {
                return BadRequest(Ex.Message);

            }
            catch (ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch (StorageValidationException Ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
