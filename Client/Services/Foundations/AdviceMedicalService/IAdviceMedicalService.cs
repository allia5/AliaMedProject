using DTO;

namespace Client.Services.Foundations.AdviceMedicalService
{
    public interface IAdviceMedicalService
    {
        public Task<List<AdviceMedicalDto>> GetAdvicesMedicalPatient(string OrdreMedicalId);
        public Task<List<AdviceMedicalDto>> GetAdvicesMedicalDoctor(string OrdreMedicalId);
        public Task PatientPostNewAdviceMedicalPatient(MedicalAdviceToAddDto medicalAdviceToAddDto);
        public Task DoctorPostNewAdviceMedicalPatient(MedicalAdviceToAddDto medicalAdviceToAddDto);
    }
}
