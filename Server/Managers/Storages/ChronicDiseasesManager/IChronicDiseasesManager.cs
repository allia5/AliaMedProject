using Server.Models.ChronicDiseases;
using Server.Models.FileChronicDisease;

namespace Server.Managers.Storages.ChronicDiseasesManager
{
    public interface IChronicDiseasesManager 
    {
        public Task<List<ChronicDisease>> SelectAllChronicDiseasesAsync();
        public Task<ChronicDisease> SelectChronicDiseasesByIdAsync(int Id);
        public Task<List<FileChronicDiseases>> SelectChronicDiseasesByIdMedicalFileAsync(Guid FileId);
    }
}
