using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Pharmacys;

namespace Server.Managers.Storages.PharmacyManager
{
    public class PharmacyManager : IPharmacyManager
    {
        public  ServerDbContext serverDbContext { get; set; }
        public PharmacyManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }
        public async Task<Pharmacy> SelectPharmacyById(Guid PharmacyId)
        {
            return await (from PharmacyItem in this.serverDbContext.Pharmacy
                          where PharmacyItem.Id == PharmacyId select PharmacyItem).
                          FirstOrDefaultAsync();
        }
    }
}
