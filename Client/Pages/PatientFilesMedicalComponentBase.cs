using Client.Services.Foundations.FileMedicalService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages
{
    public class PatientFilesMedicalComponentBase : ComponentBase
    {
        protected bool IsLoading = false;
        protected List<FileMedicalPatientDto> FileMedicalPatients = new List<FileMedicalPatientDto>();
        [Inject]
        protected IfileMedicalService  FileMedicalService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
           var UserStat = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            if(UserStat.User.Identity?.IsAuthenticated ?? false)
            {
                this.IsLoading = true;
                this.FileMedicalPatients = await this.FileMedicalService.GetFilePatient();
                this.IsLoading = false;
            }
            else
            {
                this.NavigationManager.NavigateTo("Login/Home");
            }
        }
    }
}
