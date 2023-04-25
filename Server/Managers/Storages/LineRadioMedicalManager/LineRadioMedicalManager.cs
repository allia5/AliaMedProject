using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.LineRadioMedical;

namespace Server.Managers.Storages.LineRadioMedicalManager
{
    public class LineRadioMedicalManager : ILineRadioMedicalManager
    {
        public ServerDbContext serverDbContext { get; set; }
        public LineRadioMedicalManager(ServerDbContext serverDbContext)
        {
            this.serverDbContext = serverDbContext;
        }
        public async Task<LineRadioMedicals> InsertLineRadioMedical(LineRadioMedicals lineRadioMedicals)
        {
            var result = this.serverDbContext.LineRadioMedicals.Add(lineRadioMedicals);
            await this.serverDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<LineRadioMedicals>> SelectAllLineMedicalByIdRadio(Guid RadioId)
        {
            return await (from itemLineRadio in this.serverDbContext.LineRadioMedicals where itemLineRadio.IdRadio == RadioId && itemLineRadio.Status == Models.RadioMedical.StatusRadio.notValidate select itemLineRadio).ToListAsync();
        }

        public async Task<LineRadioMedicals> SelectLineRadioById(Guid LineRadioId)
        {
            return await (from LineRadio in this.serverDbContext.LineRadioMedicals 
                          where LineRadio.Id == LineRadioId && LineRadio.Status == Models.RadioMedical.StatusRadio.notValidate
                          select LineRadio ).FirstOrDefaultAsync();
        }

        public async Task<LineRadioMedicals> UpdateLineRadioMedical(LineRadioMedicals lineRadioMedicals)
        {
           var result =  this.serverDbContext.LineRadioMedicals.Update(lineRadioMedicals);
            await this.serverDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
