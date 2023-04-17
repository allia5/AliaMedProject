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
            var result = this.ServerDbContext.Analyses.Add(analyses);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Analyses> UpdateAnalyseAsync(Analyses analyses)
        {
           var result = this.ServerDbContext.Analyses.Update(analyses);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
