using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.PrescriptionLineService;
using Server.Services.Foundation.PrescriptionService;
using System.Security.Claims;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistController : ControllerBase
    {
        public readonly IPrescriptionService prescriptionService;
        public readonly IPrescriptionLineService prescriptionLineService;

       public PharmacistController(IPrescriptionLineService prescriptionLineService,IPrescriptionService prescriptionService)
        {
            this.prescriptionService = prescriptionService;
            this.prescriptionLineService = prescriptionLineService;
        }


        [HttpPatch("PatchStatusPrescription")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PHARMACIEN")]
        public async Task<ActionResult> PatchStatusLinePrescription([FromBody] string LinePrescriptionId)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.prescriptionLineService.UpdateStatusPrescriptionLine(Email, LinePrescriptionId);
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
            }
            catch (StorageValidationException ex)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem();
            }
            finally
            {

                transaction.Dispose();

            }
        }
        [HttpGet("GetInformationPrescription/{CodeQr}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "PHARMACIEN")]
        public async Task<ActionResult<InformationPrescriptionResultDto>> GetInformationPrescription(string CodeQr)
        {
            try
            {
                CodeQr = CodeQr.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.prescriptionService.GetPrescriptionInformation(Email, CodeQr);
            }
             catch (ValidationException Ex)
            {
                return BadRequest(Ex.Message);

            }
            catch (ServiceException Ex)
            {
                return StatusCode(412);
            }
            catch (StorageValidationException Ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
