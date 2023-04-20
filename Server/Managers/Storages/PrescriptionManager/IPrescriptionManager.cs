using Server.Models.Prescriptions;

namespace Server.Managers.Storages.PrescriptionManager
{
    public interface IPrescriptionManager
    {
        public Task<Prescription> InsertPrescriptionAsync(Prescription prescription);
        public Task<Prescription> UpdatePrescriptionAsync(Prescription prescription);
        public Task SelectPrescriptionByIdMedicalOrdreAsync(Guid MedicalOrdre);
    }
}
