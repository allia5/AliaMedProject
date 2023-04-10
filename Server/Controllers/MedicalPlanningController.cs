using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtripleS.Web.Api.Models.Users.Exceptions;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.PlanningAppoimentService;
using System.Security.Claims;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalPlanningController : ControllerBase
    {
        public readonly IPlanningAppoimentService planningAppoimentService;
        public MedicalPlanningController(IPlanningAppoimentService planningAppoimentService)
        {
            this.planningAppoimentService = planningAppoimentService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]
        public async Task<ActionResult<List<AppointmentInformationDto>>> PostMedicalPlanningAppoiment(KeysReservationMedicalDto keysReservationMedicalDto)
        {
            try
            {
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.planningAppoimentService.PostNewPlanningAppoimentMedical(email, keysReservationMedicalDto);
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.Message);
            }
            catch (FailedUserServiceException Ex)
            {
                return StatusCode(403);
            }
            catch (ServiceException Ex)
            {
                return NotFound();
            }
            catch (StorageValidationException Ex)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]

        public async Task<ActionResult<List<AppointmentInformationDto>>> GetListMedicalPlanningAppoiment()
        {
            try
            {
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.planningAppoimentService.GetListPlanningAppoimentMedical(email);
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
        [HttpDelete("DeleteMedicalAppoiment")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]
        public async Task<ActionResult> DeleteMedicalAppoimentById(string IdMedicalAppoiment)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.planningAppoimentService.DeleteMedicalPlanningAppoiment(email, IdMedicalAppoiment);
                transaction.Complete();
                return Ok();
            }
            catch (ValidationException Ex)
            {
                return BadRequest();
            }
            catch (ServiceException)
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
