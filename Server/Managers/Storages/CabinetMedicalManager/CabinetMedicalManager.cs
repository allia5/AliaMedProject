using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Admin;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;

namespace Server.Managers.Storages.CabinetMedicalManager
{
    public class CabinetMedicalManager : ICabinetMedicalManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public CabinetMedicalManager(ServerDbContext ServerDbContext)
        {
            this.ServerDbContext = ServerDbContext;
        }
        public async Task<CabinetMedical> SelectCabinetMedicalByUserId(string UserId)
        {
            return await (from user in ServerDbContext.users
                          where user.Id == UserId
                          join Doctor in ServerDbContext.Doctors on user.Id equals Doctor.UserId
                          join admin in ServerDbContext.admins on Doctor.Id equals admin.IdDoctor
                          join Cabinet in ServerDbContext.cabinetMedicals on admin.IdCabinet equals Cabinet.Id
                          select Cabinet).FirstOrDefaultAsync();
        }

        public async Task<CabinetMedical> UpdateCabinetMedical(CabinetMedical cabinetMedical)
        {
            try
            {
                var result = this.ServerDbContext.cabinetMedicals.Update(cabinetMedical);
                await ServerDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<CabinetMedical> SelectCabinetMedicalById(Guid CabinetId)
        {
            return await this.ServerDbContext.cabinetMedicals.FirstAsync(e => e.Id == CabinetId);
        }

        public async Task<CabinetMedical> SelectCabinetMedicalOpenById(Guid CabinetId)
        {
            return await (from cabinet in this.ServerDbContext.cabinetMedicals where cabinet.statusService == StatusService.Enline && cabinet.Id == CabinetId select cabinet).FirstOrDefaultAsync();
        }
    }
}
