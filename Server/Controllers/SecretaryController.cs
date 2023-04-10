using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.SecretaryService;
using System.Security.Claims;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretaryController : ControllerBase
    {
        private readonly ISecretaryService secretaryService;
        public SecretaryController(ISecretaryService secretaryService)
        {
            this.secretaryService = secretaryService;
        }

        [HttpGet("GetInformationCabinetAppoiment")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE")]
        public async Task<ActionResult<List<SecretaryCabinetInformationDto>>> GetAllCabinetInformationAppoiments()
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.secretaryService.GetAllCabinetInformationAppoiment(Email);
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }







        [HttpPost("AddSecretary")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<ActionResult<SecritaryDto>> PostNewSecretary([FromBody] string EmailSecritary)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var emailAdmin = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var result = await this.secretaryService.AddSecretaryService(EmailSecritary, emailAdmin);
                transaction.Complete();
                return result;


            }
            catch (ConflictException Ex)
            {
                return StatusCode(409);
            }
            catch (StorageValidationException Ex)
            {
                return NotFound();
            }
            catch (ValidationException Ex)
            {
                return BadRequest();
            }
            catch (ServiceException Ex)
            {
                return BadRequest();
            }
            finally
            {
                transaction.Dispose();
            }
        }
        [HttpGet("GetAllSecretary")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<ActionResult<List<SecritaryDto>>> GetListSecretary()
        {
            try
            {
                var emailAdmin = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.secretaryService.GetAllSecretary(emailAdmin);
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpPatch("UpdateStatusSecretary")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<ActionResult> PatchStatusSecretary(UpdateStatusSecretaryDto updateStatusSecretaryDto)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var emailAdmin = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.secretaryService.UpdateStatusSecretaryService(updateStatusSecretaryDto, emailAdmin);
                transaction.Complete();
                return Ok();
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
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
