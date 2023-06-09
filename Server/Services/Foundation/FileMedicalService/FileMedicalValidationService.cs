﻿using DTO;
using Server.Models.Analyse;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalPlannings;
using Server.Models.Prescriptions;
using Server.Models.RadioMedical;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;

namespace Server.Services.Foundation.FileMedicalService
{
    public partial class FileMedicalService
    {
        public void ValidateEntryOnTransferFileMedical(string Email,FileTransferDto fileTransferDto)
        {
            if (string.IsNullOrEmpty(Email)) { throw new ArgumentNullException(); }
            if(ArePropertiesNull(fileTransferDto))
            {
                throw new ArgumentNullException();
            }
            
        }
        public void ValidatePrescriptionIsNull(Prescription prescription)
        {
            if(prescription == null)
            {
                throw new NullException(nameof(prescription));
            }
        }
        public void ValidateRadioIsNull(Radio Radio)
        {
            if (Radio == null)
            {
                throw new NullException(nameof(Radio));
            }
        }
        public void ValidateAnalyseIsNull(Analyses Analyse)
        {
            if (Analyse == null)
            {
                throw new NullException(nameof(Analyse));
            }
        }
        public void  ValidateStringIsNull(string Entry)
        {
            if(Entry == null) throw new ArgumentNullException(nameof(Entry));
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullException(nameof(cabinetMedical));
            }
        }
        public void validateeFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if (fileMedicals==null)
            {
                throw new NullException(nameof(fileMedicals));
            }
        }
        public void ValidateEntryOnUpdateFileMedical(string Email,UpdateFileMedicalDto updateFileMedicalDto)
        {
            if(ArePropertiesNull(updateFileMedicalDto))
            {
                throw new ArgumentNullException(nameof(updateFileMedicalDto));
            }else if(Email == null)
            {
                throw new ArgumentNullException(nameof(Email));
            }
        }
        public void  ValidateEntryOnGetFilePatient(string Email,string AppointmentId)
        {
            if(Email == null) {
                throw new ArgumentNullException(nameof(Email));
            }else if(AppointmentId == null)
            {
                throw new ArgumentNullException(nameof(AppointmentId));
            }
        }
        public void ValidateWorkDoctorIsNull(WorkDoctors workDoctors)
        {
            if (workDoctors == null)
            {
                throw new NullException(nameof(workDoctors));
            }
        }
        public void ValidateAppointmentWithDoctor(MedicalPlanning medicalPlanning,Doctors doctors)
        {
            if(medicalPlanning.IdDoctor != doctors.Id || medicalPlanning.AppointmentDate.Date != DateTime.Now.Date)
            {
                throw new CompatibilityError(nameof(medicalPlanning),nameof(doctors));
            }
            if(medicalPlanning.Status == StatusPlaning.passed)
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
        public void ValidationDoctor(Doctors doctor)
        {
            if (doctor == null || doctor.StatusDoctor == StatusDoctor.Deactivated)
            {
                throw new NullException(nameof(doctor));

            }
        }
        public void ValidationDoctorIsNull(Doctors doctor)
        {
            if (doctor == null)
            {
                throw new NullException(nameof(doctor));

            }
        }
        public void ValidateEntryOnAddFileMedical(string Email, FileMedicalToAddDto fileMedicalPatient)
        {
            if(Email == null) throw new ArgumentNullException("email");
            if (fileMedicalPatient != null)
            {
                if (ArePropertiesNull(fileMedicalPatient))
                {
                    throw new ArgumentException(nameof(fileMedicalPatient));
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(fileMedicalPatient));
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

    }
}
