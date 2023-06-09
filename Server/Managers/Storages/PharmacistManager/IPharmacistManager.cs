﻿using Server.Models.Pharmacist;

namespace Server.Managers.Storages.PharmacistManager
{
    public interface IPharmacistManager
    {
        public Task<Pharmacists> SelectPharmacistByIdAsync(Guid PharmacistId);
        public Task<Pharmacists> SelectPharmacistByIdUser(string UserId);
    }
}
