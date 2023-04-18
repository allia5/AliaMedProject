using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.OrdreMedicalService;
using System.Security.Claims;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdreMedicalController : ControllerBase
    {
        public readonly IOrdreMedicalService ordreMedicalService;
        public OrdreMedicalController(IOrdreMedicalService ordreMedicalService)
        {
            this.ControllerContext = ControllerContext;
        }
        [HttpPost("PostNewOrdreMedical")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<OrdreMedicalDto>> PostNewOrdreMedical(OrderMedicalToAddDro orderMedicalToAddDro)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var ResultOrdreMedical = await this.ordreMedicalService.AddOrdreMedicalDto(Email, orderMedicalToAddDro);
                transaction.Complete();
                return ResultOrdreMedical;

            }
            catch(ValidationException Ex)
            {
                return BadRequest();
            }catch(ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch(Exception e )
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
