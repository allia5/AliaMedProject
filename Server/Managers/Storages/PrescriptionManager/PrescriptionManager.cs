using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Prescriptions;

namespace Server.Managers.Storages.PrescriptionManager
{
    public class PrescriptionManager : IPrescriptionManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public PrescriptionManager (ServerDbContext dbContext)
        {
            ServerDbContext = dbContext;
        }
        public async Task<Prescription> InsertPrescriptionAsync(Prescription prescription)
        {
            var result = this.ServerDbContext.prescriptions.Add(prescription);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Prescription> UpdatePrescriptionAsync(Prescription prescription)
        {
            var result = this.ServerDbContext.prescriptions.Update(prescription);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Prescription> SelectPrescriptionByIdMedicalOrdreAsync(Guid MedicalOrdre)
        {
            return await (from ItemPrescription in this.ServerDbContext.prescriptions where ItemPrescription.IdMedicalOrdre == MedicalOrdre select ItemPrescription).FirstOrDefaultAsync();
        }
    }
}
