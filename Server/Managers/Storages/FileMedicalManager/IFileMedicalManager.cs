using Server.Models.fileMedical;

namespace Server.Managers.Storages.FileMedicalManager
{
    public interface IFileMedicalManager
    {
        public Task<fileMedicals> InsertFileMedical(fileMedicals fileMedicals);
    }
}
