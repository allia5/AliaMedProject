using DTO;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.RadioMedical;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.ResultRadioService
{
    public partial class ResultRadioService
    {
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
            if (PatientUser == null) throw new ArgumentNullException(nameof(PatientUser));
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
                    throw new ArgumentNullException(radioResultToAddDto);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(radioResultToAddDto));
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

        public void ValidateOrdreMedicalIsNull(MedicalOrdres medicalOrdres)
        {
            if (medicalOrdres == null)
            {
                throw new ArgumentNullException(nameof(medicalOrdres));
            }
        }
        public void ValidationDoctorIsNull(Doctors doctor)
        {
            if (doctor == null)
            {
                throw new NullException(nameof(doctor));

            }
        }
        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }

    }
}
