using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Foundation.ChronicDiseasesService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChronicDiseasesController : ControllerBase
    {
        public readonly IChronicDiseasesService chronicDiseasesService;
        public ChronicDiseasesController(IChronicDiseasesService chronicDiseasesService)
        {
            this.chronicDiseasesService = chronicDiseasesService;
        }
        [HttpGet("GetAllchronicDiseases")]
        public async Task<ActionResult<List<chronicDiseasesDto>>> GetAllchronicDiseases()
        {
            try
            {
                return await this.chronicDiseasesService.GetChronicDiseasesAsync();

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
