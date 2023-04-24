using Server.Models.Radiologys;

namespace Server.Managers.Storages.RadiologyManager
{
    public interface IRadiologyManager
    {
        public Task<Radiology> SelectRadiologyByIdDoctor(Guid DoctorId);
    }
}
