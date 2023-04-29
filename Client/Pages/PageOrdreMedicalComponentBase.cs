using Client.Services.Foundations.MedicalPlanningService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Client.Services.Foundations.OrdreMedicalService;
using DTO;
using Microsoft.AspNetCore.Components.Forms;
using Client.Services.Foundations.FileMedicalService;
using Microsoft.JSInterop;
using Client.Services.Foundations.LineAnalyseResultService;
using Client.Services.Foundations.LineRadioResultService;
using System.Collections;

namespace Client.Pages
{
    public class PageOrdreMedicalComponentBase :ComponentBase
    {
        protected bool isLoading = false;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected List<OrdreMedicalDto> ordreMedicalDtos = new List<OrdreMedicalDto>();
        protected OrderMedicalToAddDro OrderMedicalToAddDro = new OrderMedicalToAddDro();
        protected RadioToAddDto RadioToAddDto = new RadioToAddDto();
        protected AnalyseToAddDto AnalyseToAddDto = new AnalyseToAddDto();
        protected PrescriptionDto PrescriptionDto= new PrescriptionDto();
        protected PrescriptionLineDto PrescriptionLineDto= new PrescriptionLineDto();
        protected List<PrescriptionLineDto> ListPrescriptionLineDto = new List<PrescriptionLineDto>();
        protected LineRadioMedicalDto LineRadioMedicalDto = new LineRadioMedicalDto();
        protected List<LineRadioMedicalDto> ListLineRadioDto = new List<LineRadioMedicalDto>();
        protected LineAnalyseMedicalDto LineAnalyseMedicalDto = new LineAnalyseMedicalDto();
        protected List<LineAnalyseMedicalDto> ListLineAnalyseDto = new List<LineAnalyseMedicalDto>();
        protected MedicalFileArchiveDto MedicalFileArchive = new MedicalFileArchiveDto();
        protected List<MedicalOrdresDto> ListmedicalOrdre = new List<MedicalOrdresDto>();
        protected MedicalOrdreDetails medicalOrdreDetails = new MedicalOrdreDetails();
        protected PrescriptionLineInformationDto prescriptionLineInformation = new PrescriptionLineInformationDto();
        protected int CountInput = 0;
        [Parameter]
        public string AppointmentId { get; set; }
        [Parameter]
        public string FileId { get; set; }

       
           



        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IOrdreMedicalService OrdreMedicalService { get; set; }

        [Inject]
          protected ILineAnalyseResultService LineAnalyseResultService { get; set; }
        [Inject]
        protected ILineRadioResultService lineRadioResultService { get; set; }
        [Inject]
        protected IfileMedicalService fileMedicalService { get; set; }  
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("import", "/Js/ScripteFileMedical.js");
            }
        }

        protected async Task DownloadFileRadio(string LineRadioId)
        {
            try
            {


                // Save the file
                var FileResult = await this.lineRadioResultService.GetFileResultRadio(AppointmentId,LineRadioId);
                if(FileResult.DataFile != null)
                {
                    MemoryStream stream = new MemoryStream(FileResult.DataFile);
                    using var streamRef = new DotNetStreamReference(stream: stream);
                    var fileName = "File.pdf";
                    await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
                }
                else
                {
                    throw new Exception("File Is Empty");
                }
              



            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            var AuthUser = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            if(AuthUser.User.Identity?.IsAuthenticated?? false)
            {
                try
                {
                    
                    this.MedicalFileArchive = await this.OrdreMedicalService.GetMedicalFileArchive(FileId, AppointmentId);
                    this.ListmedicalOrdre = this.MedicalFileArchive.medicalOrdres.OrderByDescending(e=>e.medicalOrdreDetails.DateValidation).ToList();
                    isLoading = false;
                }
                catch(Exception Ex)
                {
                    this.ErrorMessage= Ex.Message;
                    isLoading = false;
                }
                
            }
            else
            {
                this.NavigationManager.NavigateTo("/Login/Home",forceLoad:true);
            }

        }

        public async Task OnSelectOrdreMedical(string IdOrdreMedical)
        {
            var OrdreMedical = this.MedicalFileArchive.medicalOrdres.Where(e => e.medicalOrdreDetails.Id == IdOrdreMedical).FirstOrDefault();
            this.medicalOrdreDetails = OrdreMedical?.medicalOrdreDetails ?? new MedicalOrdreDetails();
        }
       
        public async Task OnAddPrescription()
        {
            this.OrderMedicalToAddDro.Prescription = PrescriptionDto;

            OrderMedicalToAddDro.Prescription.prescriptionLines = ListPrescriptionLineDto;
        }
        public async Task OnSkipePrescription()
        {
            this.OrderMedicalToAddDro.Prescription = null;
        }
        public async Task OnAddRadio()
        {
          
            this.OrderMedicalToAddDro.RadioToAdd = RadioToAddDto;

            OrderMedicalToAddDro.RadioToAdd.LineRadioMedicals = ListLineRadioDto;
        }
        public async Task OnSkipRadio()
        {
            this.OrderMedicalToAddDro.RadioToAdd = null;
        }
        public async Task OnUpdateAnalyse()
        {
            this.OrderMedicalToAddDro.AnalyseToAdd = AnalyseToAddDto;
            this.OrderMedicalToAddDro.AnalyseToAdd.LineAnalyseMedicals = ListLineAnalyseDto;
        }
        public async Task OnSkipAnalyse()
        {
            this.OrderMedicalToAddDro.AnalyseToAdd = null;
        }
        public async Task OnAddLinePrescription()
        {
           
            this.ListPrescriptionLineDto.Add(PrescriptionLineDto);
            
            PrescriptionLineDto = new PrescriptionLineDto ();


        }
        public async Task OnAddLineRadio()
        {

            this.ListLineRadioDto.Add(LineRadioMedicalDto);

            LineRadioMedicalDto = new LineRadioMedicalDto();


        }
        public async Task OnAddLineAnalyse()
        {

            this.ListLineAnalyseDto.Add(LineAnalyseMedicalDto);

            LineAnalyseMedicalDto = new LineAnalyseMedicalDto();


        }
        protected async Task HandleFileAnalyseSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                 this.AnalyseToAddDto.FileMedicalAnalyse = buffer;
            }
        }
        protected async Task HandleFileRadioSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                this.RadioToAddDto.FileMedicalRadio = buffer;
            }
        }
        protected async Task HandleFilePrescriptionSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                this.PrescriptionDto.PrescriptionFile = buffer;
            }
        }
        protected async Task Done()
        {
            try
            {
                this.isLoading = true;
                this.OrderMedicalToAddDro.AppointmentId = this.AppointmentId;
                this.OrderMedicalToAddDro.FileId = this.FileId;
                if(OrderMedicalToAddDro.Prescription == null && OrderMedicalToAddDro.RadioToAdd == null && OrderMedicalToAddDro.AnalyseToAdd == null)
                {
              
                    throw new Exception("Error Save Ordre Medical");
               
                }

                    await this.OrdreMedicalService.PostOrdreMedicalPatient(OrderMedicalToAddDro);

                    SuccessMessage = "Operation Success";
                    this.isLoading = false;
                this.ErrorMessage = null;

                
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                this.isLoading = false;
            }
          
        }
    }
}
