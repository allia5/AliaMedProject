using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.secretary;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using Server.Models.WorkDoctor;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.SecretaryService
{
    public static class SecretaryMapperService
    {
        public static SecretaryCabinetInformationDto MapperToSecretaryCabinetInformationDto(List<DoctorInformationAppointmentDto> ListdoctorInformation, CabinetInformationAppointmentDto cabinetInformation)
        {
            return new SecretaryCabinetInformationDto
            {
                CabinetInformation = cabinetInformation,
                ListDoctorInformation = ListdoctorInformation
            };
        }

        public static CabinetInformationAppointmentDto MapperToCabinetInformationAppointmentDto(CabinetMedical cabinetMedical)
        {
            return new CabinetInformationAppointmentDto
            {
                Id = EncryptGuid(cabinetMedical.Id),
                Adress = cabinetMedical.Adress,
                AdressMap = cabinetMedical.MapAdress,
                Image = cabinetMedical.image,
                Name = cabinetMedical.NameCabinet,
                NumberPhone = cabinetMedical.numberPhone,
                Services = cabinetMedical.Services


            };
        }
        public static DoctorInformationAppointmentDto mapperToDoctorInformationAppointmentDto(User user, List<string> Specialities, WorkDoctors workDoctors)
        {
            return new DoctorInformationAppointmentDto
            {
                FirstName = user.Firstname,
                LastName = user.LastName,
                Sexe = (Sexe)user.Sexe,
                Specialities = Specialities,
                TimeEnd = workDoctors.EndTime,
                TimeReady = workDoctors.ReadyTime,
                Id = EncryptGuid(Guid.Parse(user.Id)),
                NumberPatientAccepted = workDoctors.NbPatientAvailble
            };
        }
        public static Secretarys MapperToNewSecretary(UpdateStatusSecretaryDto updateStatusSecretaryDto, Secretarys secretarys)
        {
            secretarys.Status = (StatusSecretary)updateStatusSecretaryDto.StatusSecritary;
            return secretarys;
        }
        public static SecritaryDto MapperToSecretaryDto(User SecretaryUser, Secretarys secretarys)
        {
            return new SecritaryDto
            {
                IdSecritary = EncryptGuid(secretarys.Id),
                DateOfBirth = SecretaryUser.DateOfBirth,
                Email = SecretaryUser.Email,
                FirstName = SecretaryUser.Firstname,
                LastName = SecretaryUser.LastName,
                NumberPhone = SecretaryUser.PhoneNumber,
                sexe = (Sexe)SecretaryUser.Sexe,
                StatusSecritary = (StatusSecritary)secretarys.Status

            };
        }
        public static MailRequest MapperToMailRequest(User SecretaryUser, CabinetMedical cabinetMedical)
        {
            return new MailRequest
            {
                ToEmail = SecretaryUser.Email,
                Subject = "Notification",
                Body = " <h3> AliaMed.Com </h3> " +
                                           $"<a> You Are {SecretaryUser.Status.ToString()} to the role Secretary In Cabinet:{cabinetMedical.NameCabinet}</a>" + "<br/>"
            };
        }

        public static Secretarys MapperSecretary(Secretarys secretarys)
        {
            secretarys.Status = StatusSecretary.Active;
            return secretarys;
        }
        public static Secretarys MapperToSecritary(Guid CabinetId, string UserId)
        {
            return new Secretarys
            {
                Id = Guid.NewGuid(),
                DateCreate = DateTime.Now,
                IdUser = UserId,
                IdCabinetMedical = CabinetId,
                Status = StatusSecretary.Active


            };
        }
        public static UserRole MaperToUserRole(string idUser, Guid IdRole)
        {
            return new UserRole
            {
                Id = Guid.NewGuid(),
                IdUser = idUser,
                RoleId = IdRole

            };
        }
    }
}
