using Server.Models.PrescriptionLine;

namespace Server.Managers.Storages.PrescriptionLineManager
{
    public interface IPrescriptionLineManager
    {
        public Task<PrescriptionLines> InsertPrescriptionLineAsync(PrescriptionLines prescriptionLines);
    }
}
