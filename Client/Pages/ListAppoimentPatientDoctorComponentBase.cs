using Client.Services.Exceptions;
using Client.Services.Foundations.MedicalPlanningService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Client.Pages
{
    public class ListAppoimentPatientDoctorComponentBase : ComponentBase
    {
        [Parameter]
        public string CabinetId { get; set; }
        protected string IdAppoimentDelyed = null;
        protected string IndexBtnTwo = null;
        protected string IndexBtnOne = null;
    
        protected string IndexBtnthree=null;
        protected bool IndexBtnSearshloading = false;
        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        protected DateTime DateAppoiment { get; set; } = DateTime.Now;
        protected List<PlanningDto> planningDtos = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosStill = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosAbsent = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosTreated = new List<PlanningDto>();
        protected HubConnection? hubConnection { get; set; }
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
                if (UserStat.User.Identity?.IsAuthenticated ?? false)
                {

                    this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
                    this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                    this.IsLoading = false;

                    this.hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7104/PlanningAppoimentHub").Build();
                    hubConnection.On<UpdateStatusAppoimentDto>("ReceiveUpdateStatusAppoitment", (ItemUpdate) =>
                    {
                   
                        if (ItemUpdate.statusPlaningDto == StatusPlaningDto.absent)
                        {
                            var item = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == ItemUpdate.Id).FirstOrDefault();
                            if (item != null)
                            {
                                this.planningDtosAbsent.Add(item);
                                this.planningDtosStill.Remove(item);
                                décrementCountAppoimentStill();
                            }
                             


                        }
                        else if (ItemUpdate.statusPlaningDto == StatusPlaningDto.Treated)
                        {
                            var item = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == ItemUpdate.Id).FirstOrDefault();
                            if (item != null)
                            {
                                this.planningDtosTreated.Add(item);
                                this.planningDtosStill.Remove(item);
                                var itemAbsent = this.planningDtosAbsent.Where(e => e.PatientAppoimentInformation.Id == ItemUpdate.Id).FirstOrDefault();
                                if (itemAbsent != null)
                                {
                                    this.planningDtosAbsent.Remove(itemAbsent);
                                }
                            }
                           
                        }
                        else if (ItemUpdate.statusPlaningDto == StatusPlaningDto.passed)
                        {
                            var item = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == ItemUpdate.Id).FirstOrDefault();
                            if (item != null)
                            {
                                this.planningDtosTreated.Remove(item);
                                this.planningDtosStill.Remove(item);
                                décrementCountAppoimentStill();
                            }

                        }else if (ItemUpdate.statusPlaningDto == StatusPlaningDto.Delayed)
                        {
                            var item = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == ItemUpdate.Id).FirstOrDefault();
                            if(item != null)
                            {
                                this.planningDtosTreated.Remove(item);
                                this.planningDtosStill.Remove(item);
                                décrementCountAppoimentStill();
                            }
                      
                        }
                        StateHasChanged();
                    });
                    await hubConnection.StartAsync();
                }


            }
            catch (UnauthorizedException Ex)
            {
                this.NavigationManager.NavigateTo("Login/Home");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                IsLoading = false;
            }
        }
        protected async Task DelayAppoiment()
        {
            try
            {
                this.IsLoading = true;
                await this.medicalPlanningService.DelayeApoimentPatient(new DelayeAppoimentMedical { DateAppoiment = DateAppoiment, Id = IdAppoimentDelyed, statusPlaningDto = StatusPlaningDto.Delayed });

                this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
                this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.IsLoading = false;
            }
            catch(Exception Ex)
            {
                this.ErrorMessage = Ex.Message;
            }
        }
        protected async Task OnNavigateToFileMedical(string IdAppointment)
        {
            this.NavigationManager.NavigateTo($"/FilesMedicalPatient/{IdAppointment.Replace("/","-")}", forceLoad:true);
        }
        protected async Task OnSearch()
        {
            IndexBtnSearshloading = true;
            this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
            this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            IndexBtnSearshloading = false;
        }
        protected async Task OnDelyed(string IdAppoiment)
        {
        
            this.IdAppoimentDelyed =IdAppoiment;

        }
        public async Task OnTreated(string IdAppoiment)
        {
            try
            {
                IndexBtnTwo = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.Treated });
                // var itemTreated = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                // if (itemTreated != null) { this.planningDtosTreated.Add(itemTreated); this.planningDtosStill.Remove(itemTreated); this.planningDtosAbsent.Remove(itemTreated); }
                this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
                this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
          
                IndexBtnTwo = null;

            }
            catch (Exception e)
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
                //  var ItemAbsent = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                //  if (ItemAbsent != null) { this.planningDtosAbsent.Add(ItemAbsent); this.planningDtosStill.Remove(ItemAbsent); }
                this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
                this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                
                IndexBtnOne = null;
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
        }

        public async Task OnPassed(string IdAppoiment)
        {
            try
            {
                IndexBtnthree = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.passed });
                this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientDoctorDto(new KeysAppoimentInformationDoctor { CabinetId = CabinetId, DateAppoiment = DateAppoiment });
                this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
                this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
               
                //   var ItemAbsent = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                //  if (ItemAbsent != null) {  this.planningDtosTreated.Remove(ItemAbsent); this.planningDtosStill.Remove(ItemAbsent); }
                IndexBtnOne = null;
             //  await  décrementCountAppoimentAbsent();
              
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
