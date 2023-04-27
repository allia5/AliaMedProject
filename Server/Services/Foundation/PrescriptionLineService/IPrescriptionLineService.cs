namespace Server.Services.Foundation.PrescriptionLineService
{
    public interface IPrescriptionLineService
    {
        public Task UpdateStatusPrescriptionLine(string Email, string PrecriptionLineId);
    }
}
