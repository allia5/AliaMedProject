using Server.Models.Doctor;
using Server.Models.UserAccount;

namespace Server.Managers.Storages.DoctorManager
{
    public interface IDoctorManager
    {
        public Task<List<User>> SelectDoctor();
        public Task<Doctors> SelectDoctorByIdUser(string IdUser);
        public Task<Doctors> SelectDoctorById(Guid IdDoctor);
    }
}
