using Client.Services.Exceptions;
using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.WorkDoctorService;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class InvitationWorkComponentBase : ComponentBase
    {
        public string ErrorMessage = null;
        protected string IndexRefuse = null;
        protected string IndexAccepte = null;
        protected bool IsLoding = true;
        public List<InvitationsDoctorDto> invitationsDoctorDtos = new List<InvitationsDoctorDto>();
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IWorkDoctorService WorkDoctorService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var UserStat = await this.AuthentificationStatService.GetAuthenticationStateAsync();
                if (UserStat.User.Identity?.IsAuthenticated ?? false)
                {
                    this.invitationsDoctorDtos = await this.WorkDoctorService.invitationsDoctorService();
                    this.IsLoding = false;
                }
                else
                {
                    this.NavigationManager.NavigateTo("Login/InvitationWork");
                }
            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message;

            }

        }
        public async Task OnRefuse(string Id)
        {
            try
            {
                this.IndexRefuse = Id;
                await this.WorkDoctorService.DeleteJobDoctorByDoctor(Id);
                invitationsDoctorDtos = invitationsDoctorDtos.Where(e => e.Id != Id).ToList();
                this.IndexAccepte = null;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
        public async Task OnAccept(string id)
        {
            try
            {
                this.IndexAccepte = id;
                await this.WorkDoctorService.UpdateStatusServiceWorkDoctor(new UpdateStatusWorkDoctorDto { Status = StatusWorkDoctor.accepted, WorkId = id });
                invitationsDoctorDtos = invitationsDoctorDtos.Where(e => e.Id != id).ToList();
                this.IndexAccepte = null;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
    }
}
