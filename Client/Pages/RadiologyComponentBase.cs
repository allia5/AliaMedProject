using Client.Services.Exceptions;
using Client.Services.Foundations.RadiologyService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
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
        protected string ButtonLoaddingOnAddResult = null;
        protected string MessageHasBeenValidated = null;
        protected RadioResultToAddDto radioResultToAdd = new RadioResultToAddDto();
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
        protected async Task OnAddRadioResult(string LineRadioId)
        {
            try
            {
                this.ButtonLoaddingOnAddResult = LineRadioId;
                this.IsLoading = true;
                this.radioResultToAdd.IdLineRadio = LineRadioId;
                await this.RadiologyService.PostRadioMedicalResult(radioResultToAdd);
               var itemLine = this.InformationRadioResultDto.RadioInformation.linesRadioMedicals.Where(e=>e.Id== LineRadioId).FirstOrDefault();
                this.InformationRadioResultDto.RadioInformation.linesRadioMedicals.Remove(itemLine);
                this.IsLoading = false;
                this.SuccessMessage = "Operation Valide";
                this.ErrorMessage = null ;
                this.ButtonLoaddingOnAddResult = null;
                radioResultToAdd = new RadioResultToAddDto();
            }
            catch(Exception ex)
            {
                this.ErrorMessage=ex.Message;
                this.IsLoading = false;
                this.ButtonLoaddingOnAddResult = null;
                this.SuccessMessage = null;
            }
          

        }
        protected async Task HandleFileRadioResultSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                this.radioResultToAdd.FileUpload = buffer;
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
            }
            
        }
    }
}
