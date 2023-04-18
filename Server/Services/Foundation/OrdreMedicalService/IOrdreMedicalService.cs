using DTO;

namespace Server.Services.Foundation.OrdreMedicalService
{
    public interface IOrdreMedicalService
    {
        public Task<OrdreMedicalDto> AddOrdreMedicalDto(string Email, OrderMedicalToAddDro orderMedicalToAdd);
    }
}
