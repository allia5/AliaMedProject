using Client.Services.Exceptions;
using Client.Services.Foundations.MedicalPlanningService;
using Client.Services.Foundations.OrdreMedicalService;
using Client.Services.Foundations.SecretaryService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Pages
{
    public class ListAppoimentPatientSecretaryComponentBase : ComponentBase
    {
        [Parameter]
        public string CabinetId { get; set; }
        [Parameter]
        public string DoctorId { get; set; }
        protected bool IndexBtnSearshloading = false;
        protected string ErrorMessage = null;
        protected bool IsLoading = true;
        protected string IndexBtnOne = null;
        protected string IndexBtnTwo = null;
        protected string IndexValidateBtn = null;
        protected DateTime DateAppoiment { get; set; } = DateTime.Now;
        protected PatientInformationDto PatientInformationDto = new PatientInformationDto();
        protected List<InformationOrderMedicalSecritary> informationOrderMedicalSecritaries = new List<InformationOrderMedicalSecritary>();
        protected List<InformationOrderMedicalSecritary> informationOrderMedicalSecritariesValidate = new List<InformationOrderMedicalSecritary>();
        protected List<InformationOrderMedicalSecritary> informationOrderMedicalSecritariesNotValidate = new List<InformationOrderMedicalSecritary>();
        protected List<PlanningDto> planningDtos = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosStill = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosAbsent = new List<PlanningDto>();
        protected List<PlanningDto> planningDtosTreated = new List<PlanningDto>();
        protected HubConnection hubConnection { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IMedicalPlanningService medicalPlanningService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IOrdreMedicalService OrdreMedicalService { get; set; }  

       
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
                    this.informationOrderMedicalSecritaries = await this.OrdreMedicalService.GetAllOrdreMedicalSecritary(new KeysAppoimentInformationSecretary { CabinetId = CabinetId, IdDoctor = DoctorId, DateAppoiment = DateAppoiment });
                    this.informationOrderMedicalSecritariesNotValidate = this.informationOrderMedicalSecritaries.Where(e=>e.informationOrdreMedical.statusOrdreMedical== StatusOrdreMedicalDto.NotValidate).ToList();
                    this.informationOrderMedicalSecritariesValidate = this.informationOrderMedicalSecritaries.Where(e => e.informationOrdreMedical.statusOrdreMedical == StatusOrdreMedicalDto.validate).ToList();
                    this.IsLoading = false;

                    this.hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7104/PlanningAppoimentHub").Build();
                    hubConnection.On<UpdateStatusAppoimentDto>("ReceiveUpdateStatusAppoitment", (ItemUpdate) =>
                    {

                        if (ItemUpdate.statusPlaningDto == StatusPlaningDto.absent)
                        {
                            var item = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == ItemUpdate.Id).FirstOrDefault();
                            if(item != null)
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
                            if(item != null)
                            {
                                this.planningDtosTreated.Remove(item);
                                this.planningDtosStill.Remove(item);
                                décrementCountAppoimentStill();

                            }

                        }
                        else if (ItemUpdate.statusPlaningDto == StatusPlaningDto.Delayed)
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
        protected async Task OnValidateOrdreMedical(string OrdreId)
        {
            try
            {
                IndexValidateBtn = OrdreId;
                var cabinetId = this.CabinetId.Replace("-", "/");
                var doctorId = this.DoctorId.Replace("-", "/");
                await this.OrdreMedicalService.UpdateStatusOrdreMedicalBySecritary(new UpdateOrdreMedicalDto { CabinetId = cabinetId, DoctorId= doctorId, OrdreMedicalId=OrdreId,StatusOrdreMedicalToUpdate=StatusOrdreMedicalDto.validate });
               var ItemOrdreMedical = this.informationOrderMedicalSecritariesNotValidate.Where(e => e.informationOrdreMedical.Id == OrdreId).FirstOrDefault();
                if(ItemOrdreMedical != null) {
                    this.informationOrderMedicalSecritariesNotValidate.Remove(ItemOrdreMedical);
                }
               
                IndexValidateBtn = null;
                
            }
            catch(Exception e)
            {
                this.ErrorMessage= e.Message;
            }
        }
        protected async Task OnShowAccountPatientOrdreMedical(string IdOrdreMedical)
        {
            var ordreMedical = this.informationOrderMedicalSecritariesNotValidate.Where(e=>e.informationOrdreMedical.Id == IdOrdreMedical).FirstOrDefault();
            if(ordreMedical != null)
            {
                this.PatientInformationDto = ordreMedical.PatientInformation;
            }
           
        }
        protected async Task OnSearch()
        {
            IndexBtnSearshloading = true;
            this.planningDtos = await this.medicalPlanningService.GetAppointmentInformationPatientSecretaryDto(new KeysAppoimentInformationSecretary { CabinetId = CabinetId, IdDoctor = DoctorId, DateAppoiment = DateAppoiment });
            this.planningDtosStill = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Still || e.PatientAppoimentInformation.Status == StatusPlaningDto.Delayed).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            this.planningDtosAbsent = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.absent).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            this.planningDtosTreated = planningDtos.Where(e => e.PatientAppoimentInformation.Status == StatusPlaningDto.Treated).OrderBy(e => e.PatientAppoimentInformation.AppoimentCount).ToList();
            IndexBtnSearshloading = false;
        }
        public async Task OnTreated(string IdAppoiment)
        {
            try
            {
                IndexBtnTwo = IdAppoiment;
                await this.medicalPlanningService.UpdateStatusApoimentPatient(new UpdateStatusAppoimentDto { Id = IdAppoiment, statusPlaningDto = StatusPlaningDto.Treated });
                /*var itemTreated=  this.planningDtos.Where(e=>e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                if(itemTreated != null) { this.planningDtosTreated.Add(itemTreated); this.planningDtosStill.Remove(itemTreated); }*/
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
               /* var ItemAbsent = this.planningDtos.Where(e => e.PatientAppoimentInformation.Id == IdAppoiment).FirstOrDefault();
                if (ItemAbsent != null) { this.planningDtosAbsent.Add(ItemAbsent); this.planningDtosStill.Remove(ItemAbsent); }*/
                IndexBtnOne = null;
             //   await décrementCountAppoimentAbsent();
                
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
