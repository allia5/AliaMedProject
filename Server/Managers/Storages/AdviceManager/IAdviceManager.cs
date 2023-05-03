using Server.Models.AdviceMedicals;

namespace Server.Managers.Storages.AdviceManager
{
    public interface IAdviceManager
    {
        public Task<List<AdviceMedical>> adviceMedicalsByIdOrdreMedicalAsync(Guid OrdreMedicalId);
       // public Task<List<AdviceMedical>> SelectAdviceMedicalByIdOrdreMedicalIdUser(Guid OrdreMedicalId,string UserId);
        public Task<AdviceMedical> UpdateAdviceMedical(AdviceMedical adviceMedical);
        public Task<AdviceMedical> InsertAdviceMedical(AdviceMedical adviceMedical);
    }

}
