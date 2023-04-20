using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;

namespace Server.Services.Foundation.FileMedicalService
{
    public partial class FileMedicalService
    {
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullException(nameof(cabinetMedical));
            }
        }
        public void validateeFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if (ArePropertiesNull(fileMedicals))
            {
                throw new NullException(nameof(fileMedicals));
            }
        }
        public void ValidateEntryOnUpdateFileMedical(string Email,UpdateFileMedicalDto updateFileMedicalDto)
        {
            if(ArePropertiesNull(updateFileMedicalDto) == false)
            {
                throw new ArgumentNullException(nameof(updateFileMedicalDto));
            }else if(Email == null)
            {
                throw new ArgumentNullException(nameof(Email));
            }
        }
        public void  ValidateEntryOnGetFilePatient(string Email,string AppointmentId)
        {
            if(Email == null) {
                throw new ArgumentNullException(nameof(Email));
            }else if(AppointmentId == null)
            {
                throw new ArgumentNullException(nameof(AppointmentId));
            }
        }
        public void ValidateWorkDoctorIsNull(WorkDoctors workDoctors)
        {
            if (workDoctors == null)
            {
                throw new NullException(nameof(workDoctors));
            }
        }
        public void ValidateAppointmentWithDoctor(MedicalPlanning medicalPlanning,Doctors doctors)
        {
            if(medicalPlanning.IdDoctor != doctors.Id || medicalPlanning.AppointmentDate.Date != DateTime.Now.Date)
            {
                throw new CompatibilityError(nameof(medicalPlanning),nameof(doctors));
            }
            if(medicalPlanning.Status == StatusPlaning.passed)
            {
                throw new StatusValidationException(nameof(medicalPlanning));
            }

        }
        public void ValidatePlanningIsNull(MedicalPlanning medicalPlanning)
        {
            if (medicalPlanning == null)
            {
                throw new NullException(nameof(medicalPlanning));
            }
        }
        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidationDoctorIsNull(Doctors doctor)
        {
            if (doctor == null)
            {
                throw new NullException(nameof(doctor));

            }
        }
        public void ValidateEntryOnAddFileMedical(string Email, FileMedicalToAddDto fileMedicalPatient)
        {
            if(Email == null) throw new ArgumentNullException("email");
            if (fileMedicalPatient != null)
            {
                if (ArePropertiesNull(fileMedicalPatient) == false)
                {
                    throw new ArgumentException(nameof(fileMedicalPatient));
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(fileMedicalPatient));
            }
           
        }
        public static bool ArePropertiesNull(object obj)
        {
            if (obj == null)
                return true;

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.GetValue(obj) == null)
                    return false;
            }

            return true;
        }

    }
}
