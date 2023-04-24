using Server.Data;
using Server.Models.Radiologys;

namespace Server.Managers.Storages.RadiologyManager
{
    public class RadiologyManager :IRadiologyManager
    {
        public ServerDbContext serverDbContext { get; set; }
        public RadiologyManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }

        public async Task<Radiology> SelectRadiologyByIdDoctor(Guid DoctorId)
        {
            return await (from RadiologyItem in this.serverDbContext.radiologies where RadiologyItem.IdDoctor ==)
        }
    }
}
