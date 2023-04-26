using DTO;
using Server.Models.Analyse;
using Server.Models.fileMedical;
using Server.Models.LineRadioMedical;
using Server.Models.MedicalOrder;
using Server.Models.RadioMedical;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.AnalyseMedicalService
{
    public  static class AnalyseMedicalMapperService
    {
        public static InformationAnalyseResultDto MapperToInformationAnalyseResult(InformationAnalyseDto AnalyseInformation, FileMedicalInformation fileMedicalInformation, PatientInformationDto patientInformationDto, DoctorInformationDto doctorInformation)
        {
            return new InformationAnalyseResultDto
            {
                DoctorInformation = doctorInformation,
                PatientInformation = patientInformationDto,
                FileMedicalInformation = fileMedicalInformation,
                informationAnalyseDto=AnalyseInformation
                
            };
        }
       
        public static InformationAnalyseDto MapperToAnalyseInformation(Analyses analyse, List<LinesAnalyseDto> ListLinesAnalyseResult, MedicalOrdres medicalOrdres )
        {
            return new InformationAnalyseDto
            {

                IdAnalyse = EncryptGuid(analyse.Id),
                LinesAnalyse = ListLinesAnalyseResult,
                DateRelease=medicalOrdres.ReleaseDate

            };
        }
     
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
    }
}
