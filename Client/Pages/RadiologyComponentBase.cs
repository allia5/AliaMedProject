using Client.Services.Exceptions;
using Client.Services.Foundations.RadiologyService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public class RadiologyComponentBase : ComponentBase
    {
        protected bool IsLoading = false;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected string CodeQr = null;
        protected bool ButtonAddIsLoding = false;
        [Inject]
        protected IJSRuntime jSRuntime { get; set; }
        protected InformationRadioResultDto InformationRadioResultDto = null;
        [Inject]
        public IRadiologyService RadiologyService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
          var UserStat = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            if(UserStat.User.Identity?.IsAuthenticated??false)
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
                this.InformationRadioResultDto = await this.RadiologyService.GetInformationRadioResultAsync(CodeQr);
                this.IsLoading = false;
                ButtonAddIsLoding = false;


            }
            catch (NoContentException Ex)
            {
                this.jSRuntime.InvokeVoidAsync("Alert", "This Ordre Medical Has Been Validated");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                this.IsLoading = false;
                ButtonAddIsLoding = false;
            }
            
        }
    }
}
