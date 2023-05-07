namespace Client.Services.Foundations.AnalyseMedicalService
{
    public interface IAnalyseMedicalService
    {
        public Task<Stream> SecritaryGetMedicalFileAnalyse(string OrdreId, string CabinetId);
    }
}
