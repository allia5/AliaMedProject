using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.SpecialisteAnalyses;

namespace Server.Managers.Storages.SpecialisteAnalyseManager
{
    public class SpecialisteAnalyseManager : ISpecialisteAnalyseManager
    {
        public ServerDbContext ServerDbContext { get; set; }    
        public SpecialisteAnalyseManager(ServerDbContext ServerDbContext)
        {
            this.ServerDbContext = ServerDbContext;
        }
        public async Task<SpecialisteAnalyse> SelectSpecialisteAnalyseByIdUser(string UserId)
        {
            return await (from specialisteAnalyse in this.ServerDbContext.SpecialisteAnalyse
                          where specialisteAnalyse.IdUser == UserId 
                          select specialisteAnalyse).FirstOrDefaultAsync();
        }
    }
}
