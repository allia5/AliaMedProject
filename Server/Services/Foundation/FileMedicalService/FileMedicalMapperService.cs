using DTO;
using Server.Models.ChronicDiseases;
using Server.Models.FileChronicDisease;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.FileMedicalService
{
    public static class FileMedicalMapperService
    {
        public static FileMedicalMainPatientDto MapperToFileMedicalMainPatientDto(List<FileMedicalPatientDto> fileMedicalPatientDtos,PatientInformationDto PatientInformationAccount)
        {
            return new FileMedicalMainPatientDto
            {
                fileMedicals = fileMedicalPatientDtos,
                MainUser = PatientInformationAccount
            };
        }
        public static FileMedicalPatientDto MapperTofileMedicalPatientDtos(int CountmedicalOrdres ,List<chronicDiseasesDto> chronicDiseases,User UserAccountDoctor , fileMedicals fileMedical)
        {
            return new FileMedicalPatientDto
            {
                chronicDiseases = chronicDiseases,
                CountOrderMedical=CountmedicalOrdres,
                DateOfBirth=fileMedical.DateOfBirth,
                DateOfCreate=fileMedical.DateOfCreate,
                FirstName=fileMedical.firstname,
                LastName=fileMedical.Lastname,
                Sexe= (Sexe)fileMedical.Sexe,
                Id=EncryptGuid( fileMedical.Id)

            }
        }
        public static FileMedicalPatientDto MapperToFileMedicalPatient(User UserAccountDoctor,fileMedicals fileMedical,FileMedicalToAddDto fileMedicalToAdd)
        {
            return new FileMedicalPatientDto
            {
                Id = EncryptGuid(fileMedical.Id),
                chronicDiseases = fileMedicalToAdd.chronicDiseases,
                CountOrderMedical = 0,
                DateOfBirth = fileMedical.DateOfBirth,
                DateOfCreate = fileMedical.DateOfCreate,
                FirstName = fileMedical.firstname,
                LastName = fileMedical.Lastname,
                Sexe = (Sexe)fileMedical.Sexe

            };
        }
        public static fileMedicals MapperToFileMedical(FileMedicalToAddDto fileMedicalToAdd , MedicalPlanning medicalPlanning)
        {
            return new fileMedicals
            {
                MedicalIdentification = GenerateID(fileMedicalToAdd.LastName, fileMedicalToAdd.FirstName, fileMedicalToAdd.DateOfBirth),
DateOfBirth = fileMedicalToAdd.DateOfBirth,
DateOfCreate =DateTime.Now,
firstname=fileMedicalToAdd.FirstName,
Lastname=fileMedicalToAdd.LastName,
Id=Guid.NewGuid(),
Sexe= (Models.UserAccount.EnumSexe)fileMedicalToAdd.Sexe,
IdDoctor=medicalPlanning.IdDoctor,
IdUser=medicalPlanning.IdUser,



            };
        }
    }
}
