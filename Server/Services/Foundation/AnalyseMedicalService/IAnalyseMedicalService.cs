using DTO;

namespace Server.Services.Foundation.AnalyseMedicalService
{
    public interface IAnalyseMedicalService
    {
        public Task<InformationAnalyseResultDto> GetAllAnalyseResultByCode(string Email,string codeQr);
    }
}
