using Server.Models.FileChronicDisease;

namespace Server.Managers.Storages.FileChronicDiseasesManager
{
    public interface IFileChronicDiseasesManager
    {
        public Task<FileChronicDiseases> insertFileChronicDisease(FileChronicDiseases fileChronicDiseases);
    }
}
