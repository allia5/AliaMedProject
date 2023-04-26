using Server.Models.PrescriptionLine;

namespace Server.Managers.Storages.LinePrescriptionMedicalManager
{
    public interface ILinePrescriptionMedicalManager
    {
        public Task<PrescriptionLines> SelectLinePrescriptionById(Guid lineId);
        public Task<List<PrescriptionLines>> SelectLinePrescriptionByIdPrescription(Guid PrescriptionId);
    }
}
