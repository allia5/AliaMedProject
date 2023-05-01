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
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.AnalyseManager;
using Server.Managers.Storages.PrescriptionManager;
using Server.Models.RadioMedical;
using Server.Models.Analyse;
using Server.Managers.Storages.SecretaryManager;
using Server.Services.Foundation.MailService;
using Server.Models.Prescriptions;
using Server.Managers.Storages.LineRadioMedicalManager;
using Server.Managers.Storages.LineAnalyseMedicalManager;
using Server.Managers.Storages.LinePrescriptionMedicalManager;
using Server.Managers.Storages.AdviceManager;

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
        public readonly ILinePrescriptionMedicalManager PrescriptionLineManager;
        public readonly IPrescriptionManager prescriptionManager;
        public readonly IRadioManager radioManager;
        public readonly IAnalyseManager analyseManager;
        public readonly ISecretaryManager secretaryManager;
        public readonly IMailService mailService;
        public readonly ILineRadioMedicalManager lineRadioMedicalManager;
        public readonly ILineAnalyseMedicalManager lineAnalyseMedicalManager;
        public readonly IAdviceManager adviceManager;
        
        public OrdreMedicalService(IAdviceManager adviceManager,ILineRadioMedicalManager lineRadioMedicalManager,ILineAnalyseMedicalManager lineAnalyseMedicalManager,IMailService mailService,ISecretaryManager secretaryManager, IPrescriptionManager prescriptionManager, ILinePrescriptionMedicalManager PrescriptionLineManager, IRadioManager radioManager, IAnalyseManager analyseManager, ICabinetMedicalManager cabinetMedicalManager, IFileMedicalService FileMedicalService,ISpecialitiesManager specialitiesManager, IOrdreMedicalManager ordreMedicalManager, IWorkDoctorManager workDoctorManager, IPlanningAppoimentManager planningAppoimentManager, UserManager<User> _UserManager, IUserManager userManager, IDoctorManager doctorManager, IFileMedicalManager fileMedicalManager)
        {
            this.adviceManager = adviceManager;
            this.doctorManager = doctorManager;
            this.lineAnalyseMedicalManager= lineAnalyseMedicalManager;
            this.lineRadioMedicalManager= lineRadioMedicalManager;
            this.mailService=mailService;
            this.secretaryManager = secretaryManager;
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

        public async Task<List<InformationOrderMedicalSecritary>> SelectAllMedicalOrdreSecritary(string Email, KeysAppoimentInformationSecretary keysAppoimentInformationSecritary) =>
           await TryCatch(async () =>
           {
               List<InformationOrderMedicalSecritary> ListinformationOrderMedicalSecritaries = new List<InformationOrderMedicalSecritary>();
               ValidateEntryOnGetAllAppoimentPatientSecretary(Email, keysAppoimentInformationSecritary);
               var UserAccountSecritary = await this._UserManager.FindByEmailAsync(Email);
               ValidateUserIsNull(UserAccountSecritary);
               var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(DecryptGuid(keysAppoimentInformationSecritary.IdDoctor).ToString());
               ValidationDoctorIsNull(Doctor);
               var Secritary = await this.secretaryManager.SelectSecretaryByIdUserIdCabinet(UserAccountSecritary.Id,DecryptGuid( keysAppoimentInformationSecritary.CabinetId));
               ValidateSecritary(Secritary);
               var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(Secritary.IdCabinetMedical);
               ValidateCabinetMedicalIsNull(Cabinet);
               var workDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(Doctor.Id,Cabinet.Id);
               ValidateWorkDoctorIsNull(workDoctor);
               var ListOrderMedical = await this.ordreMedicalManager.SelectAllMedicalOrderByIdCabinetByIdDoctorByDate(Cabinet.Id,Doctor.Id,keysAppoimentInformationSecritary.DateAppoiment);
               foreach(var ItemOrdre in ListOrderMedical)
               {
                   var FileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(ItemOrdre.IdFileMedical);
                   if(FileMedical != null)
                   {
                       var UserAccountPatient = await this._UserManager.FindByIdAsync(FileMedical.IdUser);
                       var InformationPatientDto = MppperToPatientInformationDto(UserAccountPatient);
                       var FileMedicalInformationDto = MapperToInformationFileMedical(FileMedical);
                   
                
                   var OrdreMedicalInformationDto = mapperToInformationOrdreMedical(ItemOrdre);
                       var fileRadio = await this.radioManager.SelectRadioByIdMedicalOrdre(ItemOrdre.Id);
                       if(fileRadio?.FileRadio == null)
                       {
                           OrdreMedicalInformationDto.FileRadio = false;
                       }
                       else
                       {
                           OrdreMedicalInformationDto.FileRadio = true;
                       }
                       var filePrescription = await this.prescriptionManager.SelectPrescriptionByIdMedicalOrdreAsync(ItemOrdre.Id);
                       if (filePrescription?.FilePrescription == null)
                       {
                           OrdreMedicalInformationDto.FilePrescription = false;
                       }
                       else
                       {
                           OrdreMedicalInformationDto.FilePrescription = true;
                       }
                       var FileAnalyse = await this.analyseManager.SelectAnalyseByOrdreMedicalId(ItemOrdre.Id);
                       if (FileAnalyse?.FileAnalyse == null)
                       {
                           OrdreMedicalInformationDto.fileAnalyse = false;
                       }
                       else
                       {
                           OrdreMedicalInformationDto.fileAnalyse = true;
                       }

                       var InformationOrdreMedicalItemDto = MapperToInformationOrderMedicalSecritary(InformationPatientDto, FileMedicalInformationDto, OrdreMedicalInformationDto);
                       ListinformationOrderMedicalSecritaries.Add(InformationOrdreMedicalItemDto);
                   }
                 
                   
               }

               return ListinformationOrderMedicalSecritaries;


           });
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
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(Appointment.IdCabinet);
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
                    PrescriptionInsert.FilePrescription = AddInfromationFileToToPdf(PrescriptionInsert.FilePrescription, FileMedical);
                   var stringToAddFile = "";
                    var ItemsAdd = "name Medicament" + "......................................................." + "Quantity";
                    float k = (float)0.02;
                    PrescriptionInsert.FilePrescription = AddTextToPdf(PrescriptionInsert.FilePrescription, ItemsAdd, (float)k);
                     k = (float)(k + 0.02);
                    foreach (var item in orderMedicalToAdd.Prescription.prescriptionLines)
                    {
                        ValidatePrecriptionLineOnAdd(item);
                       var prescriptionLineInsert = MapperToPrescriptionLine(PrescriptionInsert.Id, item);
                        await this.PrescriptionLineManager.InsertPrescriptionLineAsync(prescriptionLineInsert);
                         stringToAddFile =item.MedicamentName + "..............................................................." + item.Quantity + Environment.NewLine;
                        PrescriptionInsert.FilePrescription = AddTextToPdf(PrescriptionInsert.FilePrescription, stringToAddFile,k);
                        k = (float)(k + 0.02);
                    }

                    PrescriptionInsert.FilePrescription = AddInfromationDoctorToToPdf(PrescriptionInsert.FilePrescription, UserAccountDoctor, k);
                    PrescriptionInsert.FilePrescription = InsertCodeQrIntoPdf(PrescriptionInsert.FilePrescription, FileMedical.MedicalIdentification, 100, 0);
                    PrescriptionInsert.FilePrescription = InsertCodeQrIntoPdf(PrescriptionInsert.FilePrescription,EncryptString( Prescription.qrCode,"AJFNJjfjJZFJNdzj=="),0,0);
                    await this.prescriptionManager.UpdatePrescriptionAsync(PrescriptionInsert);
                    OrdreMedicalResult.Lines = orderMedicalToAdd.Prescription.prescriptionLines;
                }
                if (orderMedicalToAdd.RadioToAdd != null)
                {
                    ValidateRadioOnAdd(orderMedicalToAdd.RadioToAdd);
                   var Radio = MapperToRadio(orderMedicalToAdd.RadioToAdd, OddreMedicalInsertResult.Id);
                    Radio.QrCode = GenerateQRCodeStringFromGuid(Radio.Id);
                    var RadioInser = await this.radioManager.InsertRadioAsync(Radio);
                    Radio.FileRadio = AddInfromationFileToToPdf(Radio.FileRadio, FileMedical);
                    float k = (float)0.02;
                    foreach (var item in orderMedicalToAdd.RadioToAdd.LineRadioMedicals)
                    {
                        ValidateRadioLineOnAdd(item);
                        var RadioLineInsert = MapperToRadioLine(Radio.Id, item);
                        await this.lineRadioMedicalManager.InsertLineRadioMedical(RadioLineInsert);
                       var stringToAddFile = item.Description + "........." + item.Instruction + Environment.NewLine;
                        Radio.FileRadio = AddTextToPdf(Radio.FileRadio, stringToAddFile, k);
                        k = (float)(k + 0.02);
                    }
                   
                    Radio.FileRadio = AddInfromationDoctorToToPdf(Radio.FileRadio, UserAccountDoctor, (float)k);
                    Radio.FileRadio = InsertCodeQrIntoPdf(Radio.FileRadio, FileMedical.MedicalIdentification, 100, 0);
                    Radio.FileRadio = InsertCodeQrIntoPdf(Radio.FileRadio,EncryptString(Radio.QrCode,"AJFNJjfjJZFJNdzj=="), 0, 0);
                    var RadioInsert =  await this.radioManager.UpdateRadioAsync(Radio);
                    OrdreMedicalResult.ResultFileMedicalRadio = RadioInsert.FileRadio;
                }
                if(orderMedicalToAdd.AnalyseToAdd != null)
                {
                    ValidateAnalyseOnAdd(orderMedicalToAdd.AnalyseToAdd);
                    var Analyse =MapperToAnalyse(orderMedicalToAdd.AnalyseToAdd, OddreMedicalInsertResult.Id);
                    Analyse.QrCode = GenerateQRCodeStringFromGuid(Analyse.Id);
                    var AnalyseInsert = await this.analyseManager.InsertAnalyseAsync(Analyse);
                  
                    Analyse.FileAnalyse = AddInfromationFileToToPdf(Analyse.FileAnalyse, FileMedical);
                    float k = (float)0.02;
                    foreach (var item in orderMedicalToAdd.AnalyseToAdd.LineAnalyseMedicals)
                    {
                        ValidateAnalyseLineOnAdd(item);
                        var AnalyseLine = MapperToAnalyseLine(Analyse.Id, item);
                        await this.lineAnalyseMedicalManager.InsertLineAnalyseMedical(AnalyseLine);
                        var stringToAddFile = item.Description + "................." + item.Instruction + Environment.NewLine;
                        Analyse.FileAnalyse = AddTextToPdf(Analyse.FileAnalyse, stringToAddFile, k);
                        k = (float)(k + 0.02);
                    }
                
                    Analyse.FileAnalyse = AddInfromationDoctorToToPdf(Analyse.FileAnalyse, UserAccountDoctor, (float)k);
                    Analyse.FileAnalyse = InsertCodeQrIntoPdf(Analyse.FileAnalyse, FileMedical.MedicalIdentification, 100, 0);
                    Analyse.FileAnalyse = InsertCodeQrIntoPdf(Analyse.FileAnalyse,EncryptString( Analyse.QrCode, "AJFNJjfjJZFJNdzj=="), 0, 0);
                    var NewAnalyseInsert = await this.analyseManager.UpdateAnalyseAsync(Analyse);
                    OrdreMedicalResult.ResultFileMedicalAnalyse = Analyse.FileAnalyse;
                }
                MapperMailRequestDeleteMedicalAppoiment(UserAccountPatient, UserAccountDoctor);
                return OrdreMedicalResult;






            });

        public async Task UpdateStatusOrdreMedicalService(string Email, UpdateOrdreMedicalDto updateOrdreMedicalDto) =>
            await TryCatch(async () =>
            {
                ValidateEntryOnUpdateStatusSecritary(Email, updateOrdreMedicalDto);
                var UserAccountSecritary = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountSecritary);
                var Doctor = await this.doctorManager.SelectDoctorByIdUserWithStatusActive(DecryptGuid(updateOrdreMedicalDto.DoctorId).ToString());
                ValidationDoctorIsNull(Doctor);
                var Cabinet = await this.cabinetMedicalManager.SelectCabinetMedicalOpenById(DecryptGuid(updateOrdreMedicalDto.CabinetId));
                ValidateCabinetMedicalIsNull(Cabinet);
                var Secritary = await this.secretaryManager.SelectSecretaryByIdUserIdCabinet(UserAccountSecritary.Id, Cabinet.Id);
                ValidateSecritary(Secritary);
                var WorDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Doctor.Id, Cabinet.Id);
                ValidateWorkDoctorIsNull(WorDoctor);
                var OdreMedical = await this.ordreMedicalManager.SelectMedicalOrdreByIdByIdDoctorByIdCabinet(DecryptGuid(updateOrdreMedicalDto.OrdreMedicalId), Doctor.Id, Cabinet.Id);
                ValidateOrdreMedicalIsNull(OdreMedical);
                var newOrdreMedical = MapperToNewOrdreMedical(OdreMedical, updateOrdreMedicalDto, Secritary);
                await this.ordreMedicalManager.UpdateMedicalOrdreAsync(OdreMedical);
                var FileMedicalPatient = await this.fileMedicalManager.SelectFileMedicalByIdAsync(OdreMedical.IdFileMedical);
                if(FileMedicalPatient != null)
                {
                    var AccountPatient = await this._UserManager.FindByIdAsync(FileMedicalPatient.IdUser);
                    var AccountDoctor = await this.userManager.SelectUserByIdDoctor(Doctor.Id);
                    if(AccountPatient != null && AccountDoctor != null)
                    {
                        var mailRequest =MapperToMailRequestUpdateStatusOrdreMedical(AccountPatient, AccountDoctor);
                        await this.mailService.SendEmailNotification(mailRequest);

                    }
                    
                }


            });

        public async Task<MedicalFileArchiveDto> GetListOrdreFileMedical(string Email, string AppointmentId, string FileId) =>
            await TryCatch_(async () =>
            {
                MedicalFileArchiveDto medicalFileArchiveDto = new MedicalFileArchiveDto();
                List<MedicalOrdresDto> medicalOrdres = new List<MedicalOrdresDto>();
                MedicalOrdreDetails MedicalOrdreDetails = new MedicalOrdreDetails();
                List<PrescriptionLineInformationDto> prescriptionLinesInformationDto = new List<PrescriptionLineInformationDto>();
                List<RadioLineInformationDto> radioLinesInformationDto = new List<RadioLineInformationDto>();
                List<AnalyseLineInformationDto> analyseLinesInformationDto = new List<AnalyseLineInformationDto>();
                ValidateEntryOnGetArchiveOrdreFileMedical(Email,FileId,AppointmentId);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var Doctor = await this.doctorManager.SelectDoctorByIdUser(UserAccountDoctor.Id);
                ValidationDoctorIsNull(Doctor);
                var Appointment = await this.planningAppoimentManager.SelectMedicalPlannigById(DecryptGuid(AppointmentId));
                ValidatePlanningIsNull(Appointment);
                ValidateAppointmentWithDoctor(Appointment, Doctor);
                var WorkDoctor = await this.workDoctorManager.SelectWorkDoctorByIdDoctorIdCabinetWithStatusWorkActive(Doctor.Id,Appointment.IdCabinet);
                ValidateWorkDoctorIsNull(WorkDoctor);
                var fileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(DecryptGuid(FileId));
                validateeFileMedicalIsNull(fileMedical);
                medicalFileArchiveDto.informationFileMedical = MapperToInformationFileMedical(fileMedical);
                var ListOrdreMedical = await this.ordreMedicalManager.SelectListOrdreMedicalByIdMedicalFile(fileMedical.Id);
                ListOrdreMedical = ListOrdreMedical.Where(e => e.IdDoctor == Doctor.Id || (e.Visibility == Models.MedicalOrder.StatusVisibility.Public && e.Status == Models.MedicalOrder.StatuseOrdreMedical.validate)).ToList();
                foreach(var ItemOrdreMedical in ListOrdreMedical)
                {
                    var UserAccountDoctorOrdreMedical = await this.userManager.SelectUserByIdDoctor(ItemOrdreMedical.IdDoctor);
                    if(UserAccountDoctorOrdreMedical != null && UserAccountDoctorOrdreMedical.Status != UserStatus.Deactivated)
                    {
                        var Specialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(ItemOrdreMedical.IdDoctor);
                        var InformationDoctor = MapperToDoctorInformationDto(Specialities, UserAccountDoctorOrdreMedical);
                        var Prescription = await this.prescriptionManager.SelectPrescriptionByIdMedicalOrdreAsync(ItemOrdreMedical.Id);
                        if(Prescription != null)
                        {
                            var ListPrescliptionLine = await this.PrescriptionLineManager.SelectLinePrescriptionByIdPrescription(Prescription.Id);
                            foreach(var Line in ListPrescliptionLine)
                            {
                                var PrescriptionLinInformation = MapperToPrescriptionLineInformationDto(Line);
                                prescriptionLinesInformationDto.Add(PrescriptionLinInformation);
                            }
                        }
                        else
                        {
                            prescriptionLinesInformationDto = null;
                        }
                        var RadioMedical = await this.radioManager.SelectRadioByIdMedicalOrdre(ItemOrdreMedical.Id);
                        if(RadioMedical != null)
                        {
                            var ListLineRadioMedical = await this.lineRadioMedicalManager.SelectAllLineMedicalByIdRadio(RadioMedical.Id);
                         
                            foreach (var LineRadio in ListLineRadioMedical)
                            {
                                var RadioLineInformation = MapperToRadioLineInformationDto(LineRadio);
                                radioLinesInformationDto.Add(RadioLineInformation);
                            }
                        }
                        else
                        {
                            radioLinesInformationDto = null;
                        }
                        var AnalyseMedical = await this.analyseManager.SelectAnalyseByOrdreMedicalId(ItemOrdreMedical.Id);
                        if(AnalyseMedical != null)
                        {
                            var ListLineAnalyseMedical = await this.lineAnalyseMedicalManager.SelectLinesMedicalByIdAnalyseAsync(AnalyseMedical.Id);
                            foreach(var LineAnalyse in ListLineAnalyseMedical)
                            {
                                var LineAnalyseInformation =MapperToAnalyseLineInformationDto(LineAnalyse);
                                analyseLinesInformationDto.Add(LineAnalyseInformation);

                            }
                        }
                        else
                        {
                            analyseLinesInformationDto = null;
                        }
                        MedicalOrdreDetails = MapperToMedicalOrdreDetails(ItemOrdreMedical, analyseLinesInformationDto, radioLinesInformationDto, prescriptionLinesInformationDto);
                        var medicalOrdre = MapperToMedicalOrdresDto(MedicalOrdreDetails, InformationDoctor);
                        medicalOrdres.Add(medicalOrdre);
                        analyseLinesInformationDto = new List<AnalyseLineInformationDto>();
                        radioLinesInformationDto = new List<RadioLineInformationDto>();
                        prescriptionLinesInformationDto = new List<PrescriptionLineInformationDto>();


                    }

                    
                }
                medicalFileArchiveDto.medicalOrdres = medicalOrdres;

                return medicalFileArchiveDto;




            });
        
        public async Task<MedicalFileArchivePatientDto> GetMedecalArchivePatient(string Email, string FileId) =>
            await _TryCatch_(async () =>
            {
                MedicalFileArchivePatientDto medicalFileArchiveDto = new MedicalFileArchivePatientDto();
                List<AdviceMedicalDto> adviceMedicalDtos = new List<AdviceMedicalDto>();
                List<MedicalOrdresPatientDto> medicalOrdres = new List<MedicalOrdresPatientDto>();
                MedicalOrdreDetails MedicalOrdreDetails = new MedicalOrdreDetails();
                List<PrescriptionLineInformationDto> prescriptionLinesInformationDto = new List<PrescriptionLineInformationDto>();
                List<RadioLineInformationDto> radioLinesInformationDto = new List<RadioLineInformationDto>();
                List<AnalyseLineInformationDto> analyseLinesInformationDto = new List<AnalyseLineInformationDto>();
                ValidateEntryOnGetArchiveOrdreFileMedicalPatient(Email, FileId);
                var UserAccountDoctor = await this._UserManager.FindByEmailAsync(Email);
                ValidateUserIsNull(UserAccountDoctor);
                var fileMedical = await this.fileMedicalManager.SelectFileMedicalByIdAsync(DecryptGuid(FileId));
                validateeFileMedicalIsNull(fileMedical);
                medicalFileArchiveDto.informationFileMedical = MapperToInformationFileMedical(fileMedical);
                var ListOrdreMedical = await this.ordreMedicalManager.SelectListOrdreMedicalByIdMedicalFile(fileMedical.Id);
                ListOrdreMedical = ListOrdreMedical.Where(e =>e.Status == Models.MedicalOrder.StatuseOrdreMedical.validate).ToList();
                foreach (var ItemOrdreMedical in ListOrdreMedical)
                {
                    var UserAccountDoctorOrdreMedical = await this.userManager.SelectUserByIdDoctor(ItemOrdreMedical.IdDoctor);
                    if (UserAccountDoctorOrdreMedical != null && UserAccountDoctorOrdreMedical.Status != UserStatus.Deactivated)
                    {
                        var Specialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(ItemOrdreMedical.IdDoctor);
                        var InformationDoctor = MapperToDoctorInformationDto(Specialities, UserAccountDoctorOrdreMedical);
                        var Prescription = await this.prescriptionManager.SelectPrescriptionByIdMedicalOrdreAsync(ItemOrdreMedical.Id);
                        if (Prescription != null)
                        {
                            var ListPrescliptionLine = await this.PrescriptionLineManager.SelectLinePrescriptionByIdPrescription(Prescription.Id);
                            foreach (var Line in ListPrescliptionLine)
                            {
                                var PrescriptionLinInformation = MapperToPrescriptionLineInformationDto(Line);
                                prescriptionLinesInformationDto.Add(PrescriptionLinInformation);
                            }
                        }
                        else
                        {
                            prescriptionLinesInformationDto = null;
                        }
                        var RadioMedical = await this.radioManager.SelectRadioByIdMedicalOrdre(ItemOrdreMedical.Id);
                        if (RadioMedical != null)
                        {
                            var ListLineRadioMedical = await this.lineRadioMedicalManager.SelectAllLineMedicalByIdRadio(RadioMedical.Id);

                            foreach (var LineRadio in ListLineRadioMedical)
                            {
                                var RadioLineInformation = MapperToRadioLineInformationDto(LineRadio);
                                radioLinesInformationDto.Add(RadioLineInformation);
                            }
                        }
                        else
                        {
                            radioLinesInformationDto = null;
                        }
                        var AnalyseMedical = await this.analyseManager.SelectAnalyseByOrdreMedicalId(ItemOrdreMedical.Id);
                        if (AnalyseMedical != null)
                        {
                            var ListLineAnalyseMedical = await this.lineAnalyseMedicalManager.SelectLinesMedicalByIdAnalyseAsync(AnalyseMedical.Id);
                            foreach (var LineAnalyse in ListLineAnalyseMedical)
                            {
                                var LineAnalyseInformation = MapperToAnalyseLineInformationDto(LineAnalyse);
                                analyseLinesInformationDto.Add(LineAnalyseInformation);

                            }
                        }
                        else
                        {
                            analyseLinesInformationDto = null;
                        }
                        if(analyseLinesInformationDto != null || radioLinesInformationDto!=null || prescriptionLinesInformationDto != null)
                        {
                            MedicalOrdreDetails = MapperToMedicalOrdreDetails(ItemOrdreMedical, analyseLinesInformationDto, radioLinesInformationDto, prescriptionLinesInformationDto);
                           
                            var ListAdvices = await this.adviceManager.adviceMedicalsByIdOrdreMedicalAsync(ItemOrdreMedical.Id);
                            foreach(var ItemAdvice in ListAdvices)
                            {
                                var UserAccountSender = await this._UserManager.FindByIdAsync(ItemAdvice.TransmitterUserId);
                                var UserAccountReceiver = await this._UserManager.FindByIdAsync(ItemAdvice.ReceiverUserId);
                                var ItemAdviceMedicalDto = MapperToAdviceMedical(UserAccountSender, UserAccountReceiver,ItemAdvice);
                                adviceMedicalDtos.Add(ItemAdviceMedicalDto);

                            }
                            var medicalOrdre = MapperToMedicalOrdresPatientDto(MedicalOrdreDetails, InformationDoctor,adviceMedicalDtos);
                            medicalOrdres.Add(medicalOrdre);

                        }
                       
                        analyseLinesInformationDto = new List<AnalyseLineInformationDto>();
                        radioLinesInformationDto = new List<RadioLineInformationDto>();
                        prescriptionLinesInformationDto = new List<PrescriptionLineInformationDto>();
                        adviceMedicalDtos = new List<AdviceMedicalDto>();


                    }


                }
                medicalFileArchiveDto.medicalOrdres = medicalOrdres;

                return medicalFileArchiveDto;




            });
    }
}
