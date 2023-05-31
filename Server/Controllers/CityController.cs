using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Foundation.cityService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }
        [HttpGet("GetAllCities")]
        public async Task<ActionResult<List<CityDto>>> GetAllCity()
        {
            try
            {
                return await this.cityService.GetCityListAsync();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
