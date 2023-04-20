using DTO;

namespace Client.Services.Foundations.FileMedicalService
{
    public interface IfileMedicalService
    {
        public Task<Stream> GetMedicalFilePrescription(string OrdreId);
        public Task<Stream> GetMedicalFileRadio(string OrdreId);
        public Task<Stream> GetMedicalFileAnalyse(string OrdreId);
        public Task<FileMedicalPatientDto> PostFileMedicalPatientAsync(FileMedicalToAddDto fileMedicalToAdd);
        public Task<FileMedicalMainPatientDto> GetAllFileMedicalMainPatient(string IdAppointment);
        public Task UpdateFileMedicalPatient(UpdateFileMedicalDto updateFileMedicalDto);
    }
}
