using DTO;
using Microsoft.IdentityModel.Tokens;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.fileMedical;
using Server.Models.MedicalOrder;
using Server.Models.UserAccount;
using System.Runtime.Serialization;

namespace Server.Services.Foundation.AdviceMedicalService
{
    public partial class AdviceMedicalService
    {
        public void ValidateFileMedicalIsNull(fileMedicals fileMedicals)
        {
            if(fileMedicals == null)
            {
                throw new NullException(nameof(fileMedicals));
            }
        }
        public void ValidationSimilarity(User UserAccountOne,User UserAccountTwo)
        {
            if(UserAccountOne != UserAccountTwo)
            {
                throw new NullException("Faild User");
            }
        }
        public void ValidateEntryOnPostAdvicesMedical(string Email,MedicalAdviceToAddDto medicalAdviceToAddDto)
        {
            if(Email.IsNullOrEmpty()) { throw new ArgumentNullException("email"); }
            if(medicalAdviceToAddDto != null)
            {
                if(medicalAdviceToAddDto.OrdreMedicalId == null) { throw new ArgumentNullException(); }
                if(medicalAdviceToAddDto.Message == null) { throw new ArgumentNullException(); }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
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
