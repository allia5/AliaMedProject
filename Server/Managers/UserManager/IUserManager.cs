using Server.Models.UserAccount;

namespace Server.Managers.UserManager
{
    public interface IUserManager
    {
        public Task<User> SelectUserByIdDoctor(Guid IdUser);
        public Task<User> SelectUserByIdCabinet(Guid IdCabinet);
        public Task<List<User>> SelectUsersAdminByIdCabinet(Guid CabinetId);
        public Task<List<User>> SelectAllUsersDoctor();
        public Task<User> UpdateUser(User user);
    }
}
