using Server.Models.MedicalOrder;

namespace Server.Managers.Storages.OrdreMedicalManager
{
    public interface IOrdreMedicalManager
    {
        public Task<List<MedicalOrdres>> SelectListOrdreMedicalByIdMedicalFile(Guid FileId);
        public Task<MedicalOrdres> InsertOrdreMedicalAsync(MedicalOrdres MedicalOrdres);
        public Task<List<MedicalOrdres>> SelectAllMedicalOrderByIdCabinetByIdDoctorByDate(Guid CabinetId,Guid DoctorId, DateTime Date);
        public Task<MedicalOrdres> SelectMedicalOrdreByIdByIdDoctorByIdCabinet(Guid OrdreMedicalId,Guid DoctorId,Guid CabinetId);
        public Task<MedicalOrdres> UpdateMedicalOrdreAsync(MedicalOrdres MedicalOrdres);
    }
}
