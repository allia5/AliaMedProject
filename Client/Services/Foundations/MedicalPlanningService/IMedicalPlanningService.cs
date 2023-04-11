using DTO;

namespace Client.Services.Foundations.MedicalPlanningService
{
    public interface IMedicalPlanningService
    {
        public Task<List<AppointmentInformationDto>> PostAppointmentInformationDto(KeysReservationMedicalDto keysReservationMedicalDto);
        public Task<List<AppointmentInformationDto>> GetAppointmentInformationDto();
        public Task DeleteMedecalAppoiment(string IdMedicalAppoiment);
        public Task<List<PlanningDto>> GetAppointmentInformationPatientSecretaryDto(KeysAppoimentInformationSecretary keysAppoimentInformationSecretary);
        public Task<List<PlanningDto>> GetAppointmentInformationPatientDoctorDto(KeysAppoimentInformationDoctor keysAppoimentInformationDoctor);
        public Task UpdateStatusApoimentPatient(UpdateStatusAppoimentDto updateStatusAppoimentPatient);
    }
}
