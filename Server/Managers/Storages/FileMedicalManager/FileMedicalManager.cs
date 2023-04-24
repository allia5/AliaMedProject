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

        public async Task<fileMedicals> SelectFileMedicalByIdAsync(Guid FileId)
        {
            return await (from file in this.ServerDbContext.fileMedicals where file.Id == FileId select file).FirstOrDefaultAsync();
        }

        public async Task<fileMedicals> UpdateFileMedicalAsync(fileMedicals fileMedicals)
        {
            var result =  this.ServerDbContext.fileMedicals.Update(fileMedicals);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<fileMedicals> SelectFileMedicalByIdOrdreMedicalAsync(Guid OrdreMedicalId)
        {
            return await (from itemOrdreMedical in this.ServerDbContext.MedicalOrdres where itemOrdreMedical.Id == OrdreMedicalId
                          join itemFile in this.ServerDbContext.fileMedicals 
                          on itemOrdreMedical.IdFileMedical equals itemFile.Id 
                          select itemFile).FirstOrDefaultAsync();
        }
    }
}
