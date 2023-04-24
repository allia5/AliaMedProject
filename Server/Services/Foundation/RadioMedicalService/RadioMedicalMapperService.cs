using DTO;
using Server.Models.fileMedical;
using Server.Models.LineRadioMedical;
using Server.Models.RadioMedical;
using static Server.Utility.Utility;
namespace Server.Services.Foundation.RadioMedicalService
{
    public static class RadioMedicalMapperService
    {
        public static  InformationRadioResultDto MapperToInformationRadioResult(RadioInformation radioInformation,FileMedicalInformation fileMedicalInformation,PatientInformationDto patientInformationDto,DoctorInformationDto doctorInformation)
        {
            return new InformationRadioResultDto
            {
                DoctorInformation = doctorInformation,
                PatientInformation = patientInformationDto,
                FileMedicalInformation = fileMedicalInformation,
                RadioInformation= radioInformation
            };
        }
        public static RadioInformation MapperToRadioInformation(Radio radio, List<LineRadioMedicalResultDto>  ListLinesRadioResult)
        {
            return new RadioInformation
            {
               
                Id = EncryptGuid(radio.Id),
                linesRadioMedicals=ListLinesRadioResult
                
            };
        }
        public static LineRadioMedicalResultDto MapperTolineRadioMedicalResultDto(LineRadioMedicals lineRadioMedicals)
        {
            return new LineRadioMedicalResultDto
            {
Description = lineRadioMedicals.Description,
Instruction=lineRadioMedicals.Instruction,
Id=EncryptGuid(lineRadioMedicals.Id)
            };
        }
        public static FileMedicalInformation MapperToFileInformationDto(fileMedicals fileMedicals ,List<string> chronicDeasses) 
        {
            return new FileMedicalInformation
            {
                Id = EncryptGuid(fileMedicals.Id),
                FirstName = fileMedicals.firstname,
                LastName= fileMedicals.Lastname,
                chronicDiseases = chronicDeasses,
                DateOfBirth=fileMedicals.DateOfBirth,
                Sexe= (Sexe)fileMedicals.Sexe
            };
        }
    }
}
