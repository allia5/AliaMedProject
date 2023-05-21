using DTO;
using iTextSharp.text;
using Server.Models.LineAnalyseMedical;
using Server.Models.Pharmacist;
using Server.Models.Pharmacys;
using Server.Models.PrescriptionLine;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.PrescriptionLineService
{
    public static class PrescriptionLineMapperService
    {
        public static PrescriptionLines MapperToPrescriptionLines(PrescriptionLines prescriptionLines , Guid PharmacistId )
        {
            prescriptionLines.IdPharmacist = PharmacistId;
            prescriptionLines.DateValidation= DateTime.Now;
            prescriptionLines.StatusPrescriptionLine = StatusPrescriptionLine.Validate;
           return prescriptionLines;

        }
        public static MailRequest MapperToMailRequestOnUpdateStatusPrescriptionLine(PrescriptionLines prescriptionLines,Pharmacy pharmacy,User UserAccountPatient)
        {
            return new MailRequest
            {
                ToEmail = UserAccountPatient.Email,
                Subject = "Result Prescription Notification ",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n      " +
               $" <h3> Dawi.dz </h3>\r\n  " +
               $"  </div>\r\n    <div class=card-body>\r\n  " +
               $"    <h5 class=card-title> Medicament {prescriptionLines.MedicamentName}  are Validated  <br/>" +
               $" <h1 class=\"display-1\"></h1><br/> by Pharmacien : {pharmacy.PharmacistName}  </p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>"

            };
        }
    }
}
