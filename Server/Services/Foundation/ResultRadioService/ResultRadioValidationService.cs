using DTO;
using Microsoft.IdentityModel.Tokens;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.LineRadioMedical;
using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.Radiologys;
using Server.Models.RadioMedical;
using Server.Models.ResultAnalyses;
using Server.Models.ResultsRadio;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;

namespace Server.Services.Foundation.ResultRadioService
{
    public partial class ResultRadioService
    {
        public void ValidateResultLineMedical(ResultRadio resultRadio)
        {
            if (resultRadio == null)
            {
                throw new NullException(nameof(resultRadio));
            }
        }
        public void ValidateOrdreMedical(MedicalOrdres medicalOrdres)
        {
            if (medicalOrdres == null || medicalOrdres.Visibility == Models.MedicalOrder.StatusVisibility.Privet || medicalOrdres.Status == StatuseOrdreMedical.NotValidate)
            {
                throw new NullException(nameof(medicalOrdres));
            }
        }
        public void ValidateLineRadio(LineRadioMedicals lineRadioMedicals)
        {
            if(lineRadioMedicals.Status == StatusRadio.notValidate || lineRadioMedicals == null)
            {
                
                throw new NullException(nameof(LineRadioMedicals));
            }
            
            
        }
        public void ValidateEntryOnGetFileResult(string Email, string AppointmentId, string LineId)
        {
            if (Email.IsNullOrEmpty() || AppointmentId.IsNullOrEmpty() || LineId.IsNullOrEmpty())
            {
                throw new ArgumentNullException();
            }
        }
        public void ValidateFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if (fileMedicals == null) throw new ArgumentNullException(nameof(fileMedicals));
        }
        public void ValidateRadioIsNull(Radio radio)
        {
            if (radio == null)
            {
                throw new NullDataStorageException(nameof(radio));
            }
        }
        public void validationPatientIsNull(User PatientUser)
        {
            if (PatientUser == null || PatientUser.Status == UserStatus.Deactivated) throw new ArgumentNullException(nameof(PatientUser));
        }
        public void ValidateResultRadioOnAdd(string Email,RadioResultToAddDto radioResultToAddDto)
        {
            if(Email == null)
            {
                throw new ArgumentNullException("email");
            }
            if(radioResultToAddDto != null)
            {
                if (ArePropertiesNull(radioResultToAddDto))
                {
                    throw new ArgumentNullException(nameof(radioResultToAddDto));
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(radioResultToAddDto));
            }
        }
       public void ValidateLineRadioIsNull(LineRadioMedicals lineRadioMedicals)
        {
            if(lineRadioMedicals == null)
            {
                throw new NullDataStorageException(nameof(lineRadioMedicals));
            }
        }
        public static bool ArePropertiesNull(object obj)
        {
            if (obj == null)
                return true;

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.GetValue(obj) == null)
                    return true;
            }

            return false;
        }

        public void ValidateOrdreMedicalIsNull(MedicalOrdres medicalOrdres)
        {
            if (medicalOrdres == null)
            {
                throw new ArgumentNullException(nameof(medicalOrdres));
            }
        }
        public void ValidationDoctorIsNull(Doctors doctor)
        {
            if (doctor == null || doctor.StatusDoctor== StatusDoctor.Deactivated)
            {
                throw new NullException(nameof(doctor));

            }
        }
        public void ValidateRadiologyIsNull(Radiology radiology)
        {
            if(radiology == null)
            {
                throw new NullException(nameof(radiology));
            }
        }
        public void ValidateUserIsNull(User user)
        {
            if (user == null || user.Status == UserStatus.Deactivated)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidateAppointmentWithDoctor(MedicalPlanning medicalPlanning, Doctors doctors)
        {
            if (medicalPlanning.IdDoctor != doctors.Id || medicalPlanning.AppointmentDate.Date != DateTime.Now.Date)
            {
                throw new CompatibilityError(nameof(medicalPlanning), nameof(doctors));
            }
            if (medicalPlanning.Status == StatusPlaning.passed)
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
        public void ValidateWorkDoctorIsNull(WorkDoctors workDoctors)
        {
            if (workDoctors == null)
            {
                throw new NullException(nameof(workDoctors));
            }
        }
    }
}
