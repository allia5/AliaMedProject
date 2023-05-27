using Client.Services.Exceptions;
using DTO;

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Client.Services.Foundations.UserService
{
    public class UserService : IUserService
    {
        public HttpClient HttpClient { get; set; }
        public UserService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public async Task<List<DoctorSearchDto>> GetListDoctorAvailble(int CityId)
        {

            var result = await this.HttpClient.GetAsync($"/api/Patient/GetDoctorsAvailble/{CityId}");
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

        public async Task ForgotPasswordUserAccount(string Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                throw new BadRequestException("Bad Request");
            }
           var result = await this.HttpClient.GetAsync($"/api/UserAccount/ForgotPasswordUserAccount/{Email}");
            if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new BadRequestException("Bad Request");
            }else if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new ProblemException("Error Intern");
            }
        }

        public async Task ResetPasswordUserAccount(ResetPasswordUserAccountDto ResetPasswordUserAccountDto)
        {
            
            var requesHttp = new HttpRequestMessage(HttpMethod.Patch, "/api/UserAccount/ResetPasswordUserAccount");
            var Json = JsonSerializer.Serialize(ResetPasswordUserAccountDto);
           requesHttp.Content = new StringContent(Json, Encoding.UTF8, "application/json");
            var result = await this.HttpClient.SendAsync(requesHttp);
            if(result.StatusCode != System.Net.HttpStatusCode.OK)
            {
             
                throw new BadRequestException("Bad Request");
            }

        }
    }
}
