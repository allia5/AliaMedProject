using DTO;

namespace Client.Services.Foundations.OrdreMedicalService
{
    public interface IOrdreMedicalService
    {
        public Task<OrdreMedicalDto> PostOrdreMedicalPatient(OrderMedicalToAddDro orderMedicalToAddDro);
        
    }
}
