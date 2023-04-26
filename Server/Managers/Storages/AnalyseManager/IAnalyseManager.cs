using Microsoft.OpenApi.Any;
using Server.Models.Analyse;

namespace Server.Managers.Storages.AnalyseManager
{
    public interface IAnalyseManager
    {
        public Task<Analyses> InsertAnalyseAsync(Analyses analyses);
        public Task<Analyses> UpdateAnalyseAsync(Analyses analyses);
        public Task<Analyses> SelectAnalyseByOrdreMedicalId(Guid MedicalOrdreId);
        public Task<Analyses> SelectAnalyseByCodeAsync(string CodeQr);
        public Task<Analyses> SelectAnalyseByIdAsync(Guid AnalyseId);

    }
}
