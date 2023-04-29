using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.ResultAnalyses;

namespace Server.Managers.Storages.AnalyseResultManager
{
    public class AnalyseResultManager : IAnalyseResultManager
    {
        public ServerDbContext serverDbContext { get; set; }
        public AnalyseResultManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }
        public async Task<ResultAnalyse> InsertAnalyseResultAsync(ResultAnalyse resultAnalyse)
        {
            try
            {
                var Result = this.serverDbContext.resultAnalyses.Add(resultAnalyse);
                await this.serverDbContext.SaveChangesAsync();
                return Result.Entity;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<ResultAnalyse> SelectResultAnalyseByIdLineMedcialAnalyse(Guid IdLine)
        {
            return await (from ItemResultAnalyse in this.serverDbContext.resultAnalyses
                          where ItemResultAnalyse.IdLineAnalyse == IdLine 
                          select ItemResultAnalyse).FirstOrDefaultAsync();
        }
    }
}
