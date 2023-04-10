using Client.Services.Exceptions;
using Client.Services.Foundations.SignInService;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class ValidationAccountBase : ComponentBase
    {
        protected MessageResultDto messageResult = new MessageResultDto();
        protected string ErrorMessage { get; set; }
        [Inject]
        protected ISignInService signInService { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Token { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            try
            {
                if (this.Id != null && this.Token != null)
                {
                    this.messageResult = await this.signInService.ValidateAccountService(this.Id, this.Token);


                }
            }
            catch (BadRequestException Ex)
            {
                ErrorMessage = Ex.Message;

            }
            catch (UnauthorizedException Ex)
            {
                ErrorMessage = Ex.Message;
            }

            catch (NullException Ex)
            {
                ErrorMessage = "Empty Content";
            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message;
            }




        }
    }
}




