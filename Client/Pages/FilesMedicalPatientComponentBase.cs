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
        protected string MedicalIdentificationNumber = "";
        protected bool ButtonOnMove = false;
        protected FileMedicalMainPatientDto FilesMainPatient = null;
        protected UpdateFileMedicalDto FileMedicalToUpdate =null;
        protected FileMedicalToAddDto fileMedicalToAdd = new FileMedicalToAddDto();

        protected List<chronicDiseasesDto> chronicDiseasesUpdateDtosNotIn = new  List<chronicDiseasesDto>();
        protected List<chronicDiseasesDto> chronicDiseasesDtos = new List<chronicDiseasesDto>();
        protected List<chronicDiseasesDto> chronicDiseasesDtosPatient = new List<chronicDiseasesDto>();
        protected List<chronicDiseasesDto> chronicDiseasesDtosToAdd = new List<chronicDiseasesDto>();
        protected List<chronicDiseasesDto> chronicDiseasesDtosNotInToAdd = new List<chronicDiseasesDto>();
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
                    this.chronicDiseasesDtosNotInToAdd = chronicDiseasesDtos;
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
                this.IsLoading = false;
            }
          

          
        }
        protected async Task TransferFileMedical()
        {
            try
            {
                ButtonOnMove = true;
                await this.FilemedicalService.TransferFileMedical(new FileTransferDto {AppointmentId=IdAppointment,IdMedical=MedicalIdentificationNumber });
                this.FilesMainPatient = await this.FilemedicalService.GetAllFileMedicalMainPatient(IdAppointment);
                ButtonOnMove = false;

            }
            catch(Exception e)
            {
                this.ErrorMessage = e.Message;
                ButtonOnMove = false;
            }
        }
        /*function update File Medical*/
        public async Task OnUpdateFileMedical()
        {
            try
            {
                FileMedicalToUpdate.AppointmentId = IdAppointment;
                await this.FilemedicalService.UpdateFileMedicalPatient(FileMedicalToUpdate);
                this.SuccessMessage = "Operation Suucess";

            }
            catch(Exception ex)
            {
                this.ErrorMessage= ex.Message;
            }
           

        }
        public async Task DeleteMaladieOnUpdate(chronicDiseasesDto chronicDiseasesDto)
        {
            this.FileMedicalToUpdate.ChronicDiseases.Remove(chronicDiseasesDto);
            this.chronicDiseasesUpdateDtosNotIn.Add(chronicDiseasesDto);

        }
        public async Task AddMaladieOnUpdate(chronicDiseasesDto chronicDiseasesDto)
        {
            this.FileMedicalToUpdate.ChronicDiseases.Add(chronicDiseasesDto);
            this.chronicDiseasesUpdateDtosNotIn.Remove(chronicDiseasesDto);
        }
        

        public async Task GetInformationFileMedicalUpdate(string FileMedicalId )
        {
            this.FileMedicalToUpdate = new UpdateFileMedicalDto();
            this.FileMedicalToUpdate.ChronicDiseases = new List<chronicDiseasesDto>();
            this.chronicDiseasesUpdateDtosNotIn = new List<chronicDiseasesDto>();
            var FileMedicalPatient = this.FilesMainPatient.fileMedicals.Where(e => e.Id == FileMedicalId).FirstOrDefault();
            this.FileMedicalToUpdate.ChronicDiseases = FileMedicalPatient.chronicDiseases;
          
            this.FileMedicalToUpdate.DateOfBirth = FileMedicalPatient.DateOfBirth;
            this.FileMedicalToUpdate.FirstName= FileMedicalPatient.FirstName;
            this.FileMedicalToUpdate.LastName= FileMedicalPatient.LastName;
            this.FileMedicalToUpdate.Sexe = FileMedicalPatient.Sexe;
            this.FileMedicalToUpdate.FileId = FileMedicalPatient.Id;
            bool index = false;

           foreach (var itemOne in this.chronicDiseasesDtos.ToList())
            {
                foreach (var itemTwo in this.FileMedicalToUpdate.ChronicDiseases.ToList())
                {
                    if(itemOne.name == itemTwo.name)
                    {
                        index = true;
                       
                    }
                   
                }
                if (index == false)
                {
                    this.chronicDiseasesUpdateDtosNotIn.Add(itemOne);
                }
               
                index = false;
            }

        }


        /*Function Show Chronic Disease*/

        public async Task ShowchronicDiseases(string IdFileMedical)
        {
            var FileMedicalPatient = this.FilesMainPatient.fileMedicals.Where(e => e.Id == IdFileMedical).FirstOrDefault();
            this.chronicDiseasesDtosPatient = FileMedicalPatient.chronicDiseases;
        }






        /*Function Add Files*/


        protected async Task BtnOnAddFile()
        {
            chronicDiseasesDtosNotInToAdd = this.chronicDiseasesDtos;
        }

        public async Task AjouterMaladie(chronicDiseasesDto chronicDiseases)
        {

            this.chronicDiseasesDtosToAdd.Add(chronicDiseases);
            this.chronicDiseasesDtosNotInToAdd.Remove(chronicDiseases);
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
                this.chronicDiseasesDtos = await this.chronicDiseasesService.GetChronicDiseasesAsync();
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
        }
        public async Task OnNavigateToOrdreMedical(string FileId)
        {
            this.NavigationManager.NavigateTo($"/OrdreMidical/{IdAppointment.Replace("/","-")}/{FileId.Replace("/", "-")}", forceLoad: true);
        }

    }
}
