using DTO;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.FileMedicalService
{
    public partial class FileMedicalService
    {
        public void ValidateAppointmentWithDoctor(MedicalPlanning medicalPlanning,Doctors doctors)
        {
            if(medicalPlanning.IdDoctor != doctors.Id)
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
