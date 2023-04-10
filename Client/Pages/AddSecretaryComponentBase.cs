using Client.Services.Exceptions;
using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.SecretaryService;
using DTO;
using Microsoft.AspNetCore.Components;
using System.Threading;

namespace Client.Pages
{
    public class AddSecretaryComponentBase : ComponentBase
    {
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected string IndexOne = null;
        protected string IndexTwo = null;
        protected string IndexThree = null;
        protected bool isLoding = true;
        protected string Email = null;
        protected bool ButtonAddIsLoding = false;
        protected List<SecritaryDto> secritaryDtos = new List<SecritaryDto>();
        protected List<SecritaryDto> secritaryDtosActive = new List<SecritaryDto>();
        protected List<SecritaryDto> secritaryDtosBlocked = new List<SecritaryDto>();
        [Inject]
        protected ISercretaryService sercretaryService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var UserStat = await this.AuthentificationStatService.GetAuthenticationStateAsync();
            if (UserStat.User.Identity?.IsAuthenticated ?? false)
            {
                this.secritaryDtos = await this.sercretaryService.GetAllSecretary();
                this.secritaryDtosBlocked = this.secritaryDtos.Where(e => e.StatusSecritary == StatusSecritary.Block).ToList();
                this.secritaryDtosActive = this.secritaryDtos.Where(e => e.StatusSecritary == StatusSecritary.Active).ToList();
                this.isLoding = false;
            }
            else
            {
                this.NavigationManager.NavigateTo("Login/AddSecretary");
            }
        }
        public async Task OnDelete(string SecretaryId)
        {
            try
            {
                this.IndexOne = SecretaryId;
                await this.sercretaryService.UpdateStatusSecretary(new UpdateStatusSecretaryDto { SecretaryId = SecretaryId, StatusSecritary = StatusSecritary.Deleted });
                this.secritaryDtosActive = this.secritaryDtosActive.Where(e => e.IdSecritary != SecretaryId).ToList();
                this.IndexOne = null;
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }
        public async Task OnBack(string SecretaryId)
        {
            this.IndexThree = SecretaryId;
            await this.sercretaryService.UpdateStatusSecretary(new UpdateStatusSecretaryDto { SecretaryId = SecretaryId, StatusSecritary = StatusSecritary.Active });
            var itemActive = this.secritaryDtos.Where(e => e.IdSecritary == SecretaryId).First();
            this.secritaryDtosActive.Add(itemActive);
            this.secritaryDtosBlocked.Remove(itemActive);
            this.IndexThree = null;
        }
        public async Task OnBlock(string SecretaryId)
        {
            this.IndexTwo = SecretaryId;
            await this.sercretaryService.UpdateStatusSecretary(new UpdateStatusSecretaryDto { SecretaryId = SecretaryId, StatusSecritary = StatusSecritary.Block });
            var itemBlocked = this.secritaryDtos.Where(e => e.IdSecritary == SecretaryId).First();
            this.secritaryDtosBlocked.Add(itemBlocked);
            this.secritaryDtosActive.Remove(itemBlocked);
           
            this.IndexTwo = null;
        }
        public async Task AddSecretary()
        {
            try
            {
                this.ButtonAddIsLoding = true;
                var result = await this.sercretaryService.AddSecretary(this.Email);
                this.secritaryDtosActive.Add(result);
                this.ButtonAddIsLoding = false;
                this.SuccessMessage = "Add Operation seccess ";



            }
            catch (BadRequestException Ex)
            {
                this.ErrorMessage = Ex.Message;

            }
            catch (NotFoundException Ex)
            {
                this.ErrorMessage = Ex.Message;

            }
            catch (ConflictException Ex)
            {
                this.ErrorMessage = Ex.Message;

            }
            catch (Exception e)
            {
                this.ErrorMessage = "Error Intern";

            }
        }
    }
}
