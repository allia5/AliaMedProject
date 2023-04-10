using DTO;

namespace Client.Services.Foundations.CabinetMedicalService
{
    public interface ICabinetMedicalService
    {
        public Task<CabinetMedicalDto> GetInformationFromCabinetMedical();
        public Task<CabinetMedicalDto> UpdateInformationCabinetMedical(CabinetMedicalDto cabinetMedicalDto);
    }
}
