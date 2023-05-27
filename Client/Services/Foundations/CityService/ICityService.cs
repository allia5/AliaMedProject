using DTO;

namespace Client.Services.Foundations.CityService
{
    public interface ICityService
    {
        public Task<List<CityDto>> GetAllCities();
    }
}
