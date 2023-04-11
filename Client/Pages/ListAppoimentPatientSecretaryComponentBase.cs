using Client.Services.Exceptions;
using Client.Services.Foundations.MedicalPlanningService;
using Client.Services.Foundations.SecretaryService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages
{
    public class ListAppoimentPatientSecretaryComponentBase : ComponentBase
    {
        [Parameter]
        public string CabinetId { get; set; }
        [Parameter]
        public string DoctorId { get; set; }

        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        protected DateTime DateAppoiment { get; set; } = DateTime.Now;
        protected List<PlanningDto> planningDtos = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosStill = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosAbsent = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosTreated = new List<PlanningDto>();
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IMedicalPlanningService medicalPlanningService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var UserStat = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
                if(UserStat.User.Identity?.IsAuthenticated ?? false)
                {
                   
                    this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientSecretaryDto( new KeysAppoimentInformationSecretary { CabinetId = CabinetId ,IdDoctor = DoctorId, DateAppoiment = DateAppoiment});
                    this.planningDtosStill = planningDtos.Where(e=>e.PatientAppoimentInformation.Status == StatusPlaningDto.Still).ToList();
                    this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).ToList();
                    this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).ToList();
                    this.IsLoading = false;
                }

            }catch(UnauthorizedException Ex)
            {
                this.NavigationManager.NavigateTo("Login/Home");
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                IsLoading = false;
            }
        }

    }
}
