using DTO;

namespace Client.Services.Foundations.MedicalPlanningService
{
    public interface IMedicalPlanningService
    {
        public Task<List<AppointmentInformationDto>> PostAppointmentInformationDto(KeysReservationMedicalDto keysReservationMedicalDto);
        public Task<List<AppointmentInformationDto>> GetAppointmentInformationDto();
        public Task DeleteMedecalAppoiment(string IdMedicalAppoiment);
    }
}
