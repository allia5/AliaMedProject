using DTO;
using Server.Models.ChronicDiseases;
using Server.Models.FileChronicDisease;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;
using static Server.Utility.Utility;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;
using Server.Models.Specialites;

namespace Server.Services.Foundation.FileMedicalService
{
    public static class FileMedicalMapperService
    {
        public static MailRequest MapperToMailRequestUpdateFileMedical(fileMedicals FileMedical, User UserAccountDoctor,User NewUserAccount,User OldUserAccount)
        {
            return new MailRequest
            {
                ToEmail = OldUserAccount.Email,
                Subject = "Notification",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n       <h3> Dawi.dz </h3>\r\n    </div>\r\n    <div class=card-body>\r\n      <h5 class=card-title> File Medical notification  </h5>\r\n        <p class=card-text>Your File Medical {FileMedical.Lastname},{FileMedical.firstname} Is Moved To User Account {NewUserAccount.LastName},{NewUserAccount.Firstname}   {DateTime.Now}  <br/> by Doctor :{UserAccountDoctor.Firstname} ,{UserAccountDoctor.LastName}</p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>"

            };
        }
        public static FileMedicalMainPatientDto MapperToFileMedicalMainPatientDto(List<FileMedicalPatientDto> fileMedicalPatientDtos,PatientInformationDto PatientInformationAccount)
        {
            return new FileMedicalMainPatientDto
            {
                fileMedicals = fileMedicalPatientDtos,
                MainUser = PatientInformationAccount
            };
        }

        public static fileMedicals MapperToNewFileMedical(Guid DoctorId,fileMedicals fileMedicals,string UserId)
        {
            fileMedicals.IdDoctor = DoctorId;
            fileMedicals.IdUser = UserId; return fileMedicals;
        }
        public static FileMedicalPatientDto MapperTofileMedicalPatientDtos(int CountmedicalOrdres ,List<chronicDiseasesDto> chronicDiseases,User UserAccountDoctor , fileMedicals fileMedical,List<Specialite> specialitiesDoctor)
        {
            return new FileMedicalPatientDto
            {
                chronicDiseases = chronicDiseases,
                CountOrderMedical = CountmedicalOrdres,
                DateOfBirth = fileMedical.DateOfBirth,
                DateOfCreate = fileMedical.DateOfCreate,
                FirstName = fileMedical.firstname,
                LastName = fileMedical.Lastname,
                Sexe = (Sexe)fileMedical.Sexe,
                Id = EncryptGuid(fileMedical.Id),
                Doctor = MapperToDoctorInformationDto(specialitiesDoctor, UserAccountDoctor)


            };
        }
        public static FileMedicalPatientDto MapperToFileMedicalPatient(User UserAccountDoctor,fileMedicals fileMedical,FileMedicalToAddDto fileMedicalToAdd,List<Specialite> specialites)
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
                Sexe = (Sexe)fileMedical.Sexe,
                Doctor = MapperToDoctorInformationDto(specialites, UserAccountDoctor)

            };
        }
        public static fileMedicals MapperToFileMedicalUpdated(fileMedicals fileMedicals , UpdateFileMedicalDto updateFileMedical)
        {
            fileMedicals.DateOfBirth = updateFileMedical.DateOfBirth;
            fileMedicals.Lastname = updateFileMedical.LastName;
            fileMedicals.Sexe = (EnumSexe)updateFileMedical.Sexe;
            return fileMedicals;

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
