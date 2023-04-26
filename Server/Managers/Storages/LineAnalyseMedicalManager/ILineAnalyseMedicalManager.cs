using Server.Models.LineAnalyseMedical;

namespace Server.Managers.Storages.LineAnalyseMedicalManager
{
    public interface ILineAnalyseMedicalManager
    {
        public Task<LineAnalyseMedicals> InsertLineAnalyseMedical(LineAnalyseMedicals lineAnalyseMedicals);
        public Task<List<LineAnalyseMedicals>> SelectLinesMedicalByIdAnalyseAsync(Guid AnalyseId);
        public Task<LineAnalyseMedicals> SelectLineAnalyseById(Guid LineAnalyseId);
        public Task<LineAnalyseMedicals> UpdateLineAnalyseAsync(LineAnalyseMedicals lineAnalyseMedicals);
    }
}
