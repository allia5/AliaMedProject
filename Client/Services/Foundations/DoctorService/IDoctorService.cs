using DTO;

namespace Client.Services.Foundations.DoctorService
{
    public interface IDoctorService
    {
        public Task<List<DoctorInformationDto>> GetListInformationDoctors();
    }
}
