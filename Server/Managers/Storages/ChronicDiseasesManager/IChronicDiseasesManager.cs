using Server.Models.ChronicDiseases;

namespace Server.Managers.Storages.ChronicDiseasesManager
{
    public interface IChronicDiseasesManager 
    {
        public Task<List<ChronicDisease>> SelectAllChronicDiseasesAsync();
        public Task<ChronicDisease> SelectChronicDiseasesByIdAsync(int Id);
    }
}
