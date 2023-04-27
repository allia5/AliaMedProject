using DTO;

namespace Client.Services.Foundations.PharmacistService
{
    public interface IPharmacistService
    {
        public Task<InformationPrescriptionResultDto> GetPrescriptionInformation(string CodeQr);
        public Task UpdateStatusPrescriptionLine(string PrescriptionLineId);
    }
}
