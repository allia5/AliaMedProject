using DTO;
using Server.Managers.Storages.MedicalAnalyseClinicManager;
using Server.Models.LineAnalyseMedical;
using Server.Models.LineRadioMedical;
using Server.Models.MedicalsAnalysisClinic;
using Server.Models.ResultAnalyses;
using Server.Models.UserAccount;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.ResultAnalyseService
{
    public static class ResultAnalyseMapperService
    {
        public static FileResultDto MapperToFileResultDto(ResultAnalyse resultAnalyse)
        {
            return new FileResultDto
            {
                DataFile =DecryptFile( resultAnalyse?.AnalyseResult),
                FileType = resultAnalyse?.fileType
            };
        }
        public static ResultAnalyse MapperToResultAnalyse(Guid IdLineAnalyse,string FileType, byte[] fileUpload)
        {
            return new ResultAnalyse
            {
                AnalyseResult= EncryptFile(fileUpload),
                fileType=FileType,
                IdLineAnalyse=IdLineAnalyse,
                Id=Guid.NewGuid()
                
                
            };
        }
        public static LineAnalyseMedicals MapperToLineAnalyseMedicals(LineAnalyseMedicals lineAnalyseMedicals,Guid IdSpecialisteAnalyse)
        {
            lineAnalyseMedicals.IdSpecialisteAnalyse=IdSpecialisteAnalyse;
            lineAnalyseMedicals.DateValidation = DateTime.Now;
            lineAnalyseMedicals.Status = Models.Analyse.StatusAnalyse.validate;
            return lineAnalyseMedicals;
        }
        public static MailRequest MapperToMailRequestAddAnalyseResult(User UserAccountPatient, MedicalAnalysisClinic medicalAnalysisClinic, LineAnalyseMedicals lineAnalyseMedicals)
        {
            return new MailRequest
            {
                ToEmail = UserAccountPatient.Email,
                Subject = "Result Radio Notification ",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n      " +
                $" <h3> Dawi.dz </h3>\r\n  " +
                $"  </div>\r\n    <div class=card-body>\r\n  " +
                $"    <h5 class=card-title> The Analyse results {lineAnalyseMedicals.description}  are ready  <br/>" +
                $" <h1 class=\"display-1\"></h1><br/> by Clinic Analyse : {medicalAnalysisClinic.MedicalAnalysisName}  </p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>"

            };
        }
    }
}
