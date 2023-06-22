using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Services.Foundation.cityService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public readonly ICityService cityService;
        public ServerDbContext ServerDbContext { get; set; }

        public CityController(ICityService cityService, ServerDbContext ServerDbContext)
        {
            this.cityService = cityService;
            this.ServerDbContext = ServerDbContext;
        }
        [HttpGet]
        public async Task< IActionResult> Get()
        {
            try
            {
                return Ok(await this.ServerDbContext.City.ToListAsync());
            }
            catch (Exception exception)
            {
                return Problem($"{exception.Message} : {exception.InnerException.Message}");
            }
            
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
