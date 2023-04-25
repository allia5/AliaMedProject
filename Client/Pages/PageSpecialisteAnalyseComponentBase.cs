using Client.Services.Exceptions;
using Client.Services.Foundations.RadiologyService;
using Client.Services.Foundations.SpecialisteAnalyseService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public class PageSpecialisteAnalyseComponentBase : ComponentBase
    {
        protected bool IsLoading = false;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected string CodeQr = null;
        protected bool ButtonAddIsLoding = false;
        protected string ButtonLoaddingOnAddResult = null;
        protected string MessageHasBeenValidated = null;
       
        [Inject]
        protected IJSRuntime jSRuntime { get; set; }
        protected InformationAnalyseResultDto InformationAnalyseResultDto = null;
        [Inject]
        public ISpecialisteAnalyseService SpecialisteAnalyseService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var UserStat = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (UserStat.User.Identity?.IsAuthenticated ?? false)
            {

            }
            else
            {
                this.NavigationManager.NavigateTo("Login/RadiologistOperations");
            }
        }
        protected async Task OnAddCodeQr(string CodeQr)
        {
            try
            {
                ButtonAddIsLoding = true;
                this.IsLoading = true;
                this.CodeQr = CodeQr;
                this.InformationAnalyseResultDto = await this.SpecialisteAnalyseService.GetInformationAnalyse(CodeQr);
                this.IsLoading = false;
                ButtonAddIsLoding = false;
                ErrorMessage = null;
                SuccessMessage = null;
                MessageHasBeenValidated = null;


            }
            catch (NoContentException Ex)
            {
                this.MessageHasBeenValidated = "Not Validate";
                this.IsLoading = false;
                ButtonAddIsLoding = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                this.IsLoading = false;
                ButtonAddIsLoding = false;
                MessageHasBeenValidated = null;
            }

        }
    }
}
