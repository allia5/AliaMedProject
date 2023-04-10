using DTO;
using MimeKit.Cryptography;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;
using System.ComponentModel;

namespace Server.Services.Foundation.PlanningAppoimentService
{
    public partial class PlanningAppoimentService
    {
        public void ValidateEntryOnDelete(string Email, string IdPlanning)
        {
            if (IsInvalid(Email))
            {
                throw new InvalidException(nameof(Email), Email, "User");
            }
            else if (IsInvalid(IdPlanning))
            {
                throw new InvalidException(nameof(IdPlanning), IdPlanning, "User");
            }


        }
        public void ValidateUserIsNotInListAppoiment(string userId, List<MedicalPlanning> medicalPlannings)
        {
            var result = medicalPlannings.Where(e => e.IdUser == userId).FirstOrDefault();
            if (result != null)
            {
                throw new OccuredDataException(nameof(userId));
            }
        }
        public void ValidateWorkDoctorIsNull(WorkDoctors workDoctors)
        {
            if (workDoctors == null)
            {
                throw new NullException(nameof(workDoctors));
            }
        }
        public void ValidateStatusDocotor(Doctors Doctor)
        {
            if (Doctor.StatusDoctor == StatusDoctor.Deactivated)
            {
                throw new StatusValidationException(nameof(Doctor));
            }
        }
        public void ValidateStatusUser(User user)
        {
            if (user.Status == UserStatus.Deactivated)
            {
                throw new StatusValidationException(nameof(user));
            }
        }
        public void ValidateEntryOnPostNewAppoimentPlanning(string Email, KeysReservationMedicalDto keysReservationMedicalDto)
        {
            if (keysReservationMedicalDto != null)
            {
                if (IsInvalid(keysReservationMedicalDto.IdJob) || IsInvalid(keysReservationMedicalDto.IdCabinet) || IsInvalid(keysReservationMedicalDto.IdUserDoctor))
                {
                    throw new NullException(nameof(keysReservationMedicalDto));
                }
            }

            if (Email == null)
            {
                throw new NullException(nameof(Email));
            }
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullException(nameof(cabinetMedical));
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

        public void ValidatePlanningIsNull(MedicalPlanning medicalPlanning)
        {
            if (medicalPlanning == null)
            {
                throw new NullException(nameof(medicalPlanning));
            }
        }
        public static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
    }
}
