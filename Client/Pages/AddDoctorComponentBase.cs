using Client.Services.Exceptions;
using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.DoctorService;
using Client.Services.Foundations.WorkDoctorService;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class AddDoctorComponentBase : ComponentBase
    {
        public bool IsLoading = true;
        public string Index = null;
        public string ErrorMessage = null;
        public List<DoctorInformationDto> Informations = new List<DoctorInformationDto>();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IDoctorService doctorService { get; set; }
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }
        [Inject]
        public IWorkDoctorService workDoctorService { get; set; }

        protected override async Task OnInitializedAsync()
        {

            var UserStat = await this.AuthentificationStatService.GetAuthenticationStateAsync();
            if (UserStat.User.Identity?.IsAuthenticated ?? false)
            {
                this.Informations = await this.doctorService.GetListInformationDoctors();
                this.IsLoading = false;
            }
            else
            {
                this.NavigationManager.NavigateTo("/Home");
            }
        }
        public async Task SendInvitation(string Id)
        {
            this.Index = Id;
            try
            {
                await this.workDoctorService.SendInvitationWorkToDoctot(Id);
                Informations = Informations.Where(s => s.IdUser != Id).ToList();
                this.Index = null;
            }
            catch (BadRequestException Ex)
            {

            }
            catch (NotFoundException Ex)
            {

            }
            catch (ProblemException Ex)
            {

            }

        }


    }
}
