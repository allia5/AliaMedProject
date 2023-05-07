using DTO;
using Server.Models.Prescriptions;

namespace Server.Services.Foundation.PrescriptionService
{
    public interface IPrescriptionService
    {
        public Task<InformationPrescriptionResultDto> GetPrescriptionInformation(string Email, string Code);
        public Task<byte[]> GetFilePrescriptionByIdOrdreMedical(string Email,string OrdreMedicalId,string CabinetId);
        public Task<byte[]> PatientGetFilePrescriptionByIdOrdreMedical(string Email, string OrdreMedicalId);

    }
}
