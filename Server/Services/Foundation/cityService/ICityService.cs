
using DTO;
using Server.Models.Cities;

namespace Server.Services.Foundation.cityService
{
    public interface ICityService
    {
        public Task<List<CityDto>> GetCityListAsync();
    }
}
