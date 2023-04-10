using DTO;
using Server.Models.CabinetMedicals;

namespace Server.Services.Foundation.CabinetMedicalService
{
    public interface ICabinetMedicalService
    {
        public Task<CabinetMedicalDto> SelectCabinetMedicalByAdmin(string Email);
        public Task<CabinetMedicalDto> UpdateCabinetMedicalService(string Email,CabinetMedicalDto cabinetMedicalDto);
    }
}
