using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Models.UserAccount;
using static Server.Services.Foundation.CabinetMedicalService.CabinetMedicalMapperService;

namespace Server.Services.Foundation.CabinetMedicalService
{
    public partial class CabinetMedicalService : ICabinetMedicalService
    {
        public readonly UserManager<User> _userManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public CabinetMedicalService(ICabinetMedicalManager cabinetMedicalManager, UserManager<User> _userManager)
        {
            this._userManager = _userManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
        }
        public async Task<CabinetMedicalDto> SelectCabinetMedicalByAdmin(string Email) =>
        await TryCatch(async () =>
        {

            ValidateEntryString(Email);
            var User = await this._userManager.FindByEmailAsync(Email);
            ValidateUserIsNull(User);
            var CabinetMedical = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(User.Id);
            ValidateCabinetMedicalIsNull(CabinetMedical);
            var Result = MapperToCabinetMedicalDto(CabinetMedical);
            return Result;
        });

        public async Task<CabinetMedicalDto> UpdateCabinetMedicalService(string Email, CabinetMedicalDto cabinetMedicalDto) =>
        await TryCatch(async () =>
        {
            ValidateEntryOnUpdate(Email, cabinetMedicalDto);
            var User = await this._userManager.FindByEmailAsync(Email);
            ValidateUserIsNull(User);
            var CabinetMedical = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(User.Id);
            ValidateCabinetMedicalIsNull(CabinetMedical);
            var newCabinetMedical = cabinetMedicalDto.MapperToNewCabinetMedical(CabinetMedical);
            var ResultUpdate = await this.cabinetMedicalManager.UpdateCabinetMedical(newCabinetMedical);
            ValidateCabinetMedicalIsNull(ResultUpdate);
            var Result = MapperToCabinetMedicalDto(ResultUpdate);
            return Result;
        }
            );







    }
}
