using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.DoctorService;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly IDoctorService doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }
        [HttpGet("GetInformationDoctor")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "ADMIN")]
        public async Task<ActionResult<List<DoctorInformationDto>>> GetListInformationDoctor()
        {
            try
            {
                var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                return await this.doctorService.GetListInformationFromDoctor(email);
            }
            catch (ServiceException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (StorageValidationException Ex)
            {
                return NotFound(Ex.InnerException);
            }
            catch (ValidationException Ex)
            {
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}
