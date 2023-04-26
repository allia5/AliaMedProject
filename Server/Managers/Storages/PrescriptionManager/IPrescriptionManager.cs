using Server.Models.Prescriptions;

namespace Server.Managers.Storages.PrescriptionManager
{
    public interface IPrescriptionManager
    {
        public Task<Prescription> InsertPrescriptionAsync(Prescription prescription);
        public Task<Prescription> UpdatePrescriptionAsync(Prescription prescription);
        public Task<Prescription> SelectPrescriptionByIdMedicalOrdreAsync(Guid MedicalOrdre);
        public Task<Prescription> SelectPrescriptionByCode(string Code);
    }
}
