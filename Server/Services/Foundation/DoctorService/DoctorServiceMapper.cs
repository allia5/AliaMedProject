using DTO;
using Server.Models.Specialites;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.DoctorService
{
    public static class DoctorServiceMapper
    {
        public static DoctorInformationDto MapperToDoctorInformationDto(List<Specialite> specialities, User user)
        {
            List<string> names = specialities.Select(p => p.NameSpecialite).ToList();
            var result = new DoctorInformationDto
            {
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.Firstname,
                LastName = user.LastName,
                IdUser = user.Id,
                NumberPhone = user.PhoneNumber,
                Sexe = (Sexe)user.Sexe,
                Specialities = names

            };

            return result;
        }
    }
}
