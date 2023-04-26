using DocumentFormat.OpenXml.Packaging;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.MedicalOrder;
using Server.Models.RadioMedical;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;
using Server.Models.fileMedical;

namespace Server.Services.Foundation.RadioMedicalService
{
    public partial class RadioMedicalService
    {
        public void validationPatientIsNull(User PatientUser)
        {
            if(PatientUser == null) throw new ArgumentNullException(nameof(PatientUser));
        }
        public void ValidateFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if(fileMedicals == null) throw new ArgumentNullException(nameof(fileMedicals));
        }
        public void ValidateRadioIsNull(Radio radio)
        {
            if(radio == null)
            {
                throw new NullDataStorageException(nameof(radio));
            }
        }
        public void ValidateEntryOnGetRadioInformation(string Email,string CodeQr)
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentNullException("email");
            }
            if(string.IsNullOrEmpty(CodeQr))
            {
                throw new ArgumentNullException("codeQr");
            }
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
            if (user == null || user.Status == UserStatus.Deactivated)
            {
                throw new NullException(nameof(user));
            }
        }
    }
}
