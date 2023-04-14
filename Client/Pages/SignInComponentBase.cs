﻿using Client.Services.Exceptions;
using Client.Services.Foundations.SignInService;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class SignInBase : ComponentBase
    {
        public bool isloading = false;
        public RegistreAccountDto RegistreAccountDto = new RegistreAccountDto();
        protected MessageResultDto messageResult = new MessageResultDto();
        [Inject]
        public ISignInService SignInService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string MessageError = null;
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        protected void Next()
        {

        }
        protected void Previouse()
        {

        }
        public async Task SignIn()
        {
            try
            {
                isloading = true;
                this.messageResult = await this.SignInService.SignInAsync(RegistreAccountDto);
                isloading = false;

            }
            catch (BadRequestException ex)
            {
                MessageError = ex.Message;
            }
            catch (ConflictException ex)
            {
                MessageError = ex.Message;
            }
        }


    }
}
