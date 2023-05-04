using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.AdviceMedicalService;
using System.Security.Claims;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdviceMedicalController : ControllerBase
    {
        public readonly IAdviceMedicalService adviceMedicalService;
        public AdviceMedicalController(IAdviceMedicalService adviceMedicalService)
        {
          this.adviceMedicalService = adviceMedicalService;
        }
        [HttpPost("DoctorPostNewAdviceMedical")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult> PatientPostAdviceMedical([FromBody] MedicalAdviceToAddDto medicalAdviceToAddDto)
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.adviceMedicalService.PostNewAdviceMedicalDoctor(Email, medicalAdviceToAddDto);
                return Ok();
            }
            catch (ValidationException Ex)
            {
                return BadRequest();

            }
            catch (ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
        [HttpPost("PatientPostNewAdviceMedical")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]
        public async Task<ActionResult> DoctorPostAdviceMedical([FromBody] MedicalAdviceToAddDto medicalAdviceToAddDto)
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.adviceMedicalService.PostNewAdviceMedicalPatient(Email, medicalAdviceToAddDto);
                return Ok();
            }
            catch(ValidationException Ex)
            {
                return BadRequest();

            }
            catch(ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch(Exception ex)
            {
                return Problem();
            }
        }
        [HttpGet("GetAdviceMedicalMessages/{OrdreMedicalId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]
        public async Task<ActionResult<List<AdviceMedicalDto>>> PatientGetAdvicesMedical(string OrdreMedicalId)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                OrdreMedicalId = OrdreMedicalId.Replace("-","/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var result=await this.adviceMedicalService.PatientGetAdviceMedical(Email, OrdreMedicalId);
                transaction.Complete();
                return result;
            }catch(ValidationException Ex)
            {
                return BadRequest(Ex.Message); 
            }
            catch (ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            finally
            {
                transaction.Dispose();
            }
        }
        [HttpGet("DoctorGetAdviceMedicalMessages/{OrdreMedicalId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<List<AdviceMedicalDto>>> DoctorGetAdvicesMedical(string OrdreMedicalId)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                OrdreMedicalId = OrdreMedicalId.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var result = await this.adviceMedicalService.DoctorGetAdviceMedical(Email, OrdreMedicalId);
                transaction.Complete();
                return result;
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.Message);
            }
            catch (ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
