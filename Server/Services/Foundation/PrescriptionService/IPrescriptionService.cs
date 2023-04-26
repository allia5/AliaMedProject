using DTO;
using Server.Models.Prescriptions;

namespace Server.Services.Foundation.PrescriptionService
{
    public interface IPrescriptionService
    {
        public Task<InformationPrescriptionResultDto> GetPrescriptionInformation(string Email, string Code);
    }
}
