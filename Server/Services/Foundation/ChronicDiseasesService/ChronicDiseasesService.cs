using DTO;
using Server.Managers.Storages.ChronicDiseasesManager;
using static Server.Services.Foundation.ChronicDiseasesService.ChronicDiseasesMapperService;
namespace Server.Services.Foundation.ChronicDiseasesService
{
    public class ChronicDiseasesService : IChronicDiseasesService
    {
        public readonly IChronicDiseasesManager chronicDiseasesManager;
        public ChronicDiseasesService(IChronicDiseasesManager chronicDiseasesManager)
        {
            this.chronicDiseasesManager = chronicDiseasesManager;
        }
        public async Task<List<chronicDiseasesDto>> GetChronicDiseasesAsync()
        {
            List<chronicDiseasesDto> chronicDiseasesDtos = new List<chronicDiseasesDto>();
            var ListchronicDiseases = await this.chronicDiseasesManager.SelectAllChronicDiseasesAsync();
            foreach(var item in  ListchronicDiseases)
            {
                var chronicDiseaseDto = MapperTochronicDiseasesDto(item);
                chronicDiseasesDtos.Add(chronicDiseaseDto);
            }
            return chronicDiseasesDtos;
        }
    }
}
