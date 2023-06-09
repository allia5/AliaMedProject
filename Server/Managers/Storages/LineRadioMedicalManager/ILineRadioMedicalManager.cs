﻿using Server.Models.LineRadioMedical;

namespace Server.Managers.Storages.LineRadioMedicalManager
{
    public interface ILineRadioMedicalManager
    {
        public Task<LineRadioMedicals> InsertLineRadioMedical(LineRadioMedicals lineRadioMedicals);
        public Task<List<LineRadioMedicals>> SelectAllLineMedicalByIdRadio(Guid RadioId);
        public Task<LineRadioMedicals> SelectLineRadioById(Guid LineRadioId);
        public Task<LineRadioMedicals> UpdateLineRadioMedical(LineRadioMedicals lineRadioMedicals);
    }
}
