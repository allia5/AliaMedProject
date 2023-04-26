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
    }
}
