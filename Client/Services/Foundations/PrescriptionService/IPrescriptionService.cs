namespace Client.Services.Foundations.PrescriptionService
{
    public interface IPrescriptionService
    {
        public Task<Stream> SecritaryGetMedicalFilePrescription(string OrdreId, string CabinetId);
    }
}
