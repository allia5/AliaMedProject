using DTO;

namespace Client.Services.Foundations.SpecialisteAnalyseService
{
    public interface ISpecialisteAnalyseService
    {
        public Task<InformationAnalyseResultDto> GetInformationAnalyse(string CodeQr);
    }
}
