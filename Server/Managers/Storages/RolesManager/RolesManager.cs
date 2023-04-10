using Server.Data;
using Server.Models.UserAccount;

namespace Server.Managers.Storages.RolesManager
{
    public class RolesManager : IRolesManager
    {
        private ServerDbContext ServerDbContext { get; set; }
        public RolesManager(ServerDbContext serverDbContext)
        {
            this.ServerDbContext = serverDbContext;
        }
        public async Task<Role> GetRolesById(Guid idRole)
        {
            return await this.ServerDbContext.roles.FindAsync(idRole);
        }
    }
}
