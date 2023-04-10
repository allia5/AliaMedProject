using Server.Models.CabinetMedicals;

namespace Server.Managers.Storages.CabinetMedicalManager
{
    public interface ICabinetMedicalManager
    {
        public Task<CabinetMedical> SelectCabinetMedicalByUserId(string UserId);
        public Task<CabinetMedical> SelectCabinetMedicalById(Guid CabinetId);
        public Task<CabinetMedical> UpdateCabinetMedical(CabinetMedical cabinetMedical);
        public Task<CabinetMedical> SelectCabinetMedicalOpenById(Guid CabinetId);
    }
}
