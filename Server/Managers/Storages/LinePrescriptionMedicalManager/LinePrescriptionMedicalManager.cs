using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.PrescriptionLine;

namespace Server.Managers.Storages.LinePrescriptionMedicalManager
{
    public class LinePrescriptionMedicalManager : ILinePrescriptionMedicalManager
    {
        public ServerDbContext serverDbContext { get; set; }
        public LinePrescriptionMedicalManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }

        public async Task<PrescriptionLines> SelectLinePrescriptionById(Guid lineId)
        {
            return await (from Line in this.serverDbContext.PrescriptionLines where Line.Id == lineId select Line).FirstOrDefaultAsync();
        }

        public async Task<List<PrescriptionLines>> SelectLinePrescriptionByIdPrescription(Guid PrescriptionId)
        {
            return await(from Line in this.serverDbContext.PrescriptionLines 
                         where Line.IdPrescription == PrescriptionId 
                         select Line).ToListAsync();
        }
        public async Task<PrescriptionLines> InsertPrescriptionLineAsync(PrescriptionLines prescriptionLines)
        {
            var result = this.serverDbContext.PrescriptionLines.Add(prescriptionLines);
            await this.serverDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<PrescriptionLines> UpdatePrescriptionLine(PrescriptionLines prescriptionLines)
        {
            var Result = this.serverDbContext.PrescriptionLines.Update(prescriptionLines);
            await this.serverDbContext.SaveChangesAsync();
            return Result.Entity;
        }
    }
}
