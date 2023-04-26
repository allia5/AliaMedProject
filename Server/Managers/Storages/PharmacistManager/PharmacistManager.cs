using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Pharmacist;

namespace Server.Managers.Storages.PharmacistManager
{
    public class PharmacistManager : IPharmacistManager
    {
        public ServerDbContext serverDbContext { get; set; }
        public PharmacistManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }
        public async Task<Pharmacists> SelectPharmacistByIdAsync(Guid PharmacistId)
        {
            return await (from Pharmacist in this.serverDbContext.Pharmacists 
                          where Pharmacist.Id == PharmacistId 
                          select Pharmacist).FirstOrDefaultAsync();
        }
    }
}
