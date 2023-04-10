
using Client.Services.Foundations.LocalStorageService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;

namespace Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        protected bool IsLoding = true;
        [Inject]
        public ILocalStorageServices localStorages { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {

            await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            this.IsLoding = false;

        }




    }
}
