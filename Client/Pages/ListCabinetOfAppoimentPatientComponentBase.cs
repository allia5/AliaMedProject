using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.WorkDoctorService;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class ListCabinetOfAppoimentPatientComponentBase : ComponentBase
    {
        protected bool Isloding = true;
        protected string Index = null;
        protected string ErrorMessage = null;
        public List<JobsDoctorDto> jobs = new List<JobsDoctorDto>();
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
              
                this.jobs = await this.WorkDoctorService.GetJobsDoctorService();
                jobs= jobs.Where(e=>e.StatusServiceDoctor == StatusWorkDoctor.accepted).ToList();
                Isloding = false;
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }

    }
}
