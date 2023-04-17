using Server.Models.Analyse;

namespace Server.Managers.Storages.AnalyseManager
{
    public interface IAnalyseManager
    {
        public Task<Analyses> InsertAnalyseAsync(Analyses analyses);
    }
}
