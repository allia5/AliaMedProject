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
using Server.Controllers;

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

        public async Task<List<AdviceMedicalDto>> DoctorGetAdviceMedical(string Email, string OrdreMedicalId) =>
            await TryCatch(async () =>
            {
                List<AdviceMedicalDto> ListAdviceMedicalDtos = new List<AdviceMedicalDto>();
                ValidateEntryOnGetAdvicesMedical(Email, OrdreMedicalId);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUser(UserAccountDoctor);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
                ValidateOrdreMedical(OrdreMedical);
                var Doctor = await this.doctorManager.SelectDoctorById(OrdreMedical.IdDoctor);
                ValidationDoctor(Doctor);
                var UserAccountDoctorOrdreMedical = await this.userManager.SelectUserByIdDoctor(Doctor.Id);
                ValidationSimilarity(UserAccountDoctor, UserAccountDoctorOrdreMedical);
                var fileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(OrdreMedical.IdFileMedical);
                ValidateFileMedicalIsNull(fileMedical);
                var UserAccountPatientFileMedical = await this._UserManager.FindByIdAsync(fileMedical.IdUser);
                ValidateUser(UserAccountPatientFileMedical);
                var ListAdviceMedical = await this.adviceManager.adviceMedicalsByIdOrdreMedicalAsync(DecryptGuid(OrdreMedicalId));
                foreach (var item in ListAdviceMedical)
                {
                    if (item.transmitterUserId != UserAccountDoctor.Id)
                    {
                        item.StatusViewReceiver = Models.AdviceMedicals.StatusViewReceiver.WatchIt;
                        await this.adviceManager.UpdateAdviceMedical(item);
                    }
                }
                foreach (var item in ListAdviceMedical)
                {
                    var userAccountAdviceMedicalMessage = await this._UserManager.FindByIdAsync(item.transmitterUserId);
                    if (userAccountAdviceMedicalMessage != null)
                    {
                        var AdviceMedicalDto = MapperToAdviceMedical(userAccountAdviceMedicalMessage, item);
                        ListAdviceMedicalDtos.Add(AdviceMedicalDto);
                    }

                }
                return ListAdviceMedicalDtos;
            });

        public async Task<List<AdviceMedicalDto>> PatientGetAdviceMedical(string Email, string OrdreMedicalId) =>
            await TryCatch(async () =>
            {
                List<AdviceMedicalDto> ListAdviceMedicalDtos = new List<AdviceMedicalDto>();
                ValidateEntryOnGetAdvicesMedical(Email,OrdreMedicalId);
                var UserAccountPatient = await this._UserManager.FindByEmailAsync(Email);
                ValidateUser(UserAccountPatient);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
                ValidateOrdreMedical(OrdreMedical);
                var fileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(OrdreMedical.IdFileMedical);
                ValidateFileMedicalIsNull(fileMedical);
                var UserAccountPatientFileMedical = await this._UserManager.FindByIdAsync(fileMedical.IdUser);
                ValidateUser(UserAccountPatientFileMedical);
                ValidationSimilarity(UserAccountPatient, UserAccountPatientFileMedical);
                var Doctor = await this.doctorManager.SelectDoctorById(OrdreMedical.IdDoctor);
                ValidationDoctor(Doctor);
                var userAccountDoctor = await this.userManager.SelectUserByIdDoctor(Doctor.Id);
                ValidateUser(userAccountDoctor);
                var ListAdviceMedical =await  this.adviceManager.adviceMedicalsByIdOrdreMedicalAsync(DecryptGuid(OrdreMedicalId));
                foreach(var item in ListAdviceMedical)
                {
                    if(item.transmitterUserId != UserAccountPatient.Id)
                    {
                        item.StatusViewReceiver = Models.AdviceMedicals.StatusViewReceiver.WatchIt;
                        await this.adviceManager.UpdateAdviceMedical(item);
                    }
                }
                foreach(var item in ListAdviceMedical)
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

        public async Task PostNewAdviceMedicalDoctor(string Email, MedicalAdviceToAddDto medicalAdviceToAddDto) =>
            await TryCatch_(async () =>
            {
                ValidateEntryOnPostAdvicesMedical(Email, medicalAdviceToAddDto);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUser(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(UserAccountDoctor.Id);
                ValidationDoctor(Doctor);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(medicalAdviceToAddDto.OrdreMedicalId));
                ValidateOrdreMedical(OrdreMedical);
                var fileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(OrdreMedical.IdFileMedical);
                ValidateFileMedicalIsNull(fileMedical);
                var UserAccountPatientFileMedical = await this._UserManager.FindByIdAsync(fileMedical.IdUser);
                ValidateUser(UserAccountPatientFileMedical);
                var UserAccountDoctorOrdreMedical = await this.userManager.SelectUserByIdDoctor(OrdreMedical.IdDoctor);
                ValidationSimilarity(UserAccountDoctor, UserAccountDoctorOrdreMedical);

                var AdviceMedical = MapperToAdviceMedical(medicalAdviceToAddDto, UserAccountDoctor.Id, OrdreMedical.Id);
                await this.adviceManager.InsertAdviceMedical(AdviceMedical);
            });

        public async Task PostNewAdviceMedicalPatient(string Email, MedicalAdviceToAddDto medicalAdviceToAddDto) =>
            await TryCatch_(async () =>
            {
                ValidateEntryOnPostAdvicesMedical(Email, medicalAdviceToAddDto);
                var UserAccountPatient = await this._UserManager.FindByEmailAsync(Email);
                ValidateUser(UserAccountPatient);
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(medicalAdviceToAddDto.OrdreMedicalId));
                ValidateOrdreMedical(OrdreMedical);
                var fileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(OrdreMedical.IdFileMedical);
                ValidateFileMedicalIsNull(fileMedical);
                var UserAccountPatientFileMedical = await this._UserManager.FindByIdAsync(fileMedical.IdUser);
                ValidateUser(UserAccountPatientFileMedical);
                ValidationSimilarity(UserAccountPatient, UserAccountPatientFileMedical);
                var Doctor = await this.doctorManager.SelectDoctorById(OrdreMedical.IdDoctor);
                ValidationDoctor(Doctor);
                var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(Doctor.Id);
                ValidateUser(UserAccountDoctor);
                var AdviceMedical = MapperToAdviceMedical(medicalAdviceToAddDto, UserAccountPatient.Id, OrdreMedical.Id);
                await this.adviceManager.InsertAdviceMedical(AdviceMedical);
            });
       
    }
}
