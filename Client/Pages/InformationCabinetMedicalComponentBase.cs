﻿using Client.Services.Exceptions;
using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.CabinetMedicalService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection.Metadata;

namespace Client.Pages
{
    public class InformationCabinetMedicalComponentBase : ComponentBase
    {
        public string ErrorMessage = null;
        public string SuccessMessage = null;
        public bool IsLoding = true;
        public bool Index = false;

        public CabinetMedicalDto CabinetMedicalInformation = new CabinetMedicalDto();
        [Inject]
        public ICabinetMedicalService CabinetMedicalService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var ResultAuth = await this.AuthentificationStatService.GetAuthenticationStateAsync();
                if (ResultAuth.User.Identity?.IsAuthenticated ?? false)
                {
                    this.CabinetMedicalInformation = await this.CabinetMedicalService.GetInformationFromCabinetMedical();
                    this.IsLoding = false;
                }
                else
                {
                    this.NavigationManager.NavigateTo("/Login/InformationCabinetMedical");
                }
            }
            catch (BadRequestException Ex)
            {
                ErrorMessage = "Validation Error";
            }
            catch (NotFoundException Ex)
            {
                ErrorMessage = "Data NOt Found ";
            }
            catch (UnauthorizedException Ex)
            {
                ErrorMessage = "You Are Not Authorized";
            }
            catch (NullException Ex)
            {
                ErrorMessage = "Empty Data ";
            }
            catch (ProblemException Ex)
            {
                ErrorMessage = "Error Intern ";
            }
        }
        protected async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
           
                var file = e.File;
              
                    using (var stream = file.OpenReadStream())
                    {
                        var buffer = new byte[file.Size];
                        await stream.ReadAsync(buffer, 0, (int)file.Size);
                        CabinetMedicalInformation.Image = buffer;
                        this.ErrorMessage = null;
                    }
            
        }
      
        public async Task Update()
        {
            try
            {
                this.Index = true;
                var result = await this.CabinetMedicalService.UpdateInformationCabinetMedical(CabinetMedicalInformation);
                SuccessMessage = "Update Success";
                this.Index = false;
                
            }
            catch (Exception Ex)
            {
                this.ErrorMessage = "Intern Error";
                this.Index= false;
            }

        }
    }
}
