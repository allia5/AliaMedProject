using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.ChronicDiseases;
using Server.Models.FileChronicDisease;

namespace Server.Managers.Storages.ChronicDiseasesManager
{
    public class ChronicDiseasesManager : IChronicDiseasesManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public ChronicDiseasesManager(ServerDbContext serverDbContext)
        {
            ServerDbContext = serverDbContext;
        }

        public async Task<List<ChronicDisease>> SelectAllChronicDiseasesAsync()
        {
            return await this.ServerDbContext.chronicDiseases.ToListAsync();    
        }

        public async Task<ChronicDisease> SelectChronicDiseasesByIdAsync(int Id)
        {
            return await this.ServerDbContext.chronicDiseases.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<FileChronicDiseases>> SelectChronicDiseasesByIdMedicalFileAsync(Guid FileId)
        {
         return await (from chronicDisease in this.ServerDbContext.FileChronicDiseases where chronicDisease.IdFile == FileId select chronicDisease ).ToListAsync();
        }
    }
}
