using DTO;

namespace Client.Services.Foundations.WorkDoctorService
{
    public interface IWorkDoctorService
    {
        public Task SendInvitationWorkToDoctot(string IdUser);
        public Task<List<InvitationsDoctorDto>> invitationsDoctorService();
        public Task UpdateStatusServiceWorkDoctor(UpdateStatusWorkDoctorDto updateStatusWorkDoctorDto);
        public Task<List<JobsDoctorDto>> GetJobsDoctorService();
        public Task<JobSettingDto> GetJobSetting(string IdJob);
        public Task UpdateJobSetting(JobSettingDto jobSettingDto);
        public Task<List<DoctorCabinetDto>> GetListDoctorsInformationCabinet();
        public Task DeleteJobDoctorByAdmin(string IdJob);
        public Task DeleteJobDoctorByDoctor(string IdJob);
    }
}
