using Client.Services.Exceptions;
using DTO;
using System.Net.Http.Json;

namespace Client.Services.Foundations.UserService
{
    public class UserService : IUserService
    {
        public HttpClient HttpClient { get; set; }
        public UserService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public async Task<List<DoctorSearchDto>> GetListDoctorAvailble()
        {

            var result = await this.HttpClient.GetAsync("/api/Patient/GetDoctorsAvailble");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Content.Headers.ContentLength != 0)
                {
                    return await result.Content.ReadFromJsonAsync<List<DoctorSearchDto>>();
                }
                else
                {
                    return new List<DoctorSearchDto>();
                }

            }
            else
            {
                throw new BadRequestException("Was Error");
            }


        }
    }
}
