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
        public IfileMedicalService medicalService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
      
        protected override async Task OnInitializedAsync()
        {
          
            var ResultAuth = await this.AuthentificationStatService.GetAuthenticationStateAsync();
            if (ResultAuth.User.Identity?.IsAuthenticated ?? false)
            {
                this.chronicDiseasesDtos = await this.chronicDiseasesService.GetChronicDiseasesAsync();
                bool isIn = false;
                foreach (var Item in chronicDiseasesDtos.ToList())
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
                }
            }
            else
            {
                this.NavigationManager.NavigateTo("/Login/Home");
            }

          
        }

        public async Task AjouterMaladie(chronicDiseasesDto chronicDiseases)
        {
            this.chronicDiseasesDtos.Add(chronicDiseases);
            this.chronicDiseasesDtosToAdd.Remove(chronicDiseases);
        }

        protected async Task OnAddFileMedical()
        {
            try
            {
                fileMedicalToAdd.IdAppointment = IdAppointment;
                fileMedicalToAdd.chronicDiseases = chronicDiseasesDtos;
                var result = await this.medicalService.PostFileMedicalPatientAsync(fileMedicalToAdd);
                this.SuccessMessage = "Operation Success";
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
        }

    }
}
