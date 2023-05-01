using DTO;

namespace Client.Services.Foundations.OrdreMedicalService
{
    public interface IOrdreMedicalService
    {
        public Task PostOrdreMedicalPatient(OrderMedicalToAddDro orderMedicalToAddDro);
        public Task<List<InformationOrderMedicalSecritary>> GetAllOrdreMedicalSecritary(KeysAppoimentInformationSecretary keysAppoimentInformationSecretary);
        public Task UpdateStatusOrdreMedicalBySecritary(UpdateOrdreMedicalDto updateOrdreMedicalDto);
        public Task<MedicalFileArchiveDto> GetMedicalFileArchive(string FileId, string AppointmentId);
        public Task<MedicalFileArchivePatientDto> GetMedicalFileArchivePatient(string FileId);



    }
}
