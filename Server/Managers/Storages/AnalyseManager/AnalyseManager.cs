using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Analyse;

namespace Server.Managers.Storages.AnalyseManager
{
    public class AnalyseManager : IAnalyseManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public AnalyseManager(ServerDbContext serverDbContext)
        {
            this.ServerDbContext = serverDbContext;
        }
        public async Task<Analyses> InsertAnalyseAsync(Analyses analyses)
        {
            try
            {
                var result = this.ServerDbContext.Analyses.Add(analyses);
                await this.ServerDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<Analyses> UpdateAnalyseAsync(Analyses analyses)
        {
           var result = this.ServerDbContext.Analyses.Update(analyses);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Analyses> SelectAnalyseByOrdreMedicalId(Guid MedicalOrdreId)
        {
            return await (from ItemAnalyse in this.ServerDbContext.Analyses where ItemAnalyse.IdOrdreMedical == MedicalOrdreId select ItemAnalyse ).FirstOrDefaultAsync();
        }

        public async Task<Analyses> SelectAnalyseByCodeAsync(string CodeQr)
        {
            return await (from analyseItem in this.ServerDbContext.Analyses 
                          where analyseItem.QrCode == CodeQr 
                          select analyseItem).FirstOrDefaultAsync();
        }

        public async Task<Analyses> SelectAnalyseByIdAsync(Guid AnalyseId)
        {
            return await (from ItemAnalyse in this.ServerDbContext.Analyses where ItemAnalyse.Id == AnalyseId select ItemAnalyse).FirstOrDefaultAsync();
        }
    }
}
