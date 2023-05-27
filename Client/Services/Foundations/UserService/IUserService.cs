using DTO;

namespace Client.Services.Foundations.UserService
{
    public interface IUserService
    {

        public Task<List<DoctorSearchDto>> GetListDoctorAvailble(int CityId);
        public Task ForgotPasswordUserAccount(string Email);
        public Task ResetPasswordUserAccount(ResetPasswordUserAccountDto ResetPasswordUserAccountDto);
    }
}
