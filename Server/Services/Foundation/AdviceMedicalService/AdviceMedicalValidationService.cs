using Microsoft.IdentityModel.Tokens;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.MedicalOrder;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.AdviceMedicalService
{
    public partial class AdviceMedicalService
    {
        public void ValidateEntryOnGetAdvicesMedical(string Email,string OrdreMedcialId)
        {
            if (Email.IsNullOrEmpty())
            {
                throw new ArgumentNullException("email");
            }
            if (OrdreMedcialId.IsNullOrEmpty())
            {
                throw new ArgumentNullException("OrdreMedcialId");
            }
        }
        public void ValidateUser(User user)
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
        public void ValidateOrdreMedical(MedicalOrdres medicalOrdres)
        {
            if (medicalOrdres == null || medicalOrdres.Status == StatuseOrdreMedical.NotValidate)
            {
                throw new NullException(nameof(medicalOrdres));
            }
        }
    }
  
}
