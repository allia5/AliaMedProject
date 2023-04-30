using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Managers.Storages.LineAnalyseMedicalManager;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.ResultAnalyseService;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultLineAnalyseController : ControllerBase
    {
        public readonly IResultAnalyseService resultAnalyseService;
        public ResultLineAnalyseController(IResultAnalyseService resultAnalyseService)
        {
            this.resultAnalyseService = resultAnalyseService;
        }
        [HttpGet("GetResultFileAnalyse/{AppointmentId}/{LineAnalyseId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<FileResultDto>> GetFileResultAnalyse(string AppointmentId, string LineAnalyseId)
        {
            try
            {
                AppointmentId = AppointmentId.Replace("-", "/");
                LineAnalyseId = LineAnalyseId.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.resultAnalyseService.GetFileResultAnalyse(Email,AppointmentId,LineAnalyseId);
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
        [HttpGet("GetResultFileAnalysePatient/{LineAnalyseId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]
        public async Task<ActionResult<FileResultDto>> GetFileResultAnalysePatient(string LineAnalyseId)
        {
            try
            {
          
                LineAnalyseId = LineAnalyseId.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.resultAnalyseService.GetFileResultAnalysePatient(Email, LineAnalyseId);
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
