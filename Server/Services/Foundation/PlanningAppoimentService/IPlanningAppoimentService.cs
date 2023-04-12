using DTO;

namespace Server.Services.Foundation.PlanningAppoimentService
{
    public interface IPlanningAppoimentService
    {
        public Task<List<AppointmentInformationDto>> PostNewPlanningAppoimentMedical(string Email, KeysReservationMedicalDto keysReservationMedicalDto);
        public Task<List<AppointmentInformationDto>> GetListPlanningAppoimentMedical(string Email);
        public Task DeleteMedicalPlanningAppoiment(string Email, string IdPlanning);
        public Task<List<PlanningDto>> GetPatientAppoimentMedicalSecretary(string Email, KeysAppoimentInformationSecretary keysAppoimentInformationSecretary);
        public Task<List<PlanningDto>> GetPatientAppoimentMedicalDoctor(string Email, KeysAppoimentInformationDoctor keysAppoimentInformationDoctor);
        public Task UpdateStatusAppoimentMedical(string Email,UpdateStatusAppoimentDto updateStatusAppoiment,string Role);
        public Task DelayeAppoimentPatient(string Email, DelayeAppoimentMedical delayeAppoiment);

    }
}
