using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.AdviceManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
using static Server.Services.Foundation.AdviceMedicalService.AdviceMedicalMapperService;
using Server.Managers.Storages.DoctorManager;

namespace Server.Services.Foundation.AdviceMedicalService
{
    public partial class AdviceMedicalService : IAdviceMedicalService
    {
        public readonly IUserManager userManager;
        public readonly IAdviceManager adviceManager;
        public readonly IFileMedicalManager fileMedicalManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly UserManager<User> _UserManager;
        public readonly IDoctorManager doctorManager;
        public AdviceMedicalService(IDoctorManager doctorManager,IUserManager userManager,IAdviceManager adviceManager,IFileMedicalManager fileMedicalManager,IOrdreMedicalManager ordreMedicalManager,UserManager<User> _UserManager) 
        { 
            this.userManager = userManager;
            this.adviceManager = adviceManager;
            this.ordreMedicalManager   = ordreMedicalManager;
            this._UserManager = _UserManager;
            this.fileMedicalManager = fileMedicalManager;
            this.doctorManager= doctorManager;
        }

        public async Task<List<AdviceMedicalDto>> GetAdviceMedical(string Email, string OrdreMedicalId) =>
            await TryCatch(async () =>
            {
                List<AdviceMedicalDto> ListAdviceMedicalDtos = new List<AdviceMedicalDto>();
                ValidateEntryOnGetAdvicesMedical(Email,OrdreMedicalId);
                var UserAccount = await this._UserManager.FindByEmailAsync(Email);
                ValidateUser(UserAccount);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
                ValidateOrdreMedical(OrdreMedical);
                var Doctor = await this.doctorManager.SelectDoctorById(OrdreMedical.IdDoctor);
                ValidationDoctor(Doctor);
                var userAccountDoctor = await this.userManager.SelectUserByIdDoctor(Doctor.Id);
                ValidateUser(userAccountDoctor);
                var ListAdviceMedical =await  this.adviceManager.SelectAdviceMedicalByIdOrdreMedicalIdUser(DecryptGuid(OrdreMedicalId), UserAccount.Id);
                foreach(var item in ListAdviceMedical)
                {
                    item.StatusViewReceiver = Models.AdviceMedicals.StatusViewReceiver.WatchIt;
                    await this.adviceManager.UpdateAdviceMedical(item);
                }
                var AdvicesMedical = await this.adviceManager.adviceMedicalsByIdOrdreMedicalAsync(DecryptGuid(OrdreMedicalId));
                foreach(var item in AdvicesMedical)
                {
                    var userAccountAdviceMedicalMessage = await this._UserManager.FindByIdAsync(item.transmitterUserId);
                    if(userAccountAdviceMedicalMessage != null)
                    {
                       var AdviceMedicalDto = MapperToAdviceMedical(userAccountAdviceMedicalMessage, item);
                        ListAdviceMedicalDtos.Add(AdviceMedicalDto);
                    }
                    
                }
                return ListAdviceMedicalDtos;


            });
       

    }
}
