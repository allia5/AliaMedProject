using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.ChronicDiseasesService;
using Client.Services.Foundations.FileMedicalService;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class FilesMedicalPatientComponentBase : ComponentBase
    {
        protected bool IsLoading = true;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected FileMedicalMainPatientDto FilesMainPatient = null;
        protected FileMedicalToAddDto fileMedicalToAdd = new FileMedicalToAddDto();
        protected List<chronicDiseasesDto> chronicDiseasesDtos = new List<chronicDiseasesDto>();
        protected List<chronicDiseasesDto> chronicDiseasesDtosToAdd = new List<chronicDiseasesDto>();
        [Parameter]
        public string IdAppointment { get; set; }
        [Inject]
        public AuthentificationStatService AuthentificationStatService { get; set; }
        [Inject]
        public IChronicDiseasesService chronicDiseasesService { get; set; }
        [Inject]
        public IfileMedicalService FilemedicalService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
      
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var ResultAuth = await this.AuthentificationStatService.GetAuthenticationStateAsync();
                if (ResultAuth.User.Identity?.IsAuthenticated ?? false)
                {
                    this.IsLoading = true;
                    this.FilesMainPatient = await this.FilemedicalService.GetAllFileMedicalMainPatient(IdAppointment);
                    this.chronicDiseasesDtos = await this.chronicDiseasesService.GetChronicDiseasesAsync();
                    this.IsLoading = false;
                    /*     foreach (var Item in chronicDiseasesDtos.ToList())
                         {
                             foreach(var ItemToAdd in chronicDiseasesDtosToAdd.ToList())
                             {
                                 if(Item == ItemToAdd)
                                 {
                                     isIn = true;
                                 }
                             }
                             if(isIn==false)
                             {
                                 chronicDiseasesDtosToAdd.Add(Item);
                                 chronicDiseasesDtos.Remove(Item);
                             }
                             isIn = false;
                         }*/
                }
                else
                {
                    this.NavigationManager.NavigateTo("/Login/Home"); this.IsLoading = false;
                }
            }
            catch(Exception ex)
            {
                this.ErrorMessage=ex.Message;
            }
          

          
        }

        public async Task AjouterMaladie(chronicDiseasesDto chronicDiseases)
        {
            this.chronicDiseasesDtosToAdd.Add(chronicDiseases);
            this.chronicDiseasesDtos.Remove(chronicDiseases);
        }

        protected async Task OnAddFileMedical()
        {
            try
            {
                fileMedicalToAdd.IdAppointment = IdAppointment;
                fileMedicalToAdd.chronicDiseases = chronicDiseasesDtosToAdd;
                var result = await this.FilemedicalService.PostFileMedicalPatientAsync(fileMedicalToAdd);
                this.FilesMainPatient.fileMedicals.Add(result);
                this.SuccessMessage = "Operation Success";
                chronicDiseasesDtosToAdd = new List<chronicDiseasesDto>();
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
        }

    }
}
