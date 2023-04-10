using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.secretary;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.SecretaryService
{
    public partial class SecretaryService
    {
        public void ValidateEntry(string Entry)
        {
            if (Entry == null)
            {
                throw new NullException(nameof(Entry));
            }
        }
        public void ValidateOnUpdateStatusSecretary(string Email, UpdateStatusSecretaryDto updateStatusSecretaryDto)
        {
            ValidateEntry(Email);
            if (updateStatusSecretaryDto == null)
            {
                throw new NullException(nameof(updateStatusSecretaryDto));
            }

        }
        public bool IsValideSecritary(Secretarys secretarys)
        {
            if (secretarys != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ValidateSecritayIsNull(Secretarys secretarys)
        {
            if (secretarys == null)
            {


                throw new NullException(nameof(secretarys));
            }
        }
        public void ValidateSecritay(Secretarys secretarys)
        {
            if (IsValideSecritary(secretarys))
            {


                if (secretarys.Status == StatusSecretary.Active || secretarys.Status == StatusSecretary.Block)
                {
                    throw new OccuredDataException(nameof(secretarys));
                }
            }
        }
        public void ValidateEntry(string EmailSecretary, string EmailAdmin)
        {
            if (IsInvalid(EmailAdmin) || IsInvalid(EmailAdmin))
            {
                string Email = nameof(EmailAdmin) + nameof(EmailSecretary);
                throw new NullException(nameof(Email));
            }
            else
            {
                if (!IsValidEmail(EmailSecretary))
                {
                    throw new InvalidException(nameof(EmailSecretary), EmailSecretary, "Secretary");
                }
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidateUserSecretaryIsNull(User SecretaryUser)
        {
            if (SecretaryUser == null)
            {
                throw new NullDataStorageException(nameof(SecretaryUser));
            }
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullException(nameof(cabinetMedical));
            }
        }
        public static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
    }
}
