using Server.Models.Prescriptions;

namespace Server.Managers.Storages.PrescriptionManager
{
    public interface IPrescriptionManager
    {
        public Task<Prescription> InsertPrescriptionAsync(Prescription prescription);
    }
}
