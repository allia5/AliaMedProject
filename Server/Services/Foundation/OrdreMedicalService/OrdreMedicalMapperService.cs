using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DTO;
using QRCoder;
using Server.Models.MedicalOrder;
using System.Drawing;
using static Server.Utility.Utility;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing.Imaging;
using System.IO;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using BarcodeLib;
using System.Net.Mime;
using Server.Models.Prescriptions;
using DocumentFormat.OpenXml.Office2010.Excel;
using Server.Models;
using Server.Models.PrescriptionLine;
using Server.Models.RadioMedical;
using Server.Models.Analyse;
using Server.Models.UserAccount;
using Server.Models.fileMedical;
using Server.Models.secretary;
using Server.Models.Doctor;
using DocumentFormat.OpenXml.Spreadsheet;

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



public static byte[] AjouterCodeQRDansFichierDocx(byte[] fichierDocx, string codeQR)
    {
            try
            {
                // Générer le code QR à partir de la chaîne de caractères
                BarcodeLib.Barcode qrCode = new BarcodeLib.Barcode();
                qrCode.IncludeLabel = true;
                qrCode.Encode(BarcodeLib.TYPE.Codabar, codeQR, System.Drawing.Color.Black, System.Drawing.Color.White, 300, 300);

                // Convert the QR code to a byte array
                byte[] codeQRBytes;
                using (MemoryStream ms = new MemoryStream(0))
                {
                    qrCode.SaveImage(ms, SaveTypes.PNG);
                    codeQRBytes = ms.ToArray();
                }

                // Open the Word document in a memory stream
                using (MemoryStream docStream = new MemoryStream(fichierDocx))
                using (WordprocessingDocument doc = WordprocessingDocument.Open(docStream, true))
                {
                    MainDocumentPart mainPart = doc.MainDocumentPart;

                    if (mainPart == null)
                    {
                        mainPart = doc.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }

                    // Add the QR code image to the document
                    ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        imagePart.FeedData(imageStream);
                    }

                    // Add a new paragraph to the document with the QR code image
                    var paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                    var run = new DocumentFormat.OpenXml.Wordprocessing.Run();
                    var drawing = new DocumentFormat.OpenXml.Drawing.Paragraph(
                        new DocumentFormat.OpenXml.Drawing.Wordprocessing.Anchor(
                            new DocumentFormat.OpenXml.Drawing.Wordprocessing.SimplePosition() { X = 0, Y = 0 },
                            new DocumentFormat.OpenXml.Drawing.Wordprocessing.Extent() { Cx = 3000000L, Cy = 3000000L },
                            new DocumentFormat.OpenXml.Drawing.Wordprocessing.DocProperties() { Id = 1, Name = "CodeQR" },
                            new DocumentFormat.OpenXml.Drawing.Wordprocessing.NonVisualGraphicFrameDrawingProperties(
                                new DocumentFormat.OpenXml.Drawing.GraphicData(
                                    new DocumentFormat.OpenXml.Drawing.Pictures.Picture(
                                        new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureProperties(
                                            new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties() { Id = 0, Name = "CodeQR.png" },
                                            new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureDrawingProperties()),
                                        new DocumentFormat.OpenXml.Drawing.Pictures.BlipFill(
                                            new DocumentFormat.OpenXml.Drawing.Blip() { Embed = mainPart.GetIdOfPart(imagePart) },
                                            new DocumentFormat.OpenXml.Drawing.Stretch(new DocumentFormat.OpenXml.Drawing.FillRectangle())),
                                        new DocumentFormat.OpenXml.Drawing.Pictures.ShapeProperties())))));
                    run.AppendChild(drawing);
                    paragraph.AppendChild(run);
                    mainPart.Document.Body.AppendChild(paragraph);

                    // Save the document to the same memory stream
                    mainPart.Document.Save();
                }

                // Return the modified document as a byte array
                return fichierDocx;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
      
    }




    public static byte[] AjouterStringDansFichierDocx(byte[] fichierDocx, string chaine)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(fichierDocx, 0, fichierDocx.Length);

                    using (WordprocessingDocument doc = WordprocessingDocument.Open(ms, true))
                    {
                        MainDocumentPart mainPart = doc.MainDocumentPart;

                        if (mainPart == null)
                        {
                            mainPart = doc.AddMainDocumentPart();
                            new Document(new Body()).Save(mainPart);
                        }

                        // mainPart.Document.Body.Append(new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(MediaTypeNames.Text(chaine))));
                        DocumentFormat.OpenXml.Drawing.Paragraph paragraph = new DocumentFormat.OpenXml.Drawing.Paragraph(
        new DocumentFormat.OpenXml.Drawing.Run(
            new DocumentFormat.OpenXml.Drawing.Text(chaine)
        )
    );
                        mainPart.Document.Body.Append(paragraph);

                        mainPart.Document.Save();
                    }

                    return ms.ToArray();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
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
