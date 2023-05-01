using Server.Models.AdviceMedicals;

namespace Server.Managers.Storages.AdviceManager
{
    public interface IAdviceManager
    {
        public Task<List<AdviceMedical>> adviceMedicalsByIdOrdreMedicalAsync(Guid OrdreMedicalId);
    }
}
