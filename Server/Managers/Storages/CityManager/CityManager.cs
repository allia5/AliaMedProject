
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Cities;

namespace Server.Managers.Storages.CityManager
{
    public class CityManager : ICityManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public CityManager(ServerDbContext ServerDbContext) 
        {
            this.ServerDbContext = ServerDbContext;
        }

        public async Task<List<City>> selectAllCity()
        {
           return await this.ServerDbContext.City.ToListAsync();
        }
    }
}
