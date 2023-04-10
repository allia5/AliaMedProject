using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models.Doctor;
using Server.Models.MedicalPlannings;
using System.Runtime.CompilerServices;

namespace Server.Managers.Storages.PlanningAppoimentManager
{
    public class PlanningAppoimentManager : IPlanningAppoimentManager
    {
        public ServerDbContext ServerDbContext { get; set; }
        public PlanningAppoimentManager(ServerDbContext ServerDbContext)
        {
            this.ServerDbContext = ServerDbContext;
        }
        public async Task<MedicalPlanning> InsertMedicalPlanning(MedicalPlanning medicalPlanning)
        {
            var result = this.ServerDbContext.medicalPlannings.Add(medicalPlanning);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<MedicalPlanning>> SelectMedicalPlanningByIdDoctorIdCabinet(Guid CabinetId, Guid DoctorId, DateTime Date)
        {
            return await (from planning in this.ServerDbContext.medicalPlannings
                          where planning.IdCabinet == CabinetId
                          && planning.IdDoctor == DoctorId
                          && planning.AppointmentDate.Date == Date.Date
                          select planning).ToListAsync();
        }

        public async Task<List<MedicalPlanning>> SelectMedicalPlanningByIdUser(string UserId)
        {
            return await (from planning in this.ServerDbContext.medicalPlannings
                          where planning.IdUser == UserId
                          && planning.Status == StatusPlaning.Still


                          select planning).ToListAsync();
        }

        public async Task<MedicalPlanning> DeletePlanningMedical(MedicalPlanning MedicalPlanning)
        {
            var result = this.ServerDbContext.medicalPlannings.Remove(MedicalPlanning);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<MedicalPlanning> SelectPalnningMedicalByIdPlanningIdUser(Guid Id, string UserId)
        {
            return await (from planning in this.ServerDbContext.medicalPlannings where planning.Id == Id && planning.IdUser == UserId select planning).FirstOrDefaultAsync();
        }

        public async Task<List<MedicalPlanning>> SelectMedicalPlanningByIdDoctorIdCabinet(Guid IdDoctor, Guid IdCabinet)
        {
            return await (from planning in this.ServerDbContext.medicalPlannings where planning.IdCabinet == IdCabinet && planning.IdDoctor == IdDoctor select planning).ToListAsync();
        }

        public async Task<MedicalPlanning> UpdatePlanningMedical(MedicalPlanning medicalPlanning)
        {
            var result = this.ServerDbContext.Update(medicalPlanning);
            await this.ServerDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
