﻿using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.AnalyseManager;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.PrescriptionManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.SecretaryManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using Server.Services.Foundation.FileMedicalService;
using static Server.Utility.Utility;
using static Server.Services.Foundation.PlanningAppoimentService.PlanningAppoimentMapperService;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;
using static Server.Services.Foundation.RadioMedicalService.RadioMedicalMapperService;
using Server.Models.LineRadioMedical;
using System.Collections.Generic;
using Server.Managers.Storages.LineRadioMedicalManager;

namespace Server.Services.Foundation.RadioMedicalService
{
    public partial class RadioMedicalService : IRadioMedicalService
    {
        public readonly IFileMedicalManager FileMedicalManager;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly UserManager<User> _UserManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly IRadioManager radioManager;
        public readonly IFileChronicDiseasesManager fileChronicDiseasesManager;
        public readonly IChronicDiseasesManager chronicDiseasesManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public readonly ILineRadioMedicalManager lineRadioMedicalManager;
        public readonly ISecretaryManager secretaryManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IConfiguration configuration;

        public RadioMedicalService(IConfiguration configuration,ICabinetMedicalManager cabinetMedicalManager, ISecretaryManager secretaryManager, ILineRadioMedicalManager lineRadioMedicalManager, ISpecialitiesManager specialitiesManager, IFileChronicDiseasesManager fileChronicDiseasesManager, IChronicDiseasesManager chronicDiseasesManager, UserManager<User> _UserManager, IFileMedicalManager FileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, IPlanningAppoimentManager planningAppoimentManager, IOrdreMedicalManager ordreMedicalManager, IRadioManager radioManager)
        {
            this.configuration = configuration;
            this.secretaryManager = secretaryManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.lineRadioMedicalManager = lineRadioMedicalManager;
            this.specialitiesManager = specialitiesManager;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this._UserManager = _UserManager;
            this.FileMedicalManager = FileMedicalManager;
            this.radioManager = radioManager;
            this.chronicDiseasesManager = chronicDiseasesManager;
            this.fileChronicDiseasesManager = fileChronicDiseasesManager;

        }
        public async Task<byte[]> SecritaryGetFileRadioByIdOrdreMedical(string Email, string OrdreMedicalId, string CabinetId) =>
        await TryCatch(async () =>
        {
            ValidateEntryOnGetFileRadio(Email, OrdreMedicalId, CabinetId);
            var UserAccountSecritary = await this._UserManager.FindByEmailAsync(Email);
            ValidateUserIsNull(UserAccountSecritary);
            var Secritary = await this.secretaryManager.SelectSecretaryByIdUserIdCabinet(UserAccountSecritary.Id, DecryptGuid(CabinetId));
            ValidateSecritary(Secritary);
            var CabinetMedical = await this.cabinetMedicalManager.SelectCabinetMedicalById(Secritary.IdCabinetMedical);
            ValidateCabinetMedical(CabinetMedical);
            var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
            ValidateOrdreMedicalIsNull(OrdreMedical);
            var FileRadio = await this.radioManager.SelectRadioByIdMedicalOrdre(DecryptGuid(OrdreMedicalId));
            ValidateRadioIsNull(FileRadio);
            return DecryptFile(FileRadio.FileRadio);
        });
        public async Task<InformationRadioResultDto> GetInformationRadioMedicalResult(string Email, string CodeQr) =>

            await TryCatch(async () =>
            {
                List<string> ListChronicDeasses = new List<string>();
                List<string> ListSpecialitiesDoctor = new List<string>();
                List<LineRadioMedicalResultDto> LinesResultRadio = new List<LineRadioMedicalResultDto>();

                ValidateEntryOnGetRadioInformation(Email, CodeQr);
                var UserAcccountRadiology = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAcccountRadiology);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAcccountRadiology.Id);
                ValidationDoctorIsNull(Doctor);
                var codeQr = DecryptString(CodeQr, configuration["KeysQrCod:KeyOrdreMedical"]);
                var Radio = await this.radioManager.SelectRadioByCodeAsync(codeQr);
                ValidateRadioIsNull(Radio);
                var LinesRadio = await this.lineRadioMedicalManager.SelectAllLineMedicalByIdRadio(Radio.Id);
                LinesRadio = LinesRadio.Where(e => e.Status == Models.RadioMedical.StatusRadio.notValidate).ToList();
                var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(Radio.IdOrdreMedical);
                ValidateOrdreMedical(OrdreMedical);
                var FileMedical = await this.FileMedicalManager.SelectFileMedicalByIdAsync(OrdreMedical.IdFileMedical);
                ValidateFileMedicalIsNull(FileMedical);
                var FileChronicDeases = await this.chronicDiseasesManager.SelectChronicDiseasesByIdMedicalFileAsync(FileMedical.Id);
                foreach (var Item in FileChronicDeases)
                {
                    var chronicDeasses = await this.chronicDiseasesManager.SelectChronicDiseasesByIdAsync(Item.IdChronicDisease);
                    ListChronicDeasses.Add(chronicDeasses.NameChronicDiseases);
                }
                var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
                validationPatientIsNull(UserAccountPatient);
                var UserAccountDoctor = await this.userManager.SelectUserByIdDoctor(FileMedical.IdDoctor);
                validationPatientIsNull(UserAccountDoctor);
                var specialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(FileMedical.IdDoctor);
                foreach (var item in LinesRadio)
                {
                    var LineRadioMapper = MapperTolineRadioMedicalResultDto(item);
                    LinesResultRadio.Add(LineRadioMapper);
                }
                var InformationPatient = MppperToPatientInformationDto(UserAccountPatient);
                var informationDoctor = MapperToDoctorInformationDto(specialities, UserAccountDoctor);
                var FileInformation = MapperToFileInformationDto(FileMedical, ListChronicDeasses);

                var radioInformation = MapperToRadioInformation(Radio, LinesResultRadio, OrdreMedical);
                var Result = MapperToInformationRadioResult(radioInformation, FileInformation, InformationPatient, informationDoctor);
                return Result;

            });

        public async Task<byte[]> PatientGetFileRadioByIdOrdreMedical(string Email, string OrdreMedicalId) =>
        await TryCatch(async () =>
        {
            ValidateEntryOnGetFileRadioByPatient(Email, OrdreMedicalId);
            var UserAccount = await this._UserManager.FindByEmailAsync(Email);
            ValidateUserIsNull(UserAccount);
            var OrdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdAsync(DecryptGuid(OrdreMedicalId));
            ValidateOrdreMedical(OrdreMedical);
            var DoctorAccount = await this.userManager.SelectUserByIdDoctor(OrdreMedical.IdDoctor);
            ValidateUserIsNull(DoctorAccount);
            var Doctor = await this.doctorManager.SelectDoctorByIdUser(DoctorAccount.Id);
            ValidationDoctorIsNull(Doctor);
            var FileRadio = await this.radioManager.SelectRadioByIdMedicalOrdre(DecryptGuid(OrdreMedicalId));
            ValidateRadioIsNull(FileRadio);
            return DecryptFile( FileRadio.FileRadio);
        });

    }
}