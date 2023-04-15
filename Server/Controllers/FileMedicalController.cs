using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.FileMedicalService;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileMedicalController : ControllerBase
    {
        public readonly IFileMedicalService FileMedicalService;
        public FileMedicalController(IFileMedicalService fileMedicalService)
        {
            this.FileMedicalService = fileMedicalService;
        }
        [HttpPost("PostNewFileMedical")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<FileMedicalPatientDto>> PostNewFileMedicalPatient([FromBody] FileMedicalToAddDto fileMedicalToAdd)
        {
            try
            {
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.FileMedicalService.AddNewFileMedicalPatient(email,fileMedicalToAdd);
            }
            catch (ValidationException Ex)
            {
                return StatusCode(412);
            }
            catch (ServiceException Ex)
            {
                return BadRequest(Ex.TargetSite);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
