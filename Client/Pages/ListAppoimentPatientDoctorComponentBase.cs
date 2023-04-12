using Client.Services.Exceptions;
using Client.Services.Foundations.MedicalPlanningService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Client.Pages
{
    public class ListAppoimentPatientDoctorComponentBase : ComponentBase
    {
        [Parameter]
        public string CabinetId { get; set; }
        protected string IdAppoimentDelyed = null;
        protected string IndexBtnTwo = null;
        protected string IndexBtnOne = null;
        protected string IndexBtnthree=null;
        protected bool IndexBtnSearshloading = false;
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
                    this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
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
        protected async Task DelayAppoiment()
        {
            try
            {
                await this.medicalPlanningService.DelayeApoimentPatient(new DelayeAppoimentMedical { DateAppoiment = DateAppoiment, Id = IdAppoimentDelyed, statusPlaningDto = StatusPlaningDto.Delayed });
                var ItemAbsent = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoimentDelyed).FirstOrDefault();
                if (ItemAbsent != null) { this.planningDtosTreated.Remove(ItemAbsent); }
               
                décrementCountAppoimentAbsent();
                décrementCountAppoimentStill();
            }
            catch(Exception Ex)
            {
                this.ErrorMessage = Ex.Message;
            }
        }
        protected async Task OnSearch()
        {
            IndexBtnSearshloading = true;
            this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
            this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            IndexBtnSearshloading = false;
        }
        protected async Task OnDelyed(string IdAppoiment)
        {
            this.IdAppoimentDelyed =IdAppoiment;

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

        public async Task OnPassed(string IdAppoiment)
        {
            try
            {
                IndexBtnthree = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.passed });
                var ItemAbsent = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                if (ItemAbsent != null) {  this.planningDtosTreated.Remove(ItemAbsent); }
                IndexBtnOne = null;
                décrementCountAppoimentAbsent();
                décrementCountAppoimentStill();
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }
        protected  async Task décrementCountAppoimentAbsent()
        {
            var k = 1;
            foreach (var itemAbsent in planningDtosAbsent)
            {
                itemAbsent.PatientAppoimentInformation.AppoimentCount = k;
                k++; 
                var index = planningDtosAbsent.IndexOf(itemAbsent);
                planningDtosAbsent[index] = itemAbsent;
            }
        }
        protected async Task décrementCountAppoimentStill()
        {
            var k = 1;
            foreach (var itemAbsent in planningDtosStill)
            {
                itemAbsent.PatientAppoimentInformation.AppoimentCount = k;
                k++; 
                var index = planningDtosAbsent.IndexOf(itemAbsent);
                planningDtosAbsent[index] = itemAbsent;
            }
        }


    }
}
