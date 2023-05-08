using DTO;
using Server.Managers.Storages.RadioResultManager;
using Server.Models.LineRadioMedical;
using Server.Models.Radiologys;
using Server.Models.RadioMedical;
using Server.Models.ResultAnalyses;
using Server.Models.ResultsRadio;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
namespace Server.Services.Foundation.ResultRadioService
{
    public static class ResultRadioMapperService
    {
        public static FileResultDto MapperToFileResultDto(ResultRadio resultRadio)
        {
            return new FileResultDto
            {
                DataFile = DecryptFile( resultRadio?.FileResult),
                FileType = resultRadio?.fileType
            };
        }
        public static ResultRadio MapperToResultRadio(Guid LineRadioId,string TypeFileUpload,RadioResultToAddDto resultToAddDto)
        {
            return new ResultRadio
            {
                Id=Guid.NewGuid(),
                FileResult=EncryptFile(resultToAddDto.FileUpload),
                IdLineRadio = LineRadioId,
                fileType =TypeFileUpload
                 
            };
        }
        public static LineRadioMedicals MapperToLineRadioMedical(LineRadioMedicals lineRadioMedicals,Radiology radiology)
        {
            lineRadioMedicals.DateValidation = DateTime.Now;
            lineRadioMedicals.Status = StatusRadio.validate;
            lineRadioMedicals.IdRadiology = radiology.Id;
            return lineRadioMedicals;
        }
        public static MailRequest MapperToMailRequestAddRadioResult(User UserAccountPatient, User UserAccountRadiology,LineRadioMedicals lineRadioMedicals)
        {
            return new MailRequest
            {
                ToEmail = UserAccountPatient.Email,
                Subject = "Result Radio Notification ",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n      " +
                $" <h3> AliaMed.Com </h3>\r\n  " +
                $"  </div>\r\n    <div class=card-body>\r\n  " +
                $"    <h5 class=card-title> The x-ray results {lineRadioMedicals.Description}  are ready  <br/>" +
                $" <h1 class=\"display-1\"></h1><br/> by Doctor : {UserAccountRadiology.LastName} , {UserAccountRadiology.Firstname} </p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>"

            };
        }
    }
}
