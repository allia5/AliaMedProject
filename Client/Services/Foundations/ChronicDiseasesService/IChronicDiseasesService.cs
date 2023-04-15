using DTO;

namespace Client.Services.Foundations.ChronicDiseasesService
{
    public interface IChronicDiseasesService
    {
        public Task<List<chronicDiseasesDto>> GetChronicDiseasesAsync();
    }
}
