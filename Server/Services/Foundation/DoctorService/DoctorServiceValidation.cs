using Server.Models.CabinetMedicals;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.DoctorService
{
    public partial class DoctorService
    {
        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullDataStorageException(nameof(cabinetMedical));
            }
        }

        public void ValidateListUserIsEmpty(List<User> ListUser)
        {
            if (ListUser.Count() == 0)
            {
                throw new NotFoundException(nameof(ListUser));
            }
        }
    }
}
