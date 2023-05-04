using Client.Services.Foundations.AdviceMedicalService;
using Client.Services.Foundations.AnalyseMedicalService;
using Client.Services.Foundations.LineAnalyseResultService;
using Client.Services.Foundations.LineRadioResultService;
using Client.Services.Foundations.OrdreMedicalService;
using Client.Services.Foundations.PrescriptionService;
using Client.Services.Foundations.RadioMedicalService;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public class OrdreMedicalAdvicesDoctorComponentBase:ComponentBase
    {
        protected string MessageSend = "";
        protected bool isLoading = false;
        protected string ErrorMessage = null;
        protected string SuccessMessage = null;
        protected string OrdreMedcialIdSelected = null;
     
        protected List<MedicalAdviceDoctorDto> medicalAdviceDoctorDtos = new List<MedicalAdviceDoctorDto>();
        protected List<AdviceMedicalDto> adviceMedicalDtos = null;
        protected MedicalOrdreDetails medicalOrdreDetails = new MedicalOrdreDetails();
        protected PrescriptionLineInformationDto prescriptionLineInformation = new PrescriptionLineInformationDto();

        [Parameter]
        public string FileId { get; set; }
        [Inject]
        protected IRadioMedicalService radioMedicalService { get; set; }
        [Inject]
        protected IAnalyseMedicalService analyseMedicalService { get; set; }
        [Inject]
        protected IPrescriptionService prescriptionService { get; set; }
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
        [Inject]
        protected IAdviceMedicalService adviceMedicalService { get; set; }

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
                    this.medicalAdviceDoctorDtos = await this.OrdreMedicalService.GetMedicalAdviceDoctor();
                    this.medicalAdviceDoctorDtos = this.medicalAdviceDoctorDtos.OrderByDescending(e => e.CountMessageNotViewed).ToList();
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

        public async Task PostNewAdviceMedical(string OrdreMedicalId)
        {
            try
            {
                await this.adviceMedicalService.DoctorPostNewAdviceMedicalPatient(new MedicalAdviceToAddDto { Message = MessageSend, OrdreMedicalId = OrdreMedicalId });
                this.adviceMedicalDtos = await this.adviceMedicalService.GetAdvicesMedicalDoctor(OrdreMedicalId);
                this.adviceMedicalDtos = this.adviceMedicalDtos.OrderBy(e => e.DateSend).ToList();
            }
            catch (Exception Ex)
            {
                this.ErrorMessage = Ex.Message;
            }

        }
        public async Task OpenAdviceMedical(string IdOrdreMedical)
        {
            this.OrdreMedcialIdSelected = IdOrdreMedical;
            this.adviceMedicalDtos = await this.adviceMedicalService.GetAdvicesMedicalDoctor(IdOrdreMedical);
            this.adviceMedicalDtos = this.adviceMedicalDtos.OrderBy(e => e.DateSend).ToList();
            this.medicalAdviceDoctorDtos = await this.OrdreMedicalService.GetMedicalAdviceDoctor();
            this.medicalAdviceDoctorDtos = this.medicalAdviceDoctorDtos.OrderByDescending(e => e.CountMessageNotViewed).ToList();


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

            var OrdreMedicalAdvice = this.medicalAdviceDoctorDtos.Where(e => e.MedicalOrdreDetails.Id == IdOrdreMedical).FirstOrDefault();
            this.medicalOrdreDetails = OrdreMedicalAdvice?.MedicalOrdreDetails ?? new MedicalOrdreDetails();
        }
      /*  protected async Task DownloadFilePrescription(string OrdreMedicalId)
        {
            try
            {


                // Save the file
                var stream = await this.prescriptionService.GetMedicalFilePrescription(OrdreMedicalId);
                using var streamRef = new DotNetStreamReference(stream: stream);
                var fileName = "File.pdf";
                await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }
        protected async Task DownloadFileRadio(string OrdreMedicalId)
        {
            var stream = await this.radioMedicalService.GetMedicalFileRadio(OrdreMedicalId);
            using var streamRef = new DotNetStreamReference(stream: stream);
            var fileName = "FileRadio.pdf";
            await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
        protected async Task DownloadFileAnalyse(string OrdreMedicalId)
        {
            var stream = await this.analyseMedicalService.GetMedicalFileAnalyse(OrdreMedicalId);
            using var streamRef = new DotNetStreamReference(stream: stream);
            var fileName = "FileAnalyse.pdf";
            await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }*/
    }
}
