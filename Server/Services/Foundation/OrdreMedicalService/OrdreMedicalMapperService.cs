
using DTO;
using QRCoder;
using Server.Models.MedicalOrder;
using static Server.Utility.Utility;
using Server.Models.Prescriptions;
using Server.Models.PrescriptionLine;
using Server.Models.RadioMedical;
using Server.Models.Analyse;
using Server.Models.UserAccount;
using Server.Models.fileMedical;
using Server.Models.secretary;
using iTextSharp.text.pdf;
using iTextSharp.text;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf.parser;
using AngleSharp.Text;

namespace Server.Services.Foundation.OrdreMedicalService
{
    public static class OrdreMedicalMapperService
    {
        public static MailRequest MapperToMailRequestUpdateStatusOrdreMedical(User UserAccountPatient,User UserAccountDoctor)
        {
            return new MailRequest
            {
                ToEmail = UserAccountPatient.Email,
                Subject = "Notification",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n       <h3> AliaMed.Com </h3>\r\n    </div>\r\n    <div class=card-body>\r\n      <h5 class=card-title> Ordre Medical status notification  </h5>\r\n        <p class=card-text>Ordre Medical Has been Validate {DateTime.Now }  <br/> by Doctor :{UserAccountDoctor.Firstname} ,{UserAccountDoctor.LastName}</p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>"

            };
        }
        public static MedicalOrdres MapperToNewOrdreMedical(MedicalOrdres medicalOrdre , UpdateOrdreMedicalDto updateOrdreMedicalDto,Secretarys secretarys)
        {
            medicalOrdre.Status = (StatuseOrdreMedical)updateOrdreMedicalDto.StatusOrdreMedicalToUpdate;
            medicalOrdre.DateValidation = DateTime.Now;
            medicalOrdre.IdSecritary = secretarys.Id;
            return medicalOrdre;
            

        }
        public static InformationOrderMedicalSecritary MapperToInformationOrderMedicalSecritary(PatientInformationDto patientInformationDto,InformationFileMedical informationFileMedical,InformationOrdreMedical informationOrdreMedical)
        {
            return new InformationOrderMedicalSecritary
            {
                PatientInformation = patientInformationDto,
                informationFile = informationFileMedical,
                informationOrdreMedical = informationOrdreMedical
            };
        }
        public static InformationOrdreMedical mapperToInformationOrdreMedical(MedicalOrdres medicalOrdres)
        {
            return new InformationOrdreMedical
            {
                Id = EncryptGuid(medicalOrdres.Id),
                Summary = medicalOrdres.summary,
                DateCreate = medicalOrdres.ReleaseDate,
                statusOrdreMedical = (StatusOrdreMedicalDto)medicalOrdres.Status,

            };
        }

        public static InformationFileMedical MapperToInformationFileMedical(fileMedicals fileMedicals)
        {
            return new InformationFileMedical
            {
                DateBirth = fileMedicals.DateOfBirth,
                FirstName = fileMedicals.firstname,
                LastName = fileMedicals.Lastname,
                sexe = (Sexe)fileMedicals.Sexe
            };
        }




        public static byte[] AddTextToPdf(byte[] pdfBytes, string text,float k)
        {
            using (MemoryStream inputPdfStream = new MemoryStream(pdfBytes))
            {
                using (MemoryStream outputPdfStream = new MemoryStream())
                {
                    PdfReader pdfReader = new PdfReader(inputPdfStream);
                    PdfStamper pdfStamper = new PdfStamper(pdfReader, outputPdfStream);
                    PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);
                    
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    pdfContentByte.BeginText();
                    pdfContentByte.SetFontAndSize(baseFont, 12);
                    pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, text, pdfReader.GetPageSize(1).Width / 2, pdfReader.GetPageSize(1).Height /(2+k), 0);

                    pdfContentByte.EndText();
                    pdfStamper.Close();
                    return outputPdfStream.ToArray();
                }
            }
        }
     
       











        public static byte[] AddStringToFile(byte[] fileBytes, string stringToAdd)
        {
            string fileString = System.Text.Encoding.Default.GetString(fileBytes);
            fileString += stringToAdd;
            return System.Text.Encoding.Default.GetBytes(fileString);
        }
      








        public static MailRequest MapperMailRequestDeleteMedicalAppoiment(User userAccountPatient,User UserAccountDoctor)
        {
            return new MailRequest
            {
                ToEmail = userAccountPatient.Email,
                Subject = "Notification",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n       <h3> AliaMed.Com </h3>\r\n    </div>\r\n    <div class=card-body>\r\n      <h5 class=card-title> You are recive Ordrere Medical  <br/> <h1 class=\"display-1\"></h1><br/> by Doctor :{UserAccountDoctor.Firstname} ,{UserAccountDoctor.LastName}</p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>"

            };
        }






        public static Analyses MapperToAnalyse(AnalyseToAddDto analyseToAddDto, Guid IdOrdreMedical)
        {
            return new Analyses
            {
                Id = Guid.NewGuid(),
                description = analyseToAddDto.Description,
                IdOrdreMedical = IdOrdreMedical,
                IdMedicalAnalyse = null,
                Instruction = analyseToAddDto.Instruction,
                Status = StatusAnalyse.validate,
                DateValidation = null,
            };
        }


        public static Radio MapperToRadio(RadioToAddDto radioDto,Guid IdOrdreMedical)
        {
            return new Radio
            {
                Id = Guid.NewGuid(),
                Description = radioDto.Description,
                IdOrdreMedical = IdOrdreMedical,
                IdRadiology=null,
                Instruction=radioDto.Instruction,
                Status=StatusRadio.notValidate,
                DateValidation=null,
                
                

                
            };
        }
        public static PrescriptionLines MapperToPrescriptionLine(Guid PrescriptionId , PrescriptionLineDto prescriptionDto)
        {
            return new PrescriptionLines
            {
                Id = Guid.NewGuid(),
                Dosage = prescriptionDto.Quantity,
                IdPrescription = PrescriptionId,
                DateValidation = null,
                IdPharmacist = null,
                StatusPrescriptionLine = StatusPrescriptionLine.NotValidate,
                Description = prescriptionDto.Description,
                MedicamentName = prescriptionDto.MedicamentName
            };
        }
        public static Prescription  MapperToPrescription(OrderMedicalToAddDro orderMedicalToAddDro,MedicalOrdres medicalOrdres)
        {
            return new Prescription
            {
                Id = Guid.NewGuid(),

                IdMedicalOrdre = medicalOrdres.Id,
                instruction = orderMedicalToAddDro.Prescription.Instruction,
                FilePrescription = orderMedicalToAddDro.Prescription.PrescriptionFile


            };
        }
        public static MedicalOrdres MapperToMedicalOrdre(Guid DoctorId , Guid CabinetId , OrderMedicalToAddDro orderMedicalToAdd)
        {

            return new MedicalOrdres
            {
                Id = Guid.NewGuid(),
                DateValidation = null,
                IdCabinetMedical = CabinetId,
                IdDoctor = DoctorId,
                IdSecritary = null,
                IdFileMedical = DecryptGuid(orderMedicalToAdd.FileId),
                ReleaseDate = DateTime.Now,
                Status = StatuseOrdreMedical.NotValidate,
                summary = orderMedicalToAdd.Summary,
                Visibility = (Models.MedicalOrder.StatusVisibility)orderMedicalToAdd.Visibility,
                
                
            };
        }
    }
}
