using DTO;
using Microsoft.AspNetCore.Components;
using Client.Services.Foundations.SecretaryService;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages
{
    public class ListOfAppoimentMedicalSecretaryComponentBase : ComponentBase
    {
        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        protected List<SecretaryCabinetInformationDto> secretaryCabinetInformationDtos = new List<SecretaryCabinetInformationDto>();
        protected List<DoctorInformationAppointmentDto> ListDoctorInformation = new List<DoctorInformationAppointmentDto>();
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected ISercretaryService sercretaryService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var UserStat = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
                if (UserStat.User.Identity?.IsAuthenticated ?? false)
                {
                    this.secretaryCabinetInformationDtos = await this.sercretaryService.GetAllCabinetSecretaryInformation();
                    this.IsLoading = false;
                }
                else
                {
                    this.NavigationManager.NavigateTo("/Login/Home");
                }
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }

        protected async Task ShowListDoctor(string IdCabinet)
        {
            var CabinetInformation = this.secretaryCabinetInformationDtos.Where(e => e.CabinetInformation.Id == IdCabinet).FirstOrDefault();
            this.ListDoctorInformation = CabinetInformation.ListDoctorInformation;


        }
    }
}
