using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Specialites;

namespace Server.Managers.Storages.SpecialitiesManager
{
    public class SpecialitiesManager : ISpecialitiesManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public SpecialitiesManager(ServerDbContext ServerDbContext)
        {
            this.ServerDbContext = ServerDbContext;
        }
        public async Task<List<Specialite>> SelectSpecialitiesByIdDoctor(Guid IdDoctor)
        {
            return await (from doctorItem in this.ServerDbContext.Doctors
                          where doctorItem.Id == IdDoctor
                          join DocSp in this.ServerDbContext.specialtiesDoctors on doctorItem.Id equals DocSp.IdDoctor
                          join Sp in this.ServerDbContext.specialites on DocSp.SpecialitesId equals Sp.Id
                          select Sp).ToListAsync();
        }
    }
}
