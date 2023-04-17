﻿using Client.Services.Foundations.LocalStorageService;
using Client.Services.Foundations.MedicalPlanningService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages
{
    public class ListOfAppoimentMedicalComponentBase : ComponentBase
    {
        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        public string AdressMap = null;
        protected string Index = null;
        protected string IndexBtnSearshloading = null;
        protected DateTime DateAppoiment { get; set;  } = DateTime.Now;
        public List<AppointmentInformationDto> ListappointmentInformation = new List<AppointmentInformationDto>();
        public List<AppointmentInformationDto> ListappointmentInformationTemp = new List<AppointmentInformationDto>();
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMedicalPlanningService medicalPlanningService { get; set; }
        [Inject]
        public ILocalStorageServices localStorageServices { get; set; }

        protected async Task OnSearch()
        {
          this.ListappointmentInformationTemp =  this.ListappointmentInformation.Where(e => e.DateAppoiment.Date == DateAppoiment.Date).ToList();
        }
        protected override async Task OnInitializedAsync()
        {
            try
            {

                var result = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
                if (result.User.Identity?.IsAuthenticated ?? false)
                {
                    this.ListappointmentInformation = await this.medicalPlanningService.GetAppointmentInformationDto();
                    this.ListappointmentInformationTemp = ListappointmentInformation.Where(x => x.DateAppoiment.Date == DateAppoiment.Date).OrderByDescending(x => x.DateAppoiment).ToList();   
                    IsLoading = false;
                }
                else
                {
                    NavigationManager.NavigateTo("/Login/PlanningMedicalInformation");
                }
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                IsLoading = false;

            }


        }
        protected async Task OnDeleteMedicalAppoiment(string IdAppoimentMedical)
        {
            try
            {
                this.Index = IdAppoimentMedical;
                await this.medicalPlanningService.DeleteMedecalAppoiment(IdAppoimentMedical);
                var Item = ListappointmentInformation.Where(e => e.Id == IdAppoimentMedical).FirstOrDefault();
                this.ListappointmentInformationTemp.Remove(Item);
                this.Index = null;
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                this.Index = null;
            }
        }
        protected async Task OnUpdateAdressMap(string addressMap)
        {
            this.AdressMap = addressMap;
        }
    }
}
