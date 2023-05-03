using DTO;

namespace Client.Services.Foundations.FileMedicalService
{
    public interface IfileMedicalService
    {
      
        public Task<FileMedicalPatientDto> PostFileMedicalPatientAsync(FileMedicalToAddDto fileMedicalToAdd);
        public Task<FileMedicalMainPatientDto> GetAllFileMedicalMainPatient(string IdAppointment);
        public Task UpdateFileMedicalPatient(UpdateFileMedicalDto updateFileMedicalDto);
        public Task<List<FileMedicalPatientDto>> GetFilePatient();
        public Task TransferFileMedical(FileTransferDto fileTransferDto);
    }
}
