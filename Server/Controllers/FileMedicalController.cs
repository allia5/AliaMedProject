using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.FileMedicalService;
using Syncfusion.Pdf.Interactive;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileMedicalController : ControllerBase
    {
        public readonly IFileMedicalService FileMedicalService;
        public FileMedicalController(IFileMedicalService fileMedicalService)
        {
            this.FileMedicalService = fileMedicalService;
        }
        [HttpPatch("TransferFileMedical")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult> TranferFileMedcial(FileTransferDto fileTransferDto)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
              
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.FileMedicalService.TransferFileMedical(Email, fileTransferDto);
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
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            finally { transaction.Dispose(); }
        }

        [HttpGet("GetMedicalFilePatient")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PATIENT")]
        public async Task<ActionResult<List<FileMedicalPatientDto>>> GetFilesPatient()
        {
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                
                    return await this.FileMedicalService.GetFilesMedicalPatient(Email);
              
               
            }
            catch(ServiceException Ex)
            {
                return StatusCode(412);
            }catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

       



        [HttpPatch("PatchFileMedical")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult> UpdateFileMedical(UpdateFileMedicalDto updateFileMedicalDto)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                updateFileMedicalDto.AppointmentId = updateFileMedicalDto.AppointmentId.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.FileMedicalService.UpdateFileMedicalService(Email, updateFileMedicalDto);
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
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }finally { transaction.Dispose(); }
        }

        [HttpGet("GetFilesPatient/{IdAppointment}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<FileMedicalMainPatientDto>> GetAllFileMedicalPatient(string IdAppointment)
        {
            try
            {
                IdAppointment = IdAppointment.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.FileMedicalService.GetAllFileMedicalMainPatient(Email, IdAppointment);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.InnerException);
            }
            catch (ServiceException ex)
            {
                return StatusCode(412);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpPost("PostNewFileMedical")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "MEDECIN")]
        public async Task<ActionResult<FileMedicalPatientDto>> PostNewFileMedicalPatient([FromBody] FileMedicalToAddDto fileMedicalToAdd)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                
                fileMedicalToAdd.IdAppointment = fileMedicalToAdd.IdAppointment.Replace("-", "/");
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var result =  await this.FileMedicalService.AddNewFileMedicalPatient(email,fileMedicalToAdd);
                transaction.Complete();
                return Ok(result);
            }
            catch (ValidationException Ex)
            {
                return StatusCode(412);
            }
            catch (ServiceException Ex)
            {
                return BadRequest(Ex.TargetSite);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
