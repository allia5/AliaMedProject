using Server.Models.UserRoles;

namespace Server.Managers.Storages.UserRoleManager
{
    public interface IUserRoleManager
    {
        public Task<UserRole> InsertUserRoleAsync(UserRole UserRole);
        public Task<IQueryable<UserRole>> GetUserRolesById(string IdUser);
    }
}
