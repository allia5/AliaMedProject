using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Exceptions;
using Server.Services.Foundation.CabinetMedicalService;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetMedicalController : ControllerBase
    {
        public ICabinetMedicalService CabinetMedicalService { get; set; }
        public CabinetMedicalController(ICabinetMedicalService CabinetMedicalService)
        {
            this.CabinetMedicalService = CabinetMedicalService;
        }
        [HttpGet("GetCabinetMedicalInformation")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<ActionResult<CabinetMedicalDto>> GetInformationCabinetMedical()
        {
            try
            {
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.CabinetMedicalService.SelectCabinetMedicalByAdmin(email);

            }
            catch (StorageValidationException Ex)
            {
                return NotFound(Ex.InnerException);
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (Exception Ex)
            {
                return Problem(Ex.Message);
            }
        }

        [HttpPatch("UpdateCabinetMedical")]

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<ActionResult<CabinetMedicalDto>> UpdateCabinet([FromBody] CabinetMedicalDto cabinetMedicalDto)
        {
            try
            {
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.CabinetMedicalService.UpdateCabinetMedicalService(email, cabinetMedicalDto);
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (StorageValidationException Ex)
            {
                return NoContent();
            }
            catch (Exception Ex)
            {
                return Problem(Ex.Message);
            }
        }

    }
}
