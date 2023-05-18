using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.UserService;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class ForgotPasswordComponentBase : ComponentBase
    {
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected bool IsLoading = false;
        protected string Email = null;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set;}
        [Inject]
        public IUserService userService { get; set; }
        protected override async Task OnInitializedAsync()
        {
           var User = await this.AuthentificationStatService.GetAuthenticationStateAsync();
            if(User.User.Identity?.IsAuthenticated ?? false)
            {
                this.NavigationManager.NavigateTo("Home", forceLoad: true);
            }
          
        }
        protected async Task OnResetPassword()
        {
            try
            {
                this.IsLoading = true;
                await this.userService.ForgotPasswordUserAccount(Email);
                this.IsLoading = false;
                this.SuccessMessage = "Operation Success , Please check your email";
                this.ErrorMessage = null;

            }
            catch(Exception ex)
            {
                this.ErrorMessage = ex.Message;
                this.SuccessMessage = null;
                this.IsLoading = false;
            }
        }

    }
}
