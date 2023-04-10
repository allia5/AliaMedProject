using Server.Data;
using Server.Models.UserRoles;

namespace Server.Managers.Storages.UserRoleManager
{
    public class UserRoleManager : IUserRoleManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public UserRoleManager(ServerDbContext ServerDbContext)
        {
            this.ServerDbContext = ServerDbContext;
        }
        public async Task<UserRole> InsertUserRoleAsync(UserRole UserRole)
        {
            var result = await this.ServerDbContext.userRoles.AddAsync(UserRole);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IQueryable<UserRole>> GetUserRolesById(string IdUser)
        {
            return this.ServerDbContext.userRoles.Where(w => w.IdUser == IdUser);

        }
    }
}
