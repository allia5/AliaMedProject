using DTO;

namespace Server.Services.Foundation.RadioMedicalService
{
    public interface IRadioMedicalService
    {
        public Task<InformationRadioResultDto> GetInformationRadioMedicalResult(string Email, string CodeQr);
        public Task<byte[]> GetFileRadioByIdOrdreMedical(string OrdreMedicalId);
    }
}
