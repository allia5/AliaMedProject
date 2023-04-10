using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.WorkDoctorService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public class ListofWorkingDoctorsComponentBase : ComponentBase
    {
        protected string ErrorMessage = null;
        protected string Index = null;
        protected string idIndex = null;
        protected bool IsLoading = true;
        protected JobSettingDto jobSetting = new JobSettingDto();
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        protected IWorkDoctorService WorkDoctorService { get; set; }
        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        protected List<DoctorCabinetDto> doctorCabinetDtos = new List<DoctorCabinetDto>();
        protected List<DoctorCabinetDto> DoctorsOutOfService = new List<DoctorCabinetDto>();

        protected override async Task OnInitializedAsync()
        {

            try
            {
                var UserStat = await this.AuthentificationStatService.GetAuthenticationStateAsync();
                if (UserStat.User.Identity?.IsAuthenticated ?? false)
                {
                    var result = await this.WorkDoctorService.GetListDoctorsInformationCabinet();
                    this.doctorCabinetDtos = result.Where(s => s.StatusWork == StatusWorkDoctor.accepted).ToList();
                    this.DoctorsOutOfService = result.Where(s => s.StatusWork == StatusWorkDoctor.Notaccepted).ToList();
                    this.IsLoading = false;
                }
                else
                {
                    this.navigationManager.NavigateTo("Login/ListWorkingDoctor");
                }
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }

        protected async Task OnShowInformation(JobSettingDto jobSetting)
        {
            this.jobSetting.startTime = jobSetting.startTime;
            this.jobSetting.EndTime = jobSetting.EndTime;
            this.jobSetting.NumberPatientAccepted = jobSetting.NumberPatientAccepted;
            this.jobSetting.processingMinutes = jobSetting.processingMinutes;
            this.jobSetting.statusService = jobSetting.statusService;
            this.jobSetting.StatusReservation = jobSetting.StatusReservation;
        }

        protected async Task OnDeleteJob(string JobId)
        {
            try
            {
                this.idIndex = JobId;
                await this.WorkDoctorService.DeleteJobDoctorByAdmin(JobId);
                this.doctorCabinetDtos = this.doctorCabinetDtos.Where(e => e.JobSettingDto.IdJob != JobId).ToList();
                this.DoctorsOutOfService = this.DoctorsOutOfService.Where(k => k.JobSettingDto.IdJob != JobId).ToList();
                this.idIndex = null;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

    }
}
