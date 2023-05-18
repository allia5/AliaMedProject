using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.UserService;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class ResetPasswordUserAccountComponentBase:ComponentBase
    {
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected bool IsLoading = false;
        protected ResetPasswordUserAccountDto resetPasswordUserAccountDto = new ResetPasswordUserAccountDto();
        [Parameter]
        public string uId { get; set; }
        [Parameter]
        public string Token { get; set; }
        [Inject]
        protected IUserService UserService { get; set; }
        [Inject]
        protected AuthentificationStatService AuthentificationStatService { get; set;}
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
           var User = await this.AuthentificationStatService.GetAuthenticationStateAsync();
            if (User.User.Identity?.IsAuthenticated ?? false)
            {
                this.NavigationManager.NavigateTo("Home", forceLoad: true);
            }
           
        }
        protected async Task OnUpdatePassword()
        {
            try
            {
                this.IsLoading = true;
                this.resetPasswordUserAccountDto.Token = this.Token;
                this.resetPasswordUserAccountDto.UserId = this.uId;
                await this.UserService.ResetPasswordUserAccount(resetPasswordUserAccountDto);
                this.SuccessMessage = "Operation Success Please Try Login";
                this.ErrorMessage = null;
                this.IsLoading = false;
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
