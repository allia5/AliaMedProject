using DTO;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.Prescriptions;
using Server.Models.secretary;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;

namespace Server.Services.Foundation.OrdreMedicalService
{
    
    public partial class OrdreMedicalService
    {
        public void ValidateRadioLineOnAdd(LineRadioMedicalDto lineRadioMedical)
        {
            if(lineRadioMedical.Instruction == null || lineRadioMedical.Description == null)
            {
                throw new ArgumentNullException(nameof(lineRadioMedical));
            }
        }
     public void ValidateOrdreMedicalIsNull(MedicalOrdres medicalOrdres)
        {
            if(medicalOrdres == null)
            {
                throw new NullException(nameof(medicalOrdres));
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
        public void ValidateEntryOnUpdateStatusSecritary(string Email, UpdateOrdreMedicalDto updateOrdreMedicalDto)
        {
            if (Email == null)
            {
                throw new ArgumentNullException(nameof(Email));
            }
           if(updateOrdreMedicalDto != null) 
            { 
                if(updateOrdreMedicalDto.OrdreMedicalId == null || updateOrdreMedicalDto.DoctorId ==null || updateOrdreMedicalDto.CabinetId == null)
                {
                    throw new ArgumentNullException(nameof(updateOrdreMedicalDto));
                }
            }
        }
        public void ValidateSecritary(Secretarys secretarys)
        {
            if(secretarys != null)
            {
                if(secretarys.Status != StatusSecretary.Active)
                {
                    throw new StatusValidationException(nameof(secretarys));
                }
            }
            else
            {
                throw new NullException(nameof(secretarys));
            }
        }
        public void ValidateEntryOnGetAllAppoimentPatientSecretary(string Email, KeysAppoimentInformationSecretary keysAppoimentInformationSecretary)
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentNullException(nameof(Email));
            }
            if (keysAppoimentInformationSecretary != null)
            {
                if (keysAppoimentInformationSecretary.CabinetId == null)
                {
                    throw new ArgumentNullException(nameof(keysAppoimentInformationSecretary.CabinetId));
                }
                else if (keysAppoimentInformationSecretary.IdDoctor == null)
                {
                    throw new ArgumentNullException(nameof(keysAppoimentInformationSecretary.IdDoctor));
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(keysAppoimentInformationSecretary));
            }
        }
        public void ValidateAnalyseOnAdd(AnalyseToAddDto analyseToAddDto)
        {
            if(analyseToAddDto.LineAnalyseMedicals.Count() == 0 || analyseToAddDto.FileMedicalAnalyse ==null)
            {
                throw new ArgumentException(nameof(analyseToAddDto));
            }
        }
      
        public void ValidateRadioOnAdd(RadioToAddDto radioToAddDto )
        {
            if(radioToAddDto.FileMedicalRadio == null || radioToAddDto.LineRadioMedicals.Count()==0)
            {
                throw new ArgumentException(nameof(radioToAddDto));
            }
        }
        public void validateeFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if (fileMedicals==null)
            {
                throw new NullException(nameof(fileMedicals));
            }
        }
        public void ValidatePrecriptionLineOnAdd(PrescriptionLineDto prescriptionLine)
        {
            if(prescriptionLine.Quantity ==null || prescriptionLine.MedicamentName == null)
            {
                throw new ArgumentException(nameof(prescriptionLine));
            }
        }
       public void  ValidatePrescriptionOnAdd(PrescriptionDto prescriptionDto)
        {
            if (prescriptionDto.prescriptionLines.Count() == 0 && prescriptionDto.PrescriptionFile !=null  )
            {
                throw new InvalidException(nameof(prescriptionDto),prescriptionDto,"Prescripion");
            }
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if(cabinetMedical == null)
            {
                throw new NullException(nameof(cabinetMedical));
            }
        }
        public void ValidateWorkDoctorIsNull(WorkDoctors workDoctors)
        {
            if (workDoctors == null)
            {
                throw new NullException(nameof(workDoctors));
            }
        }
        
        public void ValidateOrdreMedicalOnAdd(OrderMedicalToAddDro orderMedicalToAddDro)
        { 
            if(orderMedicalToAddDro == null)
            {
                throw new ArgumentNullException(nameof(ordreMedicalManager));

            }
            if(orderMedicalToAddDro.AnalyseToAdd == null && orderMedicalToAddDro.RadioToAdd == null && orderMedicalToAddDro.Prescription == null ) 
            {
                throw new ArgumentNullException(nameof(ordreMedicalManager));
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
        public void ValidateUserIsNull(User user)
        {
            if (user == null || user.Status == UserStatus.Deactivated)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidationDoctorIsNull(Doctors doctor)
        {
            if (doctor == null || doctor.StatusDoctor == StatusDoctor.Deactivated)
            {
                throw new NullException(nameof(doctor));

            }
        }

       public void ValidateAnalyseLineOnAdd(LineAnalyseMedicalDto lineAnalyseMedicalDto)
        {
            if(lineAnalyseMedicalDto.Instruction == null || lineAnalyseMedicalDto.Description == null)
            {
                throw new ArgumentNullException(nameof(lineAnalyseMedicalDto));
            }
        }
    }
}
