using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.MedicalOrder;

namespace Server.Managers.Storages.OrdreMedicalManager
{
    public class OrdreMedicalManager : IOrdreMedicalManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public OrdreMedicalManager(ServerDbContext serverDbContext)
        {
            ServerDbContext = serverDbContext;
        }
        public async Task<List<MedicalOrdres>> SelectListOrdreMedicalByIdMedicalFile(Guid FileId)
        {
            return await (from ordre in this.ServerDbContext.MedicalOrdres where ordre.IdFileMedical == FileId select ordre ).ToListAsync();
                 
        }

        public async Task<MedicalOrdres> InsertOrdreMedicalAsync(MedicalOrdres MedicalOrdres)
        {
           
                var result = this.ServerDbContext.MedicalOrdres.Add(MedicalOrdres);
                await this.ServerDbContext.SaveChangesAsync();
                return result.Entity;
           
        
        }

        public async Task<List<MedicalOrdres>> SelectAllMedicalOrderByIdCabinetByIdDoctorByDate(Guid CabinetId, Guid DoctorId, DateTime Date)
        {
            return await (from Order in this.ServerDbContext.MedicalOrdres where Order.IdCabinetMedical == CabinetId && Order.IdDoctor == DoctorId && Order.ReleaseDate.Date == Date.Date select Order).ToListAsync();
        }

        public async Task<MedicalOrdres> SelectMedicalOrdreByIdByIdDoctorByIdCabinet(Guid OrdreMedicalId, Guid DoctorId, Guid CabinetId)
        {
            return await (from ItemOrdreMedical in this.ServerDbContext.MedicalOrdres
                          where ItemOrdreMedical.IdCabinetMedical == CabinetId
                          && ItemOrdreMedical.IdDoctor == DoctorId
                          && ItemOrdreMedical.Id == OrdreMedicalId
                          select ItemOrdreMedical).FirstOrDefaultAsync();
        }

        public async Task<MedicalOrdres> UpdateMedicalOrdreAsync(MedicalOrdres MedicalOrdres)
        {
            var result =  this.ServerDbContext.Update(MedicalOrdres);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<MedicalOrdres> SelectMedicalOrdreByIdAsync(Guid OrdreMedicalId)
        {
            return await (from OrdreItem in this.ServerDbContext.MedicalOrdres where OrdreItem.Id == OrdreMedicalId select OrdreItem).FirstOrDefaultAsync();
        }
    }
}
