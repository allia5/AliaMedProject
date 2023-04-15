using DTO;

namespace Server.Services.Foundation.ChronicDiseasesService
{
    public interface IChronicDiseasesService
    {
        public Task<List<chronicDiseasesDto>> GetChronicDiseasesAsync();
    }
}
