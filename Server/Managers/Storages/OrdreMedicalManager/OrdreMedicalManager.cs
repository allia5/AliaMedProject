using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.MedicalOrder;

namespace Server.Managers.Storages.OrdreMedicalManager
{
    public class OrdreMedicalManager : IOrdreMedicalManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public OrdreMedicalManager(ServerDbContext serverDbContext)
        {
            ServerDbContext = serverDbContext;
        }
        public async Task<List<MedicalOrdres>> SelectListOrdreMedicalByIdMedicalFile(Guid FileId)
        {
            return await (from ordre in this.ServerDbContext.MedicalOrdres where ordre.IdFileMedical == FileId select ordre ).ToListAsync();
                 
        }
    }
}
