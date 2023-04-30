using Client.Services.Foundations.LineAnalyseResultService;
using Client.Services.Foundations.LineRadioResultService;
using Client.Services.Foundations.OrdreMedicalService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public class ArchiveMedicalFilePatientComponentBase:ComponentBase
    {
        protected bool isLoading = false;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected MedicalFileArchiveDto MedicalFileArchive = new MedicalFileArchiveDto();
        protected List<MedicalOrdresDto> ListmedicalOrdre = new List<MedicalOrdresDto>();
        protected MedicalOrdreDetails medicalOrdreDetails = new MedicalOrdreDetails();
        protected PrescriptionLineInformationDto prescriptionLineInformation = new PrescriptionLineInformationDto();
        [Parameter]
        public string FileId { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IOrdreMedicalService OrdreMedicalService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected ILineAnalyseResultService LineAnalyseResultService { get; set; }
        [Inject]
        protected ILineRadioResultService lineRadioResultService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("import", "/Js/ScripteFileMedical.js");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            var AuthUser = await this.AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (AuthUser.User.Identity?.IsAuthenticated ?? false)
            {
                try
                {

                    this.MedicalFileArchive = await this.OrdreMedicalService.GetMedicalFileArchivePatient(FileId);
                    this.ListmedicalOrdre = this.MedicalFileArchive.medicalOrdres.OrderByDescending(e => e.medicalOrdreDetails.DateValidation).ToList();
                    isLoading = false;
                }
                catch (Exception Ex)
                {
                    this.ErrorMessage = Ex.Message;
                    isLoading = false;
                }

            }
            else
            {
                this.NavigationManager.NavigateTo("/Login/Home", forceLoad: true);
            }

        }
        protected async Task DownloadFileResultRadio(string LineRadioId)
        {
            try
            {


                // Save the file
                var FileResult = await this.lineRadioResultService.GetFileResultRadioPatient(LineRadioId);
                if (FileResult.DataFile != null && FileResult.FileType != null)
                {
                    MemoryStream stream = new MemoryStream(FileResult.DataFile);
                    using var streamRef = new DotNetStreamReference(stream: stream);
                    var fileName = "File" + FileResult.FileType;
                    await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
                }
                else
                {
                    throw new Exception("File Is Empty");
                }




            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }



        }

        protected async Task DownloadFileResultAnalyse(string LineAnalyseId)
        {
            try
            {


                // Save the file
                var FileResult = await this.LineAnalyseResultService.GetFileResultAnalysePatient(LineAnalyseId);
                if (FileResult.DataFile != null && FileResult.FileType != null)
                {
                    MemoryStream stream = new MemoryStream(FileResult.DataFile);
                    using var streamRef = new DotNetStreamReference(stream: stream);
                    var fileName = "FileAnalyse" + FileResult.FileType;
                    await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
                }
                else
                {
                    throw new Exception("File Is Empty");
                }




            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }



        }
        public async Task OnSelectOrdreMedical(string IdOrdreMedical)
        {
            var OrdreMedical = this.MedicalFileArchive.medicalOrdres.Where(e => e.medicalOrdreDetails.Id == IdOrdreMedical).FirstOrDefault();
            this.medicalOrdreDetails = OrdreMedical?.medicalOrdreDetails ?? new MedicalOrdreDetails();
        }
    }
}
