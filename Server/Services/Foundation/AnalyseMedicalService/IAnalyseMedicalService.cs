using DTO;

namespace Server.Services.Foundation.AnalyseMedicalService
{
    public interface IAnalyseMedicalService
    {
        public Task<InformationAnalyseResultDto> GetAllAnalyseResultByCode(string Email,string codeQr);
        public Task<byte[]> SecritaryGetFileAnalyseByIdOrdreMedical(string Email,string OrdreMedicalId,string CabinetId);
        public Task<byte[]> PatientGetFileAnalyseByIdOrdreMedical(string Email, string OrdreMedicalId);
    }
}
