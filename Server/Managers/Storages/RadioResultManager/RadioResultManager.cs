using Server.Data;
using Server.Models.ResultsRadio;

namespace Server.Managers.Storages.RadioResultManager
{
    public class RadioResultManager : IRadioResultManager
    {
        public readonly ServerDbContext serverDbContext;
        public RadioResultManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }
        public async Task<ResultRadio> InserRadioResult(ResultRadio resultRadio)
        {
           var Result = this.serverDbContext.RadioResult.Add(resultRadio);
            await this.serverDbContext.SaveChangesAsync();
            return Result.Entity;
        }
    }
}
