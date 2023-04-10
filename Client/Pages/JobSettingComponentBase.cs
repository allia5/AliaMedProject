using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.WorkDoctorService;
using DTO;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;

namespace Client.Pages
{
    public class JobSettingComponentBase : ComponentBase
    {
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected bool IsLoding = true;
        protected JobSettingDto JobSettingModel = new JobSettingDto();
        [Parameter]
        public string JobId { get; set; }
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        protected IWorkDoctorService WorkDoctorService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var UserStat = await this.AuthentificationStatService.GetAuthenticationStateAsync();
                if (UserStat.User.Identity?.IsAuthenticated ?? false)
                {
                    this.JobId = JobId.Replace("*", "/");
                    this.JobSettingModel = await this.WorkDoctorService.GetJobSetting(JobId);
                    this.IsLoding = false;
                }
                else
                {
                    this.navigationManager.NavigateTo("/Login/JobSetting");
                }
            }
            catch (Exception e)
            {
                this.ErrorMessage = "We Have Error please contact support";
            }


        }
        public async Task OnUpdate()
        {
            try
            {
                this.IsLoding = true;
                await this.WorkDoctorService.UpdateJobSetting(JobSettingModel);
                this.IsLoding = false;
                this.SuccessMessage = "Operation has been succefly";
            }
            catch (Exception e)
            {
                this.ErrorMessage = "Has an Error in Operation";
            }

        }



    }
}
