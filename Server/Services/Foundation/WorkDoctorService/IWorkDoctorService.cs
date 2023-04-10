using DTO;
using Microsoft.Identity.Client;

namespace Server.Services.Foundation.WorkDoctorService
{
    public interface IWorkDoctorService
    {
        public Task PostNewInvitationWorkDoctor(string Email, string UserId);
        public Task<List<InvitationsDoctorDto>> GetInvitationDoctor(string Email);
        public Task UpdateStatusServiceWorkDoctor(string Email, UpdateStatusWorkDoctorDto updateStatusWorkDoctorDto);
        public Task<List<JobsDoctorDto>> GetListJobsDoctorService(string Email);
        public Task<JobSettingDto> GetJobDoctorById(string Email, string IdJob);
        public Task UpdateSettingJobDoctor(JobSettingDto jobSettingDto, string Email);
        public Task<List<DoctorCabinetDto>> GetDoctorInformationFromCabinet(string Email);

        public Task DeleteWorkDoctorByAdmin(string Email, string IdJob);
        public Task DeleteInvitationWorkDoctorByDoctor(string Email, string jobId);

    }
}
