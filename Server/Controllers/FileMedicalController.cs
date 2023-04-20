using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Services.Foundation.FileMedicalService;
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




        [HttpGet("DownloadFilePrescription/{Id}")]
        public async Task<IActionResult> GetFilePrescription(string Id)
        {
            try
            {
                Id=System.Web.HttpUtility.UrlDecode(Id);
                var FilePrescription = await this.FileMedicalService.GetFilePrescriptionByIdOrdreMedical(Id);
                using (MemoryStream pdfStream = new())
                {
                    pdfStream.Write(FilePrescription, 0, FilePrescription.Length);
                    pdfStream.Position = 0;
                    var result = File(pdfStream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "sample.docx");
                    return result;
                }
            }
            catch(ValidationException Ex)
            {
                return BadRequest(Ex.Message);
            }catch(ServiceException Ex)
            {
                return NotFound();
            }catch(Exception e)
            {
                return Problem(e.Message);
            }
          
        }

        [HttpGet("DownloadFileRadio/{Id}")]
        public async Task<IActionResult> GetFileRadio(string Id)
        {

            var FileRadio = await this.FileMedicalService.GetFileRadioByIdOrdreMedical(Id);
            using (MemoryStream pdfStream = new())
            {
                pdfStream.Write(FileRadio, 0, FileRadio.Length);
                pdfStream.Position = 0;
                var result = File(pdfStream.ToArray(), "application/pdf", "sample.pdf");
                return result;
            }
        }

        [HttpGet("DownloadFileAnalyse/{Id}")]
        public async Task<IActionResult> GetFileAnalyse(string Id)
        {
            var FileAnalyse = await this.FileMedicalService.GetFileAnalyseByIdOrdreMedical(Id);
            using (MemoryStream pdfStream = new())
            {
                pdfStream.Write(FileAnalyse, 0, FileAnalyse.Length);
                pdfStream.Position = 0;
                var result = File(pdfStream.ToArray(), "application/pdf", "sample.pdf");
                return result;
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
