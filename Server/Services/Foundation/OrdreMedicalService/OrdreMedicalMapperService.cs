
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
using System.Drawing.Imaging;
using iTextSharp.text.pdf.qrcode;
using ZXing.QrCode.Internal;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.Wordprocessing;
using Server.Models.LineRadioMedical;
using Server.Models.LineAnalyseMedical;
using Server.Models.AdviceMedicals;

namespace Server.Services.Foundation.OrdreMedicalService
{
    public static class OrdreMedicalMapperService
    {

        public static AdviceMedicalDto MapperToAdviceMedical( User UserAccountReceiver,AdviceMedical adviceMedical)
        {
            return new AdviceMedicalDto
            {
              Id =  EncryptGuid( adviceMedical.Id),
              DateSend = adviceMedical.DateSendMessage,
              FullNameReceiver= UserAccountReceiver.Firstname + UserAccountReceiver.LastName,
             
              Message=adviceMedical.Message,
              StatusViewReceiver= (StatusViewReceiverDto)adviceMedical.StatusViewReceiver
            };
        }
        public static PrescriptionLineInformationDto MapperToPrescriptionLineInformationDto(PrescriptionLines prescriptionLines)
        {
            return new PrescriptionLineInformationDto
            {
                DateValidation = prescriptionLines?.DateValidation?? null ,
                IdLinePrescription = EncryptGuid(prescriptionLines.Id),
                MedicamentName = prescriptionLines.MedicamentName,
                Quantity = prescriptionLines.Dosage,
                statusPrescriptionLineDto = (StatusPrescriptionLineDto)prescriptionLines.StatusPrescriptionLine

            };
        }
        public static RadioLineInformationDto MapperToRadioLineInformationDto(LineRadioMedicals lineRadioMedicals)
        {
            return new RadioLineInformationDto
            {
                IdLineRadio = EncryptGuid(lineRadioMedicals.Id),
                Description = lineRadioMedicals.Description,
                statusRadio = (StatusRadioDto)lineRadioMedicals.Status,
                
            };
        }
        public static AnalyseLineInformationDto MapperToAnalyseLineInformationDto(LineAnalyseMedicals lineAnalyseMedicals)
        {
            return new AnalyseLineInformationDto
            {
                IdLineAnalyse = EncryptGuid(lineAnalyseMedicals.Id),
                Description = lineAnalyseMedicals.description,
                statusAnalyse = (StatusAnalyseDto)lineAnalyseMedicals.Status
            };
        }

        public static MedicalOrdreDetails MapperToMedicalOrdreDetails(MedicalOrdres medicalOrdres,List<AnalyseLineInformationDto> ListanalyseLineInformationDtos , List<RadioLineInformationDto> LineradioLineInformationDtos,List<PrescriptionLineInformationDto> ListPrescriptionLineInformationDtos)
        {
            return new MedicalOrdreDetails
            {
                Id = EncryptGuid(medicalOrdres.Id),
                analyseLinesInformation = ListanalyseLineInformationDtos,
                DateValidation = medicalOrdres?.DateValidation ?? null,
                prescriptionLinesInformation = ListPrescriptionLineInformationDtos,
                radioLinesInformation = LineradioLineInformationDtos,
                Summary = medicalOrdres.summary

            };
        }
        public static MedicalOrdresDto MapperToMedicalOrdresDto(MedicalOrdreDetails medicalOrdreDetails , DoctorInformationDto doctorInformationDto)
        {
            return new MedicalOrdresDto
            {
                medicalOrdreDetails = medicalOrdreDetails,
                doctorInformation = doctorInformationDto

            };
        }
        public static MedicalOrdresPatientDto MapperToMedicalOrdresPatientDto(MedicalOrdreDetails medicalOrdreDetails, DoctorInformationDto doctorInformationDto,List<AdviceMedicalDto> adviceMedicalDtos)
        {
            return new MedicalOrdresPatientDto
            {
                medicalOrdreDetails = medicalOrdreDetails,
                doctorInformation = doctorInformationDto,
                adviceMedicalsDto = adviceMedicalDtos
                
                

            };
        }
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



        public static byte[] InsertCodeQrIntoPdf(byte[] pdfBytes,string CodeQr,int x,int y)
        {
            // Créer un MemoryStream à partir du tableau de bytes PDF en entrée
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            {
                // Créer un MemoryStream à partir du tableau de bytes de l'image en entrée
              
                    // Ouvrir le fichier PDF existant avec iTextSharp
                    PdfReader pdfReader = new PdfReader(pdfStream);

                    // Créer un MemoryStream pour le nouveau fichier PDF
                    using (MemoryStream outputMemoryStream = new MemoryStream())
                    {
                        // Créer un objet PdfStamper pour écrire dans le nouveau document PDF
                        using (PdfStamper pdfStamper = new PdfStamper(pdfReader, outputMemoryStream))
                        {
                            // Récupérer la première page du document PDF
                            PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);

                            BarcodeQRCode qrcode = new BarcodeQRCode(CodeQr, 100, 100, null);
                            Image image = qrcode.GetImage();
                            // Créer un objet Image à partir du tableau de bytes de l'image en entrée
                           // Image image = Image.GetInstance(imageBytes);

                            // Positionner l'image dans le coin supérieur gauche de la première page
                            image.SetAbsolutePosition(x,y);
                    

                        // Ajouter l'image à la première page
                        pdfContentByte.AddImage(image);
                            

                            // Fermer le PdfStamper
                            pdfStamper.Close();

                            // Retourner le tableau de bytes du nouveau document PDF
                            return outputMemoryStream.ToArray();
                        }
                    }
                
            }
        }




       















        public static byte[] AddInfromationFileToToPdf(byte[] pdfBytes, fileMedicals fileMedicals)
        {
            using (MemoryStream inputPdfStream = new MemoryStream(pdfBytes))
            {
                using (MemoryStream outputPdfStream = new MemoryStream())
                {
                    PdfReader pdfReader = new PdfReader(inputPdfStream);
                    PdfStamper pdfStamper = new PdfStamper(pdfReader, outputPdfStream);
                    PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);
                    var text = "First name:  " + fileMedicals.firstname + "   Last Name:" + fileMedicals.Lastname + "   date Birth:" + fileMedicals.DateOfBirth + "   Sexe:" + fileMedicals.Sexe;
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    pdfContentByte.BeginText();
                    pdfContentByte.SetFontAndSize(baseFont, 12);
                    pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, text, pdfReader.GetPageSize(1).Width / 2, pdfReader.GetPageSize(1).Height / (2), 0);

                    pdfContentByte.EndText();
                    pdfStamper.Close();
                    return outputPdfStream.ToArray();
                }
            }
        }
        public static byte[] AddInfromationDoctorToToPdf(byte[] pdfBytes, User UserAccountDoctor ,float k)
        {
            using (MemoryStream inputPdfStream = new MemoryStream(pdfBytes))
            {
                using (MemoryStream outputPdfStream = new MemoryStream())
                {
                    PdfReader pdfReader = new PdfReader(inputPdfStream);
                    PdfStamper pdfStamper = new PdfStamper(pdfReader, outputPdfStream);
                    PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);
                    
                    var text =   UserAccountDoctor.Firstname + "         " + UserAccountDoctor.LastName+"    "+ DateTime.Now;
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    pdfContentByte.BeginText();
                    pdfContentByte.SetFontAndSize(baseFont, 12);
                    pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, "By Doctor :", pdfReader.GetPageSize(1).Width / 2, (float)(pdfReader.GetPageSize(1).Height * (0.5 - k)), 0);
                    pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, text, pdfReader.GetPageSize(1).Width / 2, (float)(pdfReader.GetPageSize(1).Height * (float)(0.5 - k-0.02)), 0);

                    pdfContentByte.EndText();
                    pdfStamper.Close();
                    return outputPdfStream.ToArray();
                }
            }
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
                    pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, text, pdfReader.GetPageSize(1).Width / 2, (float)(pdfReader.GetPageSize(1).Height * (0.5-k)) /*/(2+k)*/, 0);

                    pdfContentByte.EndText();
                    pdfStamper.Close();
                    return outputPdfStream.ToArray();
                }
            }
        }


        public static byte[] AddTextToPdfAnalyseRadio(byte[] pdfBytes, string Description, string Instruction)
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
                    Description = "Description :" + Description;
                    Instruction = "Instruction :" + Instruction;
                    pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Description, pdfReader.GetPageSize(1).Width / 2, (float)(pdfReader.GetPageSize(1).Height / (2+0.5)), 0);
                    pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Instruction, pdfReader.GetPageSize(1).Width / 2, pdfReader.GetPageSize(1).Height / (float)(2 + 1), 0);
                    pdfContentByte.EndText();
                    pdfStamper.Close();
                    return outputPdfStream.ToArray();
                }
            }
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




        public static LineRadioMedicals MapperToRadioLine(Guid RadioId, LineRadioMedicalDto lineRadioMedical)
        {
            return new LineRadioMedicals
            {
                Id = Guid.NewGuid(),
                DateValidation = null,
                Description = lineRadioMedical.Description,
                IdRadio = RadioId,
                IdRadiology = null,
                Instruction=lineRadioMedical.Instruction,
                resultRadios=null,
                Status=StatusRadio.notValidate,
                
            };
        }

        public static LineAnalyseMedicals MapperToAnalyseLine(Guid AnalyseId,LineAnalyseMedicalDto lineAnalyse)
        {
            return new LineAnalyseMedicals
            {
                Id = Guid.NewGuid(),
                DateValidation = null,
                description = lineAnalyse.Description,
                Instruction = lineAnalyse.Instruction,
                IdSpecialisteAnalyse = null,
                ResultAnalyse = null,
                Status = StatusAnalyse.notValidate,
                IdAnalyse=AnalyseId
               
            };
        }

        public static Analyses MapperToAnalyse(AnalyseToAddDto analyseToAddDto, Guid IdOrdreMedical)
        {
            return new Analyses
            {
                Id = Guid.NewGuid(),
               
                IdOrdreMedical = IdOrdreMedical,
               
               
                
             
                FileAnalyse = analyseToAddDto.FileMedicalAnalyse
            };
        }


        public static Radio MapperToRadio(RadioToAddDto radioDto,Guid IdOrdreMedical)
        {
            return new Radio
            {
                Id = Guid.NewGuid(),
                IdOrdreMedical = IdOrdreMedical,
                FileRadio= radioDto.FileMedicalRadio
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
