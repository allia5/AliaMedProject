using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.ChronicDiseasesManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileChronicDiseasesManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using Server.Services.Foundation.FileMedicalService;
using static Server.Utility.Utility;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;
using static Server.Services.Foundation.PlanningAppoimentService.PlanningAppoimentMapperService;
using static Server.Services.Foundation.OrdreMedicalService.OrdreMedicalMapperService;
using Server.Managers.Storages.PrescriptionLineManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.AnalyseManager;
using Server.Managers.Storages.PrescriptionManager;
using Server.Models.RadioMedical;

namespace Server.Services.Foundation.OrdreMedicalService
{
    public partial class OrdreMedicalService : IOrdreMedicalService
    {
        public readonly UserManager<User> _UserManager;
       
        public readonly IFileMedicalService FileMedicalService;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly IFileMedicalManager fileMedicalManager;
        public readonly IPlanningAppoimentManager planningAppoimentManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IPrescriptionLineManager PrescriptionLineManager;
        public readonly IPrescriptionManager prescriptionManager;
        public readonly IRadioManager radioManager;
        public readonly IAnalyseManager analyseManager;
        
        public OrdreMedicalService( IPrescriptionManager prescriptionManager, IPrescriptionLineManager PrescriptionLineManager, IRadioManager radioManager, IAnalyseManager analyseManager, ICabinetMedicalManager cabinetMedicalManager, IFileMedicalService FileMedicalService,ISpecialitiesManager specialitiesManager, IOrdreMedicalManager ordreMedicalManager, IWorkDoctorManager workDoctorManager, IPlanningAppoimentManager planningAppoimentManager, UserManager<User> _UserManager, IUserManager userManager, IDoctorManager doctorManager, IFileMedicalManager fileMedicalManager)
        {
            this.fileMedicalManager = fileMedicalManager;
            this.prescriptionManager = prescriptionManager;
            this.PrescriptionLineManager= PrescriptionLineManager;
            this.radioManager= radioManager;
            this.analyseManager=analyseManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.specialitiesManager = specialitiesManager;
            this.userManager = userManager;
            this.doctorManager = doctorManager;
            this.planningAppoimentManager = planningAppoimentManager;
            this._UserManager = _UserManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this.workDoctorManager = workDoctorManager;
            this.FileMedicalService = FileMedicalService;
        }
        public async Task<OrdreMedicalDto> AddOrdreMedicalDto(string Email,OrderMedicalToAddDro orderMedicalToAdd) =>
            await TryCatch(async () =>
            {
                OrdreMedicalDto OrdreMedicalResult = new OrdreMedicalDto();
                ValidateOrdreMedicalOnAdd(orderMedicalToAdd);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var FileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(DecryptGuid(orderMedicalToAdd.FileId));
                validateeFileMedicalIsNull(FileMedical);
                var ListSpecialitiesDoctor = await specialitiesManager.SelectSpecialitiesByIdDoctor(Doctor.Id);
                if (ListSpecialitiesDoctor!=null)
                {
                    var DoctorInformation = MapperToDoctorInformationDto(ListSpecialitiesDoctor, UserAccountDoctor);
                    OrdreMedicalResult.DoctorInformation = DoctorInformation;
                }
               
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid(orderMedicalToAdd.AppointmentId));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment, Doctor);
                var UserAccountPatient = await this._UserManager.FindByIdAsync(Appointment.IdUser);
                ValidateUserIsNull(UserAccountPatient);
                var WorkDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Appointment.IdDoctor, Appointment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctor);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalById(Appointment.IdCabinet);
                ValidateCabinetMedicalIsNull(Cabinet);
                var CabinetInformation = MapperToCabinetInformationAppointmentDto(Cabinet);
                OrdreMedicalResult.cabinetInformation = CabinetInformation;
               var OrdreMedical = MapperToMedicalOrdre(Doctor.Id, Cabinet.Id, orderMedicalToAdd);
                var OddreMedicalInsertResult = await this.ordreMedicalManager.InsertOrdreMedicalAsync(OrdreMedical);
                if(orderMedicalToAdd.Prescription != null)
                {
                    ValidatePrescriptionOnAdd(orderMedicalToAdd.Prescription);
                    var Prescription =MapperToPrescription(orderMedicalToAdd, OddreMedicalInsertResult);
                    Prescription.qrCode = GenerateQRCodeStringFromGuid(Prescription.Id);
                    var PrescriptionInsert = await this.prescriptionManager.InsertPrescriptionAsync(Prescription);
                    foreach(var item in orderMedicalToAdd.Prescription.prescriptionLines)
                    {
                        ValidatePrecriptionLineOnAdd(item);
                       var prescriptionLineInsert = MapperToPrescriptionLine(PrescriptionInsert.Id, item);
                        await this.PrescriptionLineManager.InsertPrescriptionLineAsync(prescriptionLineInsert);
                        PrescriptionInsert.FilePrescription = AjouterStringDansFichierDocx(orderMedicalToAdd.Prescription.PrescriptionFile, item.MedicamentName);
                       
                    }
                    await this.prescriptionManager.UpdatePrescriptionAsync(PrescriptionInsert);
                    OrdreMedicalResult.Lines = orderMedicalToAdd.Prescription.prescriptionLines;
                }
                if (orderMedicalToAdd.RadioToAdd != null)
                {
                    ValidateRadioOnAdd(orderMedicalToAdd.RadioToAdd);
                   var Radio = MapperToRadio(orderMedicalToAdd.RadioToAdd, OddreMedicalInsertResult.Id);
                    Radio.QrCode = GenerateQRCodeStringFromGuid(Radio.Id);
                    Radio.FileRadio = AjouterStringDansFichierDocx(orderMedicalToAdd.RadioToAdd.FileMedicalRadio, orderMedicalToAdd.RadioToAdd.Description);
                    Radio.FileRadio = AjouterStringDansFichierDocx(orderMedicalToAdd.RadioToAdd.FileMedicalRadio, orderMedicalToAdd.RadioToAdd.Instruction);
                    Radio.FileRadio = AjouterStringDansFichierDocx(orderMedicalToAdd.RadioToAdd.FileMedicalRadio, DateTime.Now.ToString());
                    Radio.FileRadio = AjouterCodeQRDansFichierDocx(Radio.FileRadio, Radio.QrCode);
                    var RadioInsert =  await this.radioManager.InsertRadioAsync(Radio);
                    OrdreMedicalResult.ResultFileMedicalRadio = RadioInsert.FileRadio;
                }
                if(orderMedicalToAdd.AnalyseToAdd != null)
                {
                    ValidateAnalyseOnAdd(orderMedicalToAdd.AnalyseToAdd);
                    var Analyse =MapperToAnalyse(orderMedicalToAdd.AnalyseToAdd, OddreMedicalInsertResult.Id);
                    Analyse.QrCode = GenerateQRCodeStringFromGuid(Analyse.Id);
                    Analyse.FileAnalyse = AjouterStringDansFichierDocx(orderMedicalToAdd.AnalyseToAdd.FileMedicalAnalyse,orderMedicalToAdd.AnalyseToAdd.Description);
                    Analyse.FileAnalyse = AjouterStringDansFichierDocx(orderMedicalToAdd.AnalyseToAdd.FileMedicalAnalyse, orderMedicalToAdd.AnalyseToAdd.Instruction);
                    Analyse.FileAnalyse = AjouterStringDansFichierDocx(orderMedicalToAdd.AnalyseToAdd.FileMedicalAnalyse, DateTime.Now.ToString());
                    Analyse.FileAnalyse = AjouterCodeQRDansFichierDocx(Analyse.FileAnalyse, Analyse.QrCode);
                    var AnalyseInsert = await this.analyseManager.InsertAnalyseAsync(Analyse);
                    OrdreMedicalResult.ResultFileMedicalAnalyse = Analyse.FileAnalyse;
                }
                MapperMailRequestDeleteMedicalAppoiment(UserAccountPatient, UserAccountDoctor);
                return OrdreMedicalResult;






            });
       
    }
}
