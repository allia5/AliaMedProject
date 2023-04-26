using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.AnalyseMedicalService;
using Server.Services.Foundation.ResultAnalyseService;
using System.Security.Claims;
using System.Transactions;
using static Server.Utility.Utility;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialisteAnalyseController : ControllerBase
    {
        public readonly IAnalyseMedicalService analyseMedicalService;
        public readonly IResultAnalyseService resultAnalyseService;
        public SpecialisteAnalyseController(IResultAnalyseService resultAnalyseService,IAnalyseMedicalService analyseMedicalService)
        {
            this.analyseMedicalService = analyseMedicalService;
            this.resultAnalyseService = resultAnalyseService;
        }
        [HttpPost("PostNewAnalyseResult")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ANALYSE")]
        public async Task<ActionResult> PostAnalyseResult(AnalyseResultToAdd analyseResultToAdd)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                await this.resultAnalyseService.PostNewAnalyseResult(Email, analyseResultToAdd);
                transaction.Complete();
                return Ok();
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
            finally
            {
                transaction.Dispose();
            }
        }
        [HttpGet("GetInformationAnalyse/{CodeQr}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ANALYSE")]
        public async Task<ActionResult<InformationAnalyseResultDto>> GetInformationAnalyse(string CodeQr)
        {
            try
            {
                CodeQr = CodeQr.Replace("-", "/");
                var Email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.analyseMedicalService.GetAllAnalyseResultByCode(Email, CodeQr);
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
