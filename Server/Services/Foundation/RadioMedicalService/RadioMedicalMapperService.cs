using DTO;
using Server.Models.fileMedical;
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
        public static RadioInformation MapperToRadioInformation(Radio radio)
        {
            return new RadioInformation
            {
                Description = radio.Description,
                Id = EncryptGuid(radio.Id),
                Instruction = radio.Instruction
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
