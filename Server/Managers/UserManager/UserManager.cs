using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.UserAccount;

namespace Server.Managers.UserManager
{
    public class UserManager : IUserManager
    {
        protected ServerDbContext ServerDbContext { get; set; }
        public UserManager(ServerDbContext ServerDbContext)
        {
            this.ServerDbContext = ServerDbContext;
        }
        public async Task<User> SelectUserByIdDoctor(Guid IdDoctor)
        {

            return await (from doctor in this.ServerDbContext.Doctors
                          where doctor.Id == IdDoctor && doctor.StatusDoctor == Models.Doctor.StatusDoctor.Activated
                          join user in this.ServerDbContext.users on doctor.UserId equals user.Id
                          select user).FirstOrDefaultAsync();


        }

        public async Task<User> SelectUserByIdCabinet(Guid IdCabinet)
        {
            return await (from cabinet in this.ServerDbContext.cabinetMedicals
                          where cabinet.Id == IdCabinet
                          join Admin in this.ServerDbContext.admins on cabinet.Id equals Admin.IdCabinet
                          join doctor in this.ServerDbContext.Doctors on Admin.IdDoctor equals doctor.Id
                          join user in this.ServerDbContext.users on doctor.UserId equals user.Id
                          select user).FirstAsync();
        }

        public async Task<List<User>> SelectUsersAdminByIdCabinet(Guid CabinetId)
        {
            return await (from cabinet in this.ServerDbContext.cabinetMedicals
                          where cabinet.Id == CabinetId
                          join admin in this.ServerDbContext.admins on cabinet.Id equals admin.IdCabinet
                          join doctor in this.ServerDbContext.Doctors on admin.IdDoctor equals doctor.Id
                          join user in this.ServerDbContext.users on doctor.UserId equals user.Id
                          select user).ToListAsync();
        }

        public async Task<List<User>> SelectAllUsersDoctor()
        {
            return await (from user in this.ServerDbContext.users
                          where user.Status == UserStatus.Activated
                          join doctor in this.ServerDbContext.Doctors on user.Id equals doctor.UserId
                          where doctor.StatusDoctor == Models.Doctor.StatusDoctor.Activated
                          select user).ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = this.ServerDbContext.users.Update(user);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
