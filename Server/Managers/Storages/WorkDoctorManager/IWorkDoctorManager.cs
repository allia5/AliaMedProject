using Server.Models.WorkDoctor;

namespace Server.Managers.Storages.WorkDoctorManager
{
    public interface IWorkDoctorManager
    {
        public Task<List<WorkDoctors>> SelectWorksDoctorByIdCabinet(Guid IdCabinet);
        public Task<List<WorkDoctors>> SelectWorksDoctorByIdDoctor(Guid DoctorId);
        public Task<WorkDoctors> UpdateWorkDoctor(WorkDoctors workDoctors);
        public Task<WorkDoctors> SelectWorkDoctorByIdDoctorWithIdWorkDoctor(Guid id, Guid IdDoctor);
        public Task<WorkDoctors> InsertWorkDoctor(WorkDoctors workDoctors);
        public Task<List<WorkDoctors>> SelectWorksDoctorActiveByIdDoctor(Guid DoctorId);
        public Task<WorkDoctors> DeleteWorkDoctor(WorkDoctors workDoctors);
        public Task<WorkDoctors> SelectWorkDoctorByIdAndIdCabinet(Guid Id, Guid IdCabinet);
        public Task<List<WorkDoctors>> SelectWorksDoctorByIdDoctorActive(Guid DoctorId);
        public Task<WorkDoctors> SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(Guid DoctorId, Guid CabinetId);
        public Task<List<WorkDoctors>> SelectAllWorkDoctorWithStatusActiveByIdCabinet(Guid IdCabinet);

    }
}
