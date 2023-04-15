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
        [Parameter]
        public string IdAppointment { get; set; }
        [Inject]
        public IfileMedicalService medicalService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
      
        protected override Task OnInitializedAsync()
        {
            
            return base.OnInitializedAsync();
        }

        protected async Task OnAddFileMedical()
        {
            try
            {
                fileMedicalToAdd.IdAppointment = IdAppointment;
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
