﻿using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.LineAnalyseMedical;
using Server.Models.LineRadioMedical;

namespace Server.Managers.Storages.LineAnalyseMedicalManager
{
    public class LineAnalyseMedicalManager : ILineAnalyseMedicalManager
    {
      
             public ServerDbContext serverDbContext { get; set; }
        public LineAnalyseMedicalManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }

        public async Task<LineAnalyseMedicals> InsertLineAnalyseMedical(LineAnalyseMedicals lineAnalyseMedicals)
        {
            var result =  this.serverDbContext.LineAnalyseMedicals.Add(lineAnalyseMedicals);
            await this.serverDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<LineAnalyseMedicals>> SelectLinesMedicalByIdAnalyseAsync(Guid AnalyseId)
        {
            return await (from Line in this.serverDbContext.LineAnalyseMedicals /*where Line.AnalyseId == AnalyseId*/ select Line).ToListAsync();
        }
    }
    
}
