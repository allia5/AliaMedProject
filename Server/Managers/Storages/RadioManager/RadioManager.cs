using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.RadioMedical;

namespace Server.Managers.Storages.RadioManager
{
    public class RadioManager : IRadioManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public RadioManager( ServerDbContext serverDbContext) { 
            ServerDbContext = serverDbContext;
        }


        public async Task<Radio> InsertRadioAsync(Radio radio)
        {
            var result = this.ServerDbContext.Radio.Add(radio);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Radio> UpdateRadioAsync(Radio radio)
        {
            var result = this.ServerDbContext.Radio.Update(radio);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Radio> SelectRadioByIdMedicalOrdre(Guid MedicalOrdre)
        {
            return await (from ItemRadio in this.ServerDbContext.Radio where ItemRadio.IdOrdreMedical == MedicalOrdre select ItemRadio).FirstOrDefaultAsync();
        }
    }
}
