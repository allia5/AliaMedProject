﻿using Client.Services.Exceptions;
using Client.Services.Foundations.MedicalPlanningService;
using Client.Services.Foundations.SecretaryService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages
{
    public class ListAppoimentPatientSecretaryComponentBase : ComponentBase
    {
        [Parameter]
        public string CabinetId { get; set; }
        [Parameter]
        public string DoctorId { get; set; }

        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        protected string IndexBtnOne = null;
        protected string IndexBtnTwo = null;
        protected DateTime DateAppoiment { get; set; } = DateTime.Now;
        protected List<PlanningDto> planningDtos = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosStill = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosAbsent = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosTreated = new List<PlanningDto>();
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IMedicalPlanningService medicalPlanningService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var UserStat = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
                if(UserStat.User.Identity?.IsAuthenticated ?? false)
                {
                   
                    this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientSecretaryDto( new KeysAppoimentInformationSecretary { CabinetId = CabinetId ,IdDoctor = DoctorId, DateAppoiment = DateAppoiment});
                    this.planningDtosStill = planningDtos.Where(e=>e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.IsLoading = false;
                }

            }catch(UnauthorizedException Ex)
            {
                this.NavigationManager.NavigateTo("Login/Home");
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                IsLoading = false;
            }
        }

        public async Task OnTreated(string IdAppoiment)
        {
            try
            {
                IndexBtnTwo = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.Treated });
                var itemTreated=  this.planningDtos.Where(e=>e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                if(itemTreated != null) { this.planningDtosTreated.Add(itemTreated); this.planningDtosStill.Remove(itemTreated); }
                IndexBtnTwo = null;
            

            }
            catch(Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }
        public async Task OnAbsent(string IdAppoiment)
        {
            try
            {
                IndexBtnOne = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.absent });
                var ItemAbsent = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                if (ItemAbsent != null) { this.planningDtosAbsent.Add(ItemAbsent); this.planningDtosStill.Remove(ItemAbsent); }
                IndexBtnOne = null;
             //   await décrementCountAppoimentAbsent();
                await décrementCountAppoimentStill();
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }

        protected async Task décrementCountAppoimentAbsent()
        {
            var k = 1;
            foreach (var itemAbsent in planningDtosAbsent.ToList())
            {
                
            
                var index = planningDtosAbsent.IndexOf(itemAbsent);
                itemAbsent.PatientAppoimentInformation.AppoimentCount = k;
                planningDtosAbsent[index] = itemAbsent;
                k++;
            }
        }
        protected async Task décrementCountAppoimentStill()
        {
            var k = 1;
            foreach (var itemStille in planningDtosStill.ToList())
            {
                
                var index = planningDtosStill.IndexOf(itemStille);
                itemStille.PatientAppoimentInformation.AppoimentCount = k;
                planningDtosStill[index] = itemStille;
                k++;
            }
        }

    }
}
