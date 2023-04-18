using Client.Services.Foundations.MedicalPlanningService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Client.Services.Foundations.OrdreMedicalService;
using DTO;

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
        }
    }
}
