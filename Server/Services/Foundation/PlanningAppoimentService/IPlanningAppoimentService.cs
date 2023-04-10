using DTO;

namespace Server.Services.Foundation.PlanningAppoimentService
{
    public interface IPlanningAppoimentService
    {
        public Task<List<AppointmentInformationDto>> PostNewPlanningAppoimentMedical(string Email, KeysReservationMedicalDto keysReservationMedicalDto);
        public Task<List<AppointmentInformationDto>> GetListPlanningAppoimentMedical(string Email);
        public Task DeleteMedicalPlanningAppoiment(string Email, string IdPlanning);
    }
}
