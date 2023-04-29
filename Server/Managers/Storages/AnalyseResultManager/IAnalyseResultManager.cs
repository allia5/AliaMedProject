using Server.Models.ResultAnalyses;

namespace Server.Managers.Storages.AnalyseResultManager
{
    public interface IAnalyseResultManager
    {
        public Task<ResultAnalyse> InsertAnalyseResultAsync(ResultAnalyse resultAnalyse);
        public Task<ResultAnalyse> SelectResultAnalyseByIdLineMedcialAnalyse(Guid IdLine);
    }
}
