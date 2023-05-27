

using DTO;
using Server.Models.Cities;

namespace Server.Services.Foundation.cityService
{
    public static class CityMapperService
    {
        public static List<CityDto> MapperListCity(List<City> cities)
        {
            List<CityDto> NewlistCities = new List<CityDto>();
            foreach (var city in cities) 
            {
                city.Name = city.Id +"-"+ city.Name;
                NewlistCities.Add(new CityDto { Id=city.Id , Name = city.Name});
            }
            return NewlistCities;
        }
    }
}
