using Microsoft.IdentityModel.Tokens;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.Pharmacist;
using Server.Models.Prescriptions;
using Server.Models.RadioMedical;
using Server.Models.secretary;
using Server.Models.SpecialisteAnalyses;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.PrescriptionService
{
    public partial class PrescriptionService
    {
        public void ValidateCabinetMedical(CabinetMedical cabinetMedical)
        {
            if(cabinetMedical == null && cabinetMedical.statusService == StatusService.OffLine)
            {
                throw new NullException(nameof(cabinetMedical));
            }
        }
        public void ValidateSecritary(Secretarys secretarys)
        {
            if (secretarys == null || secretarys.Status != StatusSecretary.Active)
            {
                throw new NullException(nameof(secretarys));
            }
        }
        public void ValidateEntryOnGetFilePrescription(string Email,string OrdreMedicalId,string CabinetId)
        {
            if (Email.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Email));
            if (OrdreMedicalId.IsNullOrEmpty()) throw new ArgumentNullException(nameof(OrdreMedicalId));
            if (CabinetId.IsNullOrEmpty()) throw new ArgumentNullException(nameof(CabinetId));
        }
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
        public void ValidateOrdreMedical(MedicalOrdres medicalOrdres)
        {
            if (medicalOrdres == null || medicalOrdres.Status == StatuseOrdreMedical.NotValidate)
            {
                throw new ArgumentNullException(nameof(medicalOrdres));
            }
        }
        public void ValidateOrdreMedicalIsNull(MedicalOrdres medicalOrdres)
        {
            if (medicalOrdres == null )
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
