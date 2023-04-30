using DTO;

namespace Server.Services.Foundation.FileMedicalService
{
    public interface IFileMedicalService
    {
        public Task<FileMedicalPatientDto> AddNewFileMedicalPatient(string Email, FileMedicalToAddDto fileMedicalToAdd);
        public Task<FileMedicalMainPatientDto> GetAllFileMedicalMainPatient(string Email,string IdAppointment);
        public Task UpdateFileMedicalService(string Email, UpdateFileMedicalDto fileMedicalService);
        public Task<byte[]> GetFilePrescriptionByIdOrdreMedical(string OrdreMedicalId);
        public Task<byte[]> GetFileRadioByIdOrdreMedical(string OrdreMedicalId);
        public Task<byte[]> GetFileAnalyseByIdOrdreMedical(string OrdreMedicalId);
        public Task<List<FileMedicalPatientDto>> GetFilesMedicalPatient(string Email);
    }
}
