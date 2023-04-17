using Server.Data;
using Server.Models.PrescriptionLine;
using Server.Models.Prescriptions;

namespace Server.Managers.Storages.PrescriptionLineManager
{
    public class PrescriptionLineManager : IPrescriptionLineManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public PrescriptionLineManager(ServerDbContext dbContext)
        {
            ServerDbContext = dbContext;
        }
        public async Task<PrescriptionLines> InsertPrescriptionLineAsync(PrescriptionLines prescriptionLines)
        {
            var result = this.ServerDbContext.PrescriptionLines.Add(prescriptionLines);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;

        }
    }
}
