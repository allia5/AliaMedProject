using DTO;

namespace Server.Services.Foundation.DoctorService
{
    public interface IDoctorService
    {
        public Task<List<DoctorInformationDto>> GetListInformationFromDoctor(string Email);
    }
}
