using DTO;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.Prescriptions;
using static Server.Utility.Utility;
namespace Server.Services.Foundation.PrescriptionService
{
    public static class PrescriptionMapperService
    {
        public static FileMedicalInformation MapperToFileInformationDto(fileMedicals fileMedicals, List<string> chronicDeasses)
        {
            return new FileMedicalInformation
            {
                Id = EncryptGuid(fileMedicals.Id),
                FirstName = fileMedicals.firstname,
                LastName = fileMedicals.Lastname,
                chronicDiseases = chronicDeasses,
                DateOfBirth = fileMedicals.DateOfBirth,
                Sexe = (Sexe)fileMedicals.Sexe
            };
        }
        public static  PrescriptionInfromationDto MapperToPrescriptionInformationDto(List<LinePrescriptionDto> linesPrescriptionDtos,MedicalOrdres medicalOrdres,Prescription prescription)
        {
            return new PrescriptionInfromationDto
            {
                DateRelease = medicalOrdres.ReleaseDate,
                IdPrescription = EncryptGuid(prescription.Id),
                Instruction = prescription.instruction,
                linePrescriptionDtos = linesPrescriptionDtos,


            };
        }
        public static InformationPrescriptionResultDto MapperToInformationPrescriptionResultDto(FileMedicalInformation fileMedicalInformation,PatientInformationDto patientInformationDto,DoctorInformationDto doctorInformationDto,PrescriptionInfromationDto prescriptionInfromationDto)
        {
            return new InformationPrescriptionResultDto
            {
                DoctorInformation = doctorInformationDto,
                FileMedicalInformation = fileMedicalInformation,
                PatientInformation = patientInformationDto,
                prescriptionInfromationDto = prescriptionInfromationDto

            };
        }
    }
   
}

