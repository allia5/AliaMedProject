using Client.Services.Foundations.MedicalPlanningService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Client.Services.Foundations.OrdreMedicalService;
using DTO;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages
{
    public class PageOrdreMedicalComponentBase :ComponentBase
    {
        protected bool isLoading = true;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected List<OrdreMedicalDto> ordreMedicalDtos = new List<OrdreMedicalDto>();
        protected OrderMedicalToAddDro OrderMedicalToAddDro = new OrderMedicalToAddDro();
        protected int CountInput = 0;
        [Parameter]
        public string AppointmentId { get; set; }
        [Parameter]
        public string FileId { get; set; }

        protected HubConnection? hubConnection { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IOrdreMedicalService OrdreMedicalService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public async Task OnUpdatePrescription()
        {

        }
        public async Task OnSkipePrescription()
        {

        }
        public async Task OnUpdateRadio()
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
            this.CountInput++;
            this.OrderMedicalToAddDro.Prescription.prescriptionLines.Add(new PrescriptionLineDto());
        }
        protected async Task HandleFileAnalyseSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                 this.OrderMedicalToAddDro.AnalyseToAdd.FileMedicalAnalyse = buffer;
            }
        }
        protected async Task HandleFileRadioSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                this.OrderMedicalToAddDro.RadioToAdd.FileMedicalRadio = buffer;
            }
        }
        protected async Task HandleFilePrescriptionSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                this.OrderMedicalToAddDro.Prescription.PrescriptionFile = buffer;
            }
        }
        protected async Task Done()
        {
            try
            {
                this.isLoading = true;
                this.OrderMedicalToAddDro.AppointmentId = this.AppointmentId;
                this.OrderMedicalToAddDro.FileId = this.FileId;
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
