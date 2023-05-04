using DTO;

namespace Server.Services.Foundation.AdviceMedicalService
{
    public interface IAdviceMedicalService
    {
        public Task<List<AdviceMedicalDto>> PatientGetAdviceMedical(string Email, string OrdreMedicalId);
        public Task<List<AdviceMedicalDto>> DoctorGetAdviceMedical(string Email, string OrdreMedicalId);
        public Task PostNewAdviceMedicalPatient(string Email, MedicalAdviceToAddDto medicalAdviceToAddDto);
        public Task PostNewAdviceMedicalDoctor(string Email, MedicalAdviceToAddDto medicalAdviceToAddDto);

    }
}
