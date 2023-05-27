
using DTO;
using Server.Managers.Storages.CityManager;
using Server.Models.Cities;
using static Server.Services.Foundation.cityService.CityMapperService;

namespace Server.Services.Foundation.cityService
{
    public partial class CityService : ICityService
    {
        public ICityManager CityManager { get; set; }
        public CityService(ICityManager CityManager) 
        {
            this.CityManager = CityManager;
        }
        public async Task<List<CityDto>> GetCityListAsync() =>
            await TryCatch(async () =>
            {
                var listCity = await this.CityManager.selectAllCity();
                return MapperListCity(listCity);
            });
    }
}
