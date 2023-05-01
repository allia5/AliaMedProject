namespace Client.Services.Foundations.PrescriptionService
{
    public interface IPrescriptionService
    {
        public Task<Stream> GetMedicalFilePrescription(string OrdreId);
    }
}
