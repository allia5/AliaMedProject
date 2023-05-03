using DTO;

namespace Server.Services.Foundation.AdviceMedicalService
{
    public interface IAdviceMedicalService
    {
        public Task<List<AdviceMedicalDto>> GetAdviceMedical(string Email, string OrdreMedicalId);
        public Task PostNewAdviceMedicalPatient(string Email, MedicalAdviceToAddDto medicalAdviceToAddDto);
    }
}
