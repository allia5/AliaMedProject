using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using Server.Models.WorkDoctor;
using System.Runtime.CompilerServices;
using static Server.Utility.Utility;

namespace Server.Services.UserService
{
    public static class UserMapperService
    {
        public static DoctorSearchDto MapperToDoctorSearchDto(User user, List<CabinetSearchDto> cabinetSearchDtos, List<string> Specialities)
        {
            return new DoctorSearchDto
            {
                Id = EncryptGuid(Guid.Parse(user.Id)),
                FirstName = user.Firstname,
                LastName = user.LastName,
                Sexe = (Sexe)user.Sexe,
                Specialities = Specialities,
                cabinetSearchDtos = cabinetSearchDtos


            };
        }

        public static JobSearchDto MapperToJobSearchDto(WorkDoctors workDoctors)
        {
            return new JobSearchDto
            {
                EndTime = workDoctors.EndTime,
                ReadyTime = workDoctors.ReadyTime,
                NumberPatientAvailble = workDoctors.NbPatientAvailble,
                Id = EncryptGuid(workDoctors.Id)
            };
        }
        public static CabinetSearchDto MapperToCabinetSearch(JobSearchDto jobDto, CabinetMedical cabinetMedical)
        {
            return new CabinetSearchDto
            {
                Id = EncryptGuid(cabinetMedical.Id),
                Adress = cabinetMedical.Adress,
                Image = cabinetMedical.image,
                Name = cabinetMedical.NameCabinet,
                Services = cabinetMedical.Services,
                JobSearchDto = jobDto

            };
        }

        public static User MaperToUser(this RegistreAccountDto registreAccountDto)
        {
            return new User
            {
                Email = registreAccountDto.Email,
                UserName = registreAccountDto.Email,
                LastName = registreAccountDto.LastName,
                Firstname = registreAccountDto.FirstName,
                Sexe = (EnumSexe)(int)registreAccountDto.Sexe,
                NationalNumber = registreAccountDto.NationalNumber,
                PhoneNumber = registreAccountDto.PhoneNumber,
                DateOfBirth = registreAccountDto.DateOfBirth,
                Status = UserStatus.Deactivated



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
        public static List<object> MapperToList(IQueryable obj)
        {
            return new List<object>((IEnumerable<object>)obj);
        }
        public static User AppendRfToken(User user, string RefreshToken, DateTime DateExpired)
        {
            user.RefreshToken = RefreshToken;
            user.DateExpireRefreshToken = DateExpired;
            return user;
        }
        public static JwtDto MapperToJwtResult(string Token, string RefreshToken)
        {
            return new JwtDto
            {
                Token = Token,
                RefreshToken = RefreshToken
            };
        }
    }
}
