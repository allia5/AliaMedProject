using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.AdviceMedicals;

namespace Server.Managers.Storages.AdviceManager
{
    public class AdviceManager : IAdviceManager
    {
        public ServerDbContext serverDbContext { get; set; }    
        public AdviceManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }
        public async Task<List<AdviceMedical>> adviceMedicalsByIdOrdreMedicalAsync(Guid OrdreMedicalId)
        {
            return await (from AdviceItem in this.serverDbContext.adviceMedicals where AdviceItem.OrdreMedicalId == OrdreMedicalId select AdviceItem).ToListAsync();
        }
    }
}
