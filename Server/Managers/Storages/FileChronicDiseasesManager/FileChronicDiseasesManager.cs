﻿using Server.Data;
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
            this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
