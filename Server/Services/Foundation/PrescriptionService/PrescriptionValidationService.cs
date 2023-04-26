using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.Pharmacist;
using Server.Models.Prescriptions;
using Server.Models.RadioMedical;
using Server.Models.SpecialisteAnalyses;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.PrescriptionService
{
    public partial class PrescriptionService
    {
        public void ValidateEntryOnGetPrescriptionInformation(string Email, string CodeQr)
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentNullException("email");
            }
            if (string.IsNullOrEmpty(CodeQr))
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
        public void validationPatientIsNull(User PatientUser)
        {
            if (PatientUser == null) throw new ArgumentNullException(nameof(PatientUser));
        }
        public void ValidateFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if (fileMedicals == null) throw new ArgumentNullException(nameof(fileMedicals));
        }
        public void ValidatePrescriptionIsNull(Prescription prescription)
        {
            if (prescription == null)
            {
                throw new NullDataStorageException(nameof(prescription));
            }
        }
        public void ValidatePharmacist(Pharmacists pharmacists)
        {
            if (pharmacists == null || pharmacists.status == Models.MedicalAnalysis.Statuspharmacist.Deactivated)
            {
                throw new NullException(nameof(pharmacists));
            }
        }
    }
}
