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
            this.ordreMedicalService = ordreMedicalService;
        }
        [HttpGet("GetAllOrdreArchiveFileMedcialPatient/{FileId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<MedicalFileArchivePatientDto>> GetMedicalArchivePatient( string FileId)
        {
            try
            {
                FileId = FileId.Replace("-", "/");
               
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.ordreMedicalService.GetMedecalArchivePatient(Email, FileId);
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
        }

        [HttpGet("GetAllOrdreArchiveFileMedcial/{AppointmentId}/{FileId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<MedicalFileArchiveDto>> GetMedicalArchive(string AppointmentId,string FileId)
        {
            try
            {
                FileId = FileId.Replace("-", "/");
                AppointmentId = AppointmentId.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
               return await this.ordreMedicalService.GetListOrdreFileMedical(Email,AppointmentId,FileId);
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
        }

        [HttpPatch("PatchMedicalOrdre")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE")]
        public async Task<ActionResult> UpdateStatusMedicalOrdre(UpdateOrdreMedicalDto updateFileMedicalDto)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.ordreMedicalService.UpdateStatusOrdreMedicalService(Email, updateFileMedicalDto);
                transaction.Complete();
                return Ok();
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
            finally
            {
                transaction.Dispose();
            }
          
        }

        [HttpGet("GetAllMedicalOrdreSecritary/{CabinetId}/{DoctorId}/{DateOrdreMedical}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "SECRITAIRE")]
        public async Task<ActionResult<List<InformationOrderMedicalSecritary>>> GetAllMedicalOrdreSecritary(string CabinetId,string DoctorId,string DateOrdreMedical)
        {

            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                CabinetId = CabinetId.Replace("-", "/");
                DoctorId = DoctorId.Replace("-", "/");
                DateOrdreMedical = System.Web.HttpUtility.UrlDecode(DateOrdreMedical);
                var result = await this.ordreMedicalService.SelectAllMedicalOrdreSecritary(Email, new KeysAppoimentInformationSecretary { CabinetId = CabinetId, DateAppoiment = DateTime.Parse(DateOrdreMedical), IdDoctor = DoctorId });
                return result;

            }catch(ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch(ServiceException Ex)
            {
                return StatusCode(412);

            }catch(Exception Ex)
            {
                return Problem(Ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult> PostNewOrdreMedical( OrderMedicalToAddDro orderMedicalToAddDro)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                orderMedicalToAddDro.FileId = orderMedicalToAddDro.FileId.Replace("-", "/");
                orderMedicalToAddDro.AppointmentId = orderMedicalToAddDro.AppointmentId.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var ResultOrdreMedical = await this.ordreMedicalService.AddOrdreMedicalDto(Email, orderMedicalToAddDro);
                transaction.Complete();
                return Ok();
            

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
