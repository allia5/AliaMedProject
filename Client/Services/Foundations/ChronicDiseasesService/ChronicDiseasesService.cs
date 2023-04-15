using Client.Services.Exceptions;
using DTO;
using System.Net.Http.Json;

namespace Client.Services.Foundations.ChronicDiseasesService
{
    public class ChronicDiseasesService : IChronicDiseasesService
    {
        public readonly HttpClient HttpClient;
        public ChronicDiseasesService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }
       
        public async Task<List<chronicDiseasesDto>> GetChronicDiseasesAsync()
        {
            var result  = await this.HttpClient.GetAsync("/api/ChronicDiseases/GetAllchronicDiseases");
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
               return await result.Content.ReadFromJsonAsync<List<chronicDiseasesDto>>();
            }
            else
            {
                throw new ProblemException("Error Inten");
            }
           

        }
    }
}
