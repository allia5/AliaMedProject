using Server.Models.MedicalsAnalysisClinic;

namespace Server.Managers.Storages.MedicalAnalyseClinicManager
{
    public interface IMedicalAnalyseClinicManager
    {
        public Task<MedicalAnalysisClinic> SelectMedicalAnalysisClinicById(Guid MedicalAnalyseClinicId);
    }
}
