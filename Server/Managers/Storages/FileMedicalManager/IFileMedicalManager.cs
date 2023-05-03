using Server.Models.fileMedical;

namespace Server.Managers.Storages.FileMedicalManager
{
    public interface IFileMedicalManager
    {
        public Task<fileMedicals> InsertFileMedical(fileMedicals fileMedicals);
        public Task<List<fileMedicals>> SelectFilesMedicalByIdUser(string UserId);
        public Task<fileMedicals> SelectFilesMedicalByIdMedical(string IdMedical);
        public Task<fileMedicals> SelectFileMedicalByIdAsync(Guid FileId);
        public Task<fileMedicals> UpdateFileMedicalAsync(fileMedicals fileMedicals);
        public Task<fileMedicals> SelectFileMedicalByIdOrdreMedicalAsync(Guid OrdreMedicalId);
    }
}
