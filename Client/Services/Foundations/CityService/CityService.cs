using Client.Services.Exceptions;
using DTO;
using System.Net.Http.Json;

namespace Client.Services.Foundations.CityService
{
    public class CityService : ICityService
    {
        public HttpClient HttpClient { get; set; }
        public CityService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<List<CityDto>> GetAllCities()
        {
            var resultHttpRequest = await this.HttpClient.GetAsync("/api/City/GetAllCities");
            if(resultHttpRequest.StatusCode == System.Net.HttpStatusCode.OK) 
            { 
                return await resultHttpRequest.Content.ReadFromJsonAsync<List<CityDto>>();
            }
            else
            {
                throw new BadRequestException("Bad Request");
            }
        }
    }
}
