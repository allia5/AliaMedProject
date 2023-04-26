using Client.Services.Exceptions;
using Client.Services.Foundations.PharmacistService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public class PagePharmacistComponentBase:ComponentBase
    {
        protected bool IsLoading = false;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected string CodeQr = null;
        protected bool ButtonAddIsLoding = false;
        protected string ButtonLoaddingOnAddResult = null;
        protected string MessageHasBeenValidated = null;
        protected InformationPrescriptionResultDto prescriptionResultDto = new InformationPrescriptionResultDto();
        [Inject]
        protected IPharmacistService pharmacistService { get; set; }
        [Inject]
        protected IJSRuntime jSRuntime { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }




        protected async Task OnAddCodeQr(string CodeQr)
        {
            try
            {
                ButtonAddIsLoding = true;
                this.IsLoading = true;
                this.CodeQr = CodeQr;
                this.prescriptionResultDto = await this.pharmacistService.GetPrescriptionInformation(CodeQr);
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
