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

      /*  public async Task<List<AdviceMedical>> SelectAdviceMedicalByIdOrdreMedicalIdUser(Guid OrdreMedicalId, string UserId)
        {
          return await (from ItemAdvice in this.serverDbContext.adviceMedicals where ItemAdvice.OrdreMedicalId == OrdreMedicalId && ItemAdvice.transmitterUserId ==UserId select ItemAdvice).ToListAsync();
        }*/


        public async Task<AdviceMedical> UpdateAdviceMedical(AdviceMedical adviceMedical)
        {
            var result = this.serverDbContext.adviceMedicals.Update(adviceMedical);
            await this.serverDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<AdviceMedical> InsertAdviceMedical(AdviceMedical adviceMedical)
        {
            var result =  this.serverDbContext.Add(adviceMedical);
            await this.serverDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
