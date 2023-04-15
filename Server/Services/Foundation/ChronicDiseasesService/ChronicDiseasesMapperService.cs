using DTO;
using Server.Models.ChronicDiseases;

namespace Server.Services.Foundation.ChronicDiseasesService
{
    public static class ChronicDiseasesMapperService
    {
         public static chronicDiseasesDto MapperTochronicDiseasesDto(ChronicDisease chronicDisease)
        {
            return new chronicDiseasesDto
            {
                id = chronicDisease.Id,
                name = chronicDisease.NameChronicDiseases
            };
        }
    }
}
