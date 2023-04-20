using DTO;

namespace Client.Services.Foundations.OrdreMedicalService
{
    public interface IOrdreMedicalService
    {
        public Task<OrdreMedicalDto> PostOrdreMedicalPatient(OrderMedicalToAddDro orderMedicalToAddDro);
        public Task<List<InformationOrderMedicalSecritary>> GetAllOrdreMedicalSecritary(KeysAppoimentInformationSecretary keysAppoimentInformationSecretary);
        public Task UpdateStatusOrdreMedicalBySecritary(UpdateOrdreMedicalDto updateOrdreMedicalDto);
       


    }
}
