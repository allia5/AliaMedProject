using Client.Services.Exceptions;
using Client.Services.Foundations.MedicalPlanningService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;

namespace Client.Pages
{
    public class ListAppoimentPatientDoctorComponentBase : ComponentBase
    {
        [Parameter]
        public string CabinetId { get; set; }

        protected string IndexBtnTwo = null;
        protected string IndexBtnOne = null;
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
                if (UserStat.User.Identity?.IsAuthenticated ?? false)
                {

                    this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
                    this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).ToList();
                    this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).ToList();
                    this.IsLoading = false;
                }

            }
            catch (UnauthorizedException Ex)
            {
                this.NavigationManager.NavigateTo("Login/Home");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                IsLoading = false;
            }
        }
        public async Task OnTreated(string IdAppoiment)
        {
            try
            {
                IndexBtnTwo = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.Treated });
                var itemTreated = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                if (itemTreated != null) { this.planningDtosTreated.Add(itemTreated); this.planningDtosStill.Remove(itemTreated); this.planningDtosAbsent.Remove(itemTreated); }
                IndexBtnTwo = null;

            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }
        public async Task OnAbsent(string IdAppoiment)
        {
            try
            {
                IndexBtnOne = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.absent });
                var ItemAbsent = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                if (ItemAbsent != null) { this.planningDtosAbsent.Add(ItemAbsent); this.planningDtosStill.Remove(ItemAbsent); }
                IndexBtnOne = null;
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }

    }
}
