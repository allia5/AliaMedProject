using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.RadioMedicalService;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RadiologyController : ControllerBase
    {
        public readonly IRadioMedicalService radioMedicalService;
        public RadiologyController(IRadioMedicalService radioMedicalService)
        { 
            this.radioMedicalService = radioMedicalService;
        }
        [HttpGet("GetRadioInformation/{CodeQr}")]
         [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "RADIOLOGUE")]
        public async Task<ActionResult<InformationRadioResultDto>> GetInformationRadio(string CodeQr)
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.radioMedicalService.GetInformationRadioMedicalResult(Email, CodeQr);
            }
            catch(ValidationException Ex)
            {
                return BadRequest(Ex.Message);

            }catch(ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch (StorageValidationException Ex)
            {
                return NoContent();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
