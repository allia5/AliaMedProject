

using Server.Models.Cities;

namespace Server.Managers.Storages.CityManager
{
    public interface ICityManager
    {
        public Task<List<City>> selectAllCity();
    }
}
