using Server.Models.MedicalAnalysis;
using Server.Models.SpecialisteAnalyses;

namespace Server.Managers.Storages.SpecialisteAnalyseManager
{
    public interface ISpecialisteAnalyseManager
    {
        public Task<SpecialisteAnalyse> SelectSpecialisteAnalyseByIdUser(string UserId);
    }
}
