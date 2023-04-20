using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalPlannings;
using Server.Models.Prescriptions;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;

namespace Server.Services.Foundation.OrdreMedicalService
{
    
    public partial class OrdreMedicalService
    {
        public void ValidateAnalyseOnAdd(AnalyseToAddDto analyseToAddDto)
        {
            if(analyseToAddDto.Description == null || analyseToAddDto.FileMedicalAnalyse ==null)
            {
                throw new ArgumentException(nameof(analyseToAddDto));
            }
        }
      
        public void ValidateRadioOnAdd(RadioToAddDto radioToAddDto )
        {
            if(radioToAddDto.FileMedicalRadio == null || radioToAddDto.Description == null)
            {
                throw new ArgumentException(nameof(radioToAddDto));
            }
        }
        public void validateeFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if (ArePropertiesNull(fileMedicals))
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
    }
}
