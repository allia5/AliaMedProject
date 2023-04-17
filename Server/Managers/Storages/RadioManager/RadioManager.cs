﻿using Server.Data;
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
    }
}
