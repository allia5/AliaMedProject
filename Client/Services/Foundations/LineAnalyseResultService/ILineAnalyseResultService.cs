using DTO;

namespace Client.Services.Foundations.LineAnalyseResultService
{
    public interface ILineAnalyseResultService
    {
        public Task<FileResultDto> GetFileResultAnalyse(string InAppointment, string IdLineAnalyse);
       
    }
}
