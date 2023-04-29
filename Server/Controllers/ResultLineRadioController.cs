using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.ResultRadioService;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultLineRadioController : ControllerBase
    {
        public readonly IResultRadioService resultRadioService;
        public ResultLineRadioController(IResultRadioService resultRadioService)
        {
            this.resultRadioService = resultRadioService;
        }
        [HttpGet("GetResultFileRadio/{AppointmentId}/{LineRadioId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<FileResultDto>> GetFileResultRadio(string AppointmentId,string LineRadioId)
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.resultRadioService.GetFileResultRadio(Email, AppointmentId, LineRadioId);
            }
            catch (ValidationException Ex)
            {
                return BadRequest();
            }
            catch (StorageValidationException Ex)
            {
                return NotFound();
            }
            catch (ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
