using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.CabinetMedicalService
{
    public partial class CabinetMedicalService
    {
        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidateEntryString(string Entry)
        {
            if (String.IsNullOrWhiteSpace(Entry))
            {
                throw new NullException(nameof(Entry));
            }
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullDataStorageException(nameof(cabinetMedical));
            }
        }
        public void ValidateCabinetMedicalDtoIsNull(CabinetMedicalDto cabinetMedicalDto)
        {
            if (cabinetMedicalDto == null)
            {
                throw new NullException(nameof(cabinetMedicalDto));
            }
        }
        public void ValidateEntryOnUpdate(string Email, CabinetMedicalDto cabinetMedicalDto)
        {
            ValidateEntryString(Email);
            ValidateCabinetMedicalDtoIsNull(cabinetMedicalDto);
            ValidateEntryString(cabinetMedicalDto.phoneNumber);
            ValidateEntryString(cabinetMedicalDto.name);
            ValidateEntryString(cabinetMedicalDto.Services);
            ValidateEntryString(cabinetMedicalDto.Adress);
            ValidateEntryString(cabinetMedicalDto.Status.ToString());
            ValidateEntryString(cabinetMedicalDto.JobTime);



        }
    }
}
