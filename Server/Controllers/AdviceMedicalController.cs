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
        [HttpGet("GetAdviceMedicalMessages/{OrdremEdiclaId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]
        public async Task<ActionResult<List<AdviceMedicalDto>>> GetAdvicesMedical(string OrdremEdiclaId)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                OrdremEdiclaId = OrdremEdiclaId.Replace("-","/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var result=await this.adviceMedicalService.GetAdviceMedical(Email, OrdremEdiclaId);
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
    }
}
