using DTO;

namespace Client.Services.Foundations.UserService
{
    public interface IUserService
    {
        public Task<List<DoctorSearchDto>> GetListDoctorAvailble(); 
    }
}
