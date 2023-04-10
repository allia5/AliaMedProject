using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using Server.Data;
using Server.Models.WorkDoctor;

namespace Server.Managers.Storages.WorkDoctorManager
{
    public class WorkDoctorManager : IWorkDoctorManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public WorkDoctorManager(ServerDbContext ServerDbContext)
        {
            this.ServerDbContext = ServerDbContext;
        }
        public async Task<List<WorkDoctors>> SelectWorksDoctorByIdCabinet(Guid IdCabinet)
        {
            return await (from Work in this.ServerDbContext.WorkDoctors where Work.IdCabinet == IdCabinet select Work).ToListAsync();
        }

        public async Task<WorkDoctors> InsertWorkDoctor(WorkDoctors workDoctors)
        {
            try
            {
                var result = await this.ServerDbContext.WorkDoctors.AddAsync(workDoctors);
                await this.ServerDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<WorkDoctors>> SelectWorksDoctorByIdDoctor(Guid DoctorId)
        {
            return await (from Work in this.ServerDbContext.WorkDoctors where Work.IdDoctor == DoctorId && Work.StatusWork == StatusWork.still select Work).ToListAsync();
        }

        public async Task<WorkDoctors> UpdateWorkDoctor(WorkDoctors workDoctors)
        {
            var result = this.ServerDbContext.WorkDoctors.Update(workDoctors);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<WorkDoctors> SelectWorkDoctorByIdDoctorWithIdWorkDoctor(Guid id, Guid IdDoctor)
        {
            return await (from work in this.ServerDbContext.WorkDoctors where work.Id == id && work.IdDoctor == IdDoctor select work).FirstAsync();
        }

        public async Task<List<WorkDoctors>> SelectWorksDoctorActiveByIdDoctor(Guid DoctorId)
        {
            try
            {
                return await (from Work in this.ServerDbContext.WorkDoctors where Work.IdDoctor == DoctorId && (Work.StatusWork == StatusWork.accepted || Work.StatusWork == StatusWork.Notaccepted) select Work).ToListAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<WorkDoctors> DeleteWorkDoctor(WorkDoctors workDoctors)
        {
            var result = this.ServerDbContext.WorkDoctors.Remove(workDoctors);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;


        }

        public async Task<WorkDoctors> SelectWorkDoctorByIdAndIdCabinet(Guid Id, Guid IdCabinet)
        {
            return await (from job in this.ServerDbContext.WorkDoctors where job.Id == Id && job.IdCabinet == IdCabinet select job).FirstAsync();
        }

        public async Task<List<WorkDoctors>> SelectWorksDoctorByIdDoctorActive(Guid DoctorId)
        {
            return await (from Work in this.ServerDbContext.WorkDoctors
                          where Work.IdDoctor == DoctorId
                          && Work.StatusWork == StatusWork.accepted
                          && Work.statusServcie == StatusService.InService
                          && Work.statusReservation == StatusReservation.Available
                          select Work).ToListAsync();
        }

        public async Task<WorkDoctors> SelectWorkDoctorByIdDoctorIdCabinetWithStatusActive(Guid DoctorId, Guid CabinetId)
        {
            return await (from Work in this.ServerDbContext.WorkDoctors
                          where Work.IdDoctor == DoctorId && Work.IdCabinet == CabinetId && Work.StatusWork == StatusWork.accepted
                          && Work.statusServcie == StatusService.InService
                          && Work.statusReservation == StatusReservation.Available
                          select Work).FirstOrDefaultAsync();
        }

        public async Task<List<WorkDoctors>> SelectAllWorkDoctorWithStatusActiveByIdCabinet(Guid IdCabinet)
        {
            return await (from job in this.ServerDbContext.WorkDoctors
                          where job.IdCabinet == IdCabinet
                          && job.statusServcie == StatusService.InService
                          && job.StatusWork == StatusWork.accepted
                          && job.statusReservation == StatusReservation.Available
                          select job).ToListAsync();
        }
    }
}
