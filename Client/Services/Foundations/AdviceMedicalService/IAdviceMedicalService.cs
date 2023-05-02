using DTO;

namespace Client.Services.Foundations.AdviceMedicalService
{
    public interface IAdviceMedicalService
    {
        public Task<List<AdviceMedicalDto>> GetAdvicesMedical(string OrdreMedicalId);
    }
}
