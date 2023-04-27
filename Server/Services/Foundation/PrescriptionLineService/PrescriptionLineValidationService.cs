using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.Pharmacist;
using Server.Models.PrescriptionLine;
using Server.Models.Prescriptions;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.PrescriptionLineService
{
    public partial class PrescriptionLineService
    {
        public void ValidatePrescriptionLine(PrescriptionLines prescriptionLines)
        {
            if (prescriptionLines == null )
            {
                throw new ArgumentNullException(nameof(prescriptionLines));
            }
            if(prescriptionLines.StatusPrescriptionLine == StatusPrescriptionLine.Validate)
            {
                throw new StatusValidationException(nameof(prescriptionLines));
            }
        }
        public void ValidateEntryOnUpdatePrescriptionLine(string Email, string LinePrescriptionId)
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentNullException("email");
            }
            if (string.IsNullOrEmpty(LinePrescriptionId))
            {
                throw new ArgumentNullException("LinePrescriptionId");
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
            if (doctor == null || doctor.StatusDoctor == StatusDoctor.Deactivated)
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
            if (PatientUser == null || PatientUser.Status == UserStatus.Deactivated) throw new ArgumentNullException(nameof(PatientUser));
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
