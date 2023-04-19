using Client.Services.Foundations.MedicalPlanningService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Client.Services.Foundations.OrdreMedicalService;
using DTO;
using Microsoft.AspNetCore.Components.Forms;
using Client.Services.Foundations.FileMedicalService;

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
        protected IfileMedicalService fileMedicalService { get; set; }  
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public async Task OnAddPrescription()
        {
           
        }
        public async Task OnSkipePrescription()
        {

        }
        public async Task OnAddRadio()
        {
           
        }
        public async Task OnSkipRadio()
        {

        }
        public async Task OnUpdateAnalyse()
        {
      
        }
        public async Task OnSkipAnalyse()
        {

        }
        public async Task OnAddLine()
        {
           
            this.ListPrescriptionLineDto.Add(PrescriptionLineDto);
            PrescriptionLineDto = new PrescriptionLineDto ();


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
                this.OrderMedicalToAddDro.RadioToAdd = this.RadioToAddDto;
                this.OrderMedicalToAddDro.AnalyseToAdd = this.AnalyseToAddDto;
                this.PrescriptionDto.prescriptionLines = ListPrescriptionLineDto;
                this.OrderMedicalToAddDro.Prescription =this.PrescriptionDto;
                var result = await this.OrdreMedicalService.PostOrdreMedicalPatient(OrderMedicalToAddDro);
                this.ordreMedicalDtos.Add(result);
                SuccessMessage = "Operation Success";
                this.isLoading = false;
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
          
        }
    }
}
