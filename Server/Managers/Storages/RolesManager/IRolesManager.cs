using Server.Models.UserAccount;

namespace Server.Managers.Storages.RolesManager
{
    public interface IRolesManager
    {
        public Task<Role> GetRolesById(Guid idRole);
    }
}
