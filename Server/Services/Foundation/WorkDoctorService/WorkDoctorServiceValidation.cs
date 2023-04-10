using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;

namespace Server.Services.Foundation.WorkDoctorService
{
    public partial class WorkDoctorService
    {
        public void ValidateListUsers(List<User> Users)
        {
            if (Users.Count() == 0)
            {
                throw new NullException(nameof(Users));
            }
        }
        public void ValidateOnUpdateStatusWorkDoctoter(string Email, UpdateStatusWorkDoctorDto updateStatusWorkDoctorDto)
        {
            if (updateStatusWorkDoctorDto != null)
            {
                if (updateStatusWorkDoctorDto?.Status == null)
                {
                    throw new NullException(nameof(updateStatusWorkDoctorDto.Status));
                }
                else if (updateStatusWorkDoctorDto.WorkId == null)
                {
                    throw new NullException(nameof(updateStatusWorkDoctorDto.WorkId));
                }
            }
            else
            {
                throw new NullException(nameof(updateStatusWorkDoctorDto));
            }
            if (Email == null)
            {
                throw new NullException(nameof(Email));
            }

        }
        public void ValidateEmailIsNull(string Email)
        {
            if (IsInvalid(Email))
            {
                throw new NullException(nameof(Email));
            }
        }
        public void ValidateListInvitationWorkDoctor(List<WorkDoctors> worksInvitation)
        {
            if (worksInvitation.Count() == 0)
            {
                throw new NullDataStorageException(nameof(worksInvitation));
            }
        }
        public void ValidateOnPostInvitationWorkDoctor(string Email, string parametre)
        {
            if (IsInvalid(Email))
            {
                throw new NullException(nameof(Email));
            }
            else if (IsInvalid(parametre))
            {
                throw new NullException(nameof(parametre));
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
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullDataStorageException(nameof(cabinetMedical));
            }
        }
        public void ValidateWorkDoctorIsNull(WorkDoctors workDoctors)
        {
            if (workDoctors == null)
            {
                throw new NullException(nameof(workDoctors));
            }
        }

        public static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
        public void ValidateOnUpdateSettingJob(JobSettingDto jobSettingDto)
        {
            if (IsInvalid(jobSettingDto.IdJob))
            {
                throw new NullException(nameof(jobSettingDto));
            }
            else if (IsInvalid(jobSettingDto.NumberPatientAccepted.ToString()))
            {
                throw new NullException(nameof(jobSettingDto));
            }
            else if (IsInvalid(jobSettingDto.processingMinutes.ToString()))
            {
                throw new NullException(nameof(jobSettingDto));
            }
            else if (IsInvalid(jobSettingDto.startTime.ToString()))
            {
                throw new NullException(nameof(jobSettingDto));
            }
            else if (IsInvalid(jobSettingDto.EndTime.ToString()))
            {
                throw new NullException(nameof(jobSettingDto));
            }

        }
    }
}
