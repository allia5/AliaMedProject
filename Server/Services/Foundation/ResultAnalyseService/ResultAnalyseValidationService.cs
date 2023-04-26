using DTO;
using Server.Models.Analyse;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.LineAnalyseMedical;
using Server.Models.LineRadioMedical;
using Server.Models.MedicalOrder;
using Server.Models.Radiologys;
using Server.Models.RadioMedical;
using Server.Models.SpecialisteAnalyses;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.ResultAnalyseService
{
    public partial class ResultAnalyseService
    {
        public void ValidateLineAnalyseIsNull(LineAnalyseMedicals lineAnalyseMedicals)
        {
            if(lineAnalyseMedicals == null)
            {
                throw new NullException(nameof(lineAnalyseMedicals));
            }
            if(lineAnalyseMedicals.Status == StatusAnalyse.validate)
            {
                throw new StatusValidationException(nameof(lineAnalyseMedicals.Status));
            }
        }
        public void ValidateSpecialisteAnalyse(SpecialisteAnalyse specialisteAnalyse)
        {
            if(specialisteAnalyse==null || specialisteAnalyse.Status== StatusSpecialisteAnalyse.Deactivated)
            {
                throw new NullException(nameof(specialisteAnalyse));
            }
        }
        public void ValidateEntryOnAddAnalyseResult(string Email,AnalyseResultToAdd analyseResultToAdd)
        {
            if (string.IsNullOrEmpty(Email)) { throw new ArgumentNullException(nameof(Email)); }
           if(analyseResultToAdd != null)
            {
                if(analyseResultToAdd.IdLineAnalyse ==null || analyseResultToAdd.FileUpload == null)
                {
                    throw new ArgumentNullException(nameof(analyseResultToAdd));
                }
            }else { 
                throw new ArgumentNullException(nameof(analyseResultToAdd));
            }
        }
        
        public void ValidateFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if (fileMedicals == null) throw new ArgumentNullException(nameof(fileMedicals));
        }
        public void validationPatientIsNull(User PatientUser)
        {
            if (PatientUser == null) throw new ArgumentNullException(nameof(PatientUser));
        }
       
        public void ValidateAnalyseIsNull(Analyses Analyse)
        {
            if (Analyse == null)
            {
                throw new NullDataStorageException(nameof(Analyse));
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
