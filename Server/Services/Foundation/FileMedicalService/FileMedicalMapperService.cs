using DTO;
using Server.Models.ChronicDiseases;
using Server.Models.FileChronicDisease;
using Server.Models.fileMedical;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.FileMedicalService
{
    public static class FileMedicalMapperService
    {
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
