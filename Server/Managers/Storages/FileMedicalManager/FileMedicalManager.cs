using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.fileMedical;

namespace Server.Managers.Storages.FileMedicalManager
{
    public class FileMedicalManager : IFileMedicalManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public FileMedicalManager(ServerDbContext dbContext)
        {
            ServerDbContext = dbContext;
        }
        public async Task<fileMedicals> InsertFileMedical(fileMedicals fileMedicals)
        {
          
                var result = this.ServerDbContext.fileMedicals.Add(fileMedicals);
                await this.ServerDbContext.SaveChangesAsync();
                return result.Entity;
          
          

        }

        public async Task<List<fileMedicals>> SelectFilesMedicalByIdUser(string UserId)
        {
            return await (from file in this.ServerDbContext.fileMedicals where file.IdUser == UserId select file).ToListAsync();
        }
    }
}
