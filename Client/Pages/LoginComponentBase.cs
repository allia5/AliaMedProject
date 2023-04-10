using Client.Services.Exceptions;
using Client.Services.Foundations.LoginService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages
{
    public class LoginComponentBase : ComponentBase
    {
        public LoginAccountDto loginAccount = new LoginAccountDto();
        public string ErrorMessage = null;
        [Parameter]
        public string ReturnUrl { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public ILoginService loginService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }





        protected async override Task OnParametersSetAsync()
        {

            var Stat = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (Stat.User.Identity?.IsAuthenticated ?? false)
            {
                this.navigationManager.NavigateTo(ReturnUrl, forceLoad: true);
            }

        }
        public async Task OnValid()
        {
            try
            {
                await this.loginService.AuthentificationAccount(loginAccount);
                navigationManager.NavigateTo(ReturnUrl, forceLoad: true);
            }
            catch (UnauthorizedException Ex)
            {
                this.ErrorMessage = Ex.Message;

            }
            catch (NotFoundException Ex)
            {
                this.ErrorMessage = Ex.Message;
            }
            catch (BadRequestException Ex)
            {
                this.ErrorMessage = Ex.Message;
            }
        }

    }
}
