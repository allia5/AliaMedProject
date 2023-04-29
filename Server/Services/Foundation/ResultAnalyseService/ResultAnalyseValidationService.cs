using DTO;
using Microsoft.IdentityModel.Tokens;
using Server.Models.Analyse;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.LineAnalyseMedical;
using Server.Models.LineRadioMedical;
using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.Radiologys;
using Server.Models.RadioMedical;
using Server.Models.ResultAnalyses;
using Server.Models.SpecialisteAnalyses;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;

namespace Server.Services.Foundation.ResultAnalyseService
{
    public partial class ResultAnalyseService
    {
        public void ValidateEntryOnGetFileResult(string Email,string AppointmentId , string LineId)
        {
            if(Email.IsNullOrEmpty() || AppointmentId.IsNullOrEmpty() || LineId.IsNullOrEmpty())
            {
                throw new ArgumentNullException();
            }
        }

        public void ValidateLineAnlayse(LineAnalyseMedicals lineAnalyseMedicals)
        {
            if (lineAnalyseMedicals == null || lineAnalyseMedicals.Status != StatusAnalyse.validate)
            {
                throw new NullException(nameof(lineAnalyseMedicals));
            }
            
        }
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
            if (PatientUser == null || PatientUser.Status == UserStatus.Deactivated) throw new ArgumentNullException(nameof(PatientUser));
        }

        
        public void ValidateAnalyseIsNull(Analyses Analyse)
        {
            if (Analyse == null)
            {
                throw new NullDataStorageException(nameof(Analyse));
            }
        }

        public void ValidateOrdreMedical(MedicalOrdres medicalOrdres)
        {
            if (medicalOrdres == null || medicalOrdres.Visibility == Models.MedicalOrder.StatusVisibility.Privet || medicalOrdres.Status==StatuseOrdreMedical.NotValidate)
            {
                throw new NullException(nameof(medicalOrdres));
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
            if (doctor == null ||doctor.StatusDoctor == StatusDoctor.Deactivated)
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
        public void ValidateResultLineMedical(ResultAnalyse resultAnalyse)
        {
            if(resultAnalyse == null)
            {
                throw new NullException(nameof(resultAnalyse));
            }
        }
    }
}
