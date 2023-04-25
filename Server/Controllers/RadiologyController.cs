using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.RadioMedicalService;
using Server.Services.Foundation.ResultRadioService;
using System.Security.Claims;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RadiologyController : ControllerBase
    {
        public readonly IRadioMedicalService radioMedicalService;
        public readonly IResultRadioService resultRadioService;
        public RadiologyController(IRadioMedicalService radioMedicalService, IResultRadioService resultRadioService)
        { 
            this.radioMedicalService = radioMedicalService;
            this.resultRadioService = resultRadioService;
        }

        [HttpPost("PostRadioResult")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "RADIOLOGUE")]
        public async Task<ActionResult> PostNewRadioMedicalResult( RadioResultToAddDto radioResultToAddDto)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.resultRadioService.AddRadioResultService(Email, radioResultToAddDto);
                transaction.Complete();
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest();
            }
            catch (ServiceException ex)
            {
                return StatusCode(412);
            }catch(StorageValidationException ex)
            {
                return NoContent();
            }catch(Exception e)
            {
                return Problem();
            }
            finally
            {
                   
                transaction.Dispose();
               
            }
        }

        [HttpGet("GetRadioInformation/{CodeQr}")]
         [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "RADIOLOGUE")]
        public async Task<ActionResult<InformationRadioResultDto>> GetInformationRadio(string CodeQr)
        {
            try
            {
                CodeQr = CodeQr.Replace("-", "/");
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
