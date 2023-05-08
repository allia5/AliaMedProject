using iTextSharp.text.pdf;
using Server.Models.Pharmacys;

namespace Server.Managers.Storages.PharmacyManager
{
    public interface IPharmacyManager
    {
        public Task<Pharmacy> SelectPharmacyById(Guid PharmacyId);
      
    }
}
