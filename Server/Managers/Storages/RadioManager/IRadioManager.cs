﻿using Server.Models.RadioMedical;

namespace Server.Managers.Storages.RadioManager
{
    public interface IRadioManager
    {
        public Task<Radio> InsertRadioAsync(Radio radio);
        public Task<Radio> UpdateRadioAsync(Radio radio);
        public Task<Radio> SelectRadioByIdMedicalOrdre(Guid MedicalOrdre);
        public Task<Radio> SelectRadioByCodeAsync(string Code);
        public Task<Radio> SelectRadioByIdAsync(Guid RadioId);
    }
}
