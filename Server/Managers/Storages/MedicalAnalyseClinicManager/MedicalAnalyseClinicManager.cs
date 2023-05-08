using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.MedicalsAnalysisClinic;

namespace Server.Managers.Storages.MedicalAnalyseClinicManager
{
    public class MedicalAnalyseClinicManager : IMedicalAnalyseClinicManager
    {
        public  ServerDbContext serverDbContext { get; set; }
        public MedicalAnalyseClinicManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }
        public async Task<MedicalAnalysisClinic> SelectMedicalAnalysisClinicById(Guid MedicalAnalyseClinicId)
        {
            return await (from AnalyseClinicItem in this.serverDbContext.MedicalAnalysisClinic 
                          where AnalyseClinicItem.Id == MedicalAnalyseClinicId 
                          select AnalyseClinicItem).FirstOrDefaultAsync();
        }
    }
}
