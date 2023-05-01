namespace Client.Services.Foundations.AnalyseMedicalService
{
    public interface IAnalyseMedicalService
    {
        public Task<Stream> GetMedicalFileAnalyse(string OrdreId);
    }
}
