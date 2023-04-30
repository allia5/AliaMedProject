using DTO;

namespace Server.Services.Foundation.ResultRadioService
{
    public interface  IResultRadioService
    {
        public Task AddRadioResultService(string Email, RadioResultToAddDto RadioResultToAddDto);
        public Task<FileResultDto> GetFileResultRadio(string Email, string AppointmentId, string LineRadioId);
        public Task<FileResultDto> GetFileResultRadioPatient(string Email, string LineRadioId);
    }
}
