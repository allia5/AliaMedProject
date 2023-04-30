using DTO;

namespace Client.Services.Foundations.LineRadioResultService
{
    public interface ILineRadioResultService
    {
        public Task<FileResultDto> GetFileResultRadio(string InAppointment, string IdLineRadio);
        public Task<FileResultDto> GetFileResultRadioPatient(string IdLineRadio);
    }
}
