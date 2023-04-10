using Server.Models.Specialites;

namespace Server.Managers.Storages.SpecialitiesManager
{
    public interface ISpecialitiesManager
    {
        public Task<List<Specialite>> SelectSpecialitiesByIdDoctor(Guid IdDoctor);
    }
}
