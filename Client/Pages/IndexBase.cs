using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
       
       
        
        protected override async Task OnInitializedAsync()
        {
            var result = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (!result.User.Identity?.IsAuthenticated ?? false)
            {
                this.NavigationManager.NavigateTo("/Login/Home");
            }

        }
    }
}
