using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OtripleS.Web.Api.Models.Users.Exceptions;
using Server.Hubs.PlanningAppoimentHub;
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
        public readonly IHubContext<PlanningAppoimentHub> hubContext;
        
        public MedicalPlanningController(IPlanningAppoimentService planningAppoimentService, IHubContext<PlanningAppoimentHub> hubContext)
        {
            this.planningAppoimentService = planningAppoimentService;
            this.hubContext = hubContext;
        }

        [HttpPatch("DelayAppoiment")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult> PatchStatusAppoimentPatient([FromBody] DelayeAppoimentMedical delayeAppoiment)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
              
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.planningAppoimentService.DelayeAppoimentPatient(Email, delayeAppoiment);
                this.hubContext.Clients.All.SendAsync("ReceiveUpdateStatusAppoitment", new UpdateStatusAppoimentDto { Id=delayeAppoiment.Id,statusPlaningDto=delayeAppoiment.statusPlaningDto}).Wait();
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

        [HttpPatch("UpdateStatusAppoiment")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE,MEDECIN")]
        public async Task<ActionResult> PatchStatusAppoimentPatient([FromBody] UpdateStatusAppoimentDto updateStatusAppoimentDto)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var Role = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.planningAppoimentService.UpdateStatusAppoimentMedical(Email, updateStatusAppoimentDto,Role);
                
               this.hubContext.Clients.All.SendAsync("ReceiveUpdateStatusAppoitment", updateStatusAppoimentDto).Wait();
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

        [HttpGet("ListAppoimentDoctorPatient/{CabinetId}/{DateAppoiment}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<List<PlanningDto>>> GetListAppoimentPlanningPatientDoctor(string CabinetId, string DateAppoiment)
        {
            try
            {
                CabinetId = CabinetId.Replace("-", "/");
                
                DateAppoiment = System.Web.HttpUtility.UrlDecode(DateAppoiment);
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.planningAppoimentService.GetPatientAppoimentMedicalDoctor(email, new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateTime.Parse(DateAppoiment)});
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (ServiceException Ex)
            {

                return StatusCode(412);
            }
            catch (Exception Ex)
            {
                return Problem(Ex.Message);
            }
        }







        [HttpGet("ListAppoimentSecretaryPatient/{CabinetId}/{DoctorId}/{DateAppoiment}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE")]
        public async Task<ActionResult<List<PlanningDto>>> GetListAppoimentPlanningPatientSecretary(string CabinetId ,string DoctorId,string DateAppoiment)
        {
            try
            {
                CabinetId = CabinetId.Replace("-", "/");
                DoctorId = DoctorId.Replace("-", "/");
                DateAppoiment = System.Web.HttpUtility.UrlDecode(DateAppoiment);
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.planningAppoimentService.GetPatientAppoimentMedicalSecretary(email, new KeysAppoimentInformationSecretary { CabinetId = CabinetId, DateAppoiment = DateTime.Parse(DateAppoiment), IdDoctor = DoctorId });
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch(ServiceException Ex)
            {

                return StatusCode(412);
            }catch (Exception Ex) 
            {
                return Problem(Ex.Message); 
            }
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
