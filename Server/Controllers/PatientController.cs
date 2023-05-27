using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.UserService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public readonly IUserService userService;
        public PatientController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("GetDoctorsAvailble/{CityId}")]
        public async Task<ActionResult<List<DoctorSearchDto>>> GetDoctorsAvailble(int CityId)
        {
            try
            {
                return await this.userService.GetDoctorsAvailble(CityId);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
