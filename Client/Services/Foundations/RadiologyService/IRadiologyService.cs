using DTO;

namespace Client.Services.Foundations.RadiologyService
{
    public interface IRadiologyService
    {
        public Task<InformationRadioResultDto> GetInformationRadioResultAsync(string CodeQr);
        public Task PostRadioMedicalResult(RadioResultToAddDto RadioResultToAddDto);
    }
}
