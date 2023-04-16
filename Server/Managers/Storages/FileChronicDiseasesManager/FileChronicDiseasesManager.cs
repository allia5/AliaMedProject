using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.FileChronicDisease;

namespace Server.Managers.Storages.FileChronicDiseasesManager
{
    public class FileChronicDiseasesManager : IFileChronicDiseasesManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public FileChronicDiseasesManager(ServerDbContext serverDbContext)
        {
            ServerDbContext = serverDbContext;
        }
        public async Task<FileChronicDiseases> insertFileChronicDisease(FileChronicDiseases fileChronicDiseases)
        {
          var result =  this.ServerDbContext.FileChronicDiseases.Add(fileChronicDiseases);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteFileFileChronicDiseaseByFileId(Guid FileId)
        {
            var FileChronicDeases = await (from item in this.ServerDbContext.FileChronicDiseases where item.IdFile == FileId select item).ToListAsync();
            foreach(var ItemToRemove in FileChronicDeases)
            {
                this.ServerDbContext.FileChronicDiseases.Remove(ItemToRemove);
                await this.ServerDbContext.SaveChangesAsync();
            }
        }
    }
}
