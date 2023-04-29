using DTO;

namespace Server.Services.Foundation.ResultAnalyseService
{
    public interface IResultAnalyseService
    {
        public Task PostNewAnalyseResult(string Email, AnalyseResultToAdd analyseResultToAdd);
        public Task<FileResultDto> GetFileResultAnalyse(string Email, string AppointmentId, string LineAnalyseId);
    }
}
