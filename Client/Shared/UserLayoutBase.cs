using Client.Services.Foundations.AuthentificationStatService;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Client.Shared
{
    public class UserLayoutBase : ComponentBase
    {
        protected string EmailUser = null;
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }
        protected override async Task OnInitializedAsync()
        {

            var UserStat= await this.AuthentificationStatService.GetAuthenticationStateAsync();
            if(UserStat.User.Identity?.IsAuthenticated ?? false)
            {
                this.EmailUser = UserStat.User.FindFirst(ClaimTypes.Name).Value;
            }


        }
    }
}
