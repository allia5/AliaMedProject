using Server.Models.MedicalOrder;

namespace Server.Managers.Storages.OrdreMedicalManager
{
    public interface IOrdreMedicalManager
    {
        public Task<List<MedicalOrdres>> SelectListOrdreMedicalByIdMedicalFile(Guid FileId);
        public Task<MedicalOrdres> InsertOrdreMedicalAsync(MedicalOrdres MedicalOrdres);
    }
}
