using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models.AdviceMedicals;
using Server.Models.Doctor;
using Server.Models.fileMedical;
using Server.Models.MedicalAnalysis;
using Server.Models.MedicalPlannings;
using Server.Models.Pharmacist;
using Server.Models.secretary;
using Server.Models.SpecialisteAnalyses;
using Server.Models.UserRoles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.UserAccount
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {


        [Required]
        public string Firstname { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string NationalNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public EnumSexe Sexe { get; set; }
        [Required]
        public UserStatus Status { get; set; }
        [Required]
        public DateTime DateCreateAccount { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? DateExpireRefreshToken { get; set; }

        [JsonIgnore]
        public IEnumerable<Pharmacists> pharmacists { get; set; }
        [JsonIgnore]
        public IEnumerable<SpecialisteAnalyse> MedicalAnalyse { get; set; }
        [JsonIgnore]
        public IEnumerable<Doctors> Doctor { get; set; }
        [JsonIgnore]
        public IEnumerable<Secretarys> Secretarys { get; set; }
        [JsonIgnore]
        public IEnumerable<MedicalPlanning> MedicalPlanning { get; set; }
        [JsonIgnore]
        public IEnumerable<fileMedicals> fileMedical { get; set; }
        [JsonIgnore]
        public IEnumerable<UserRole> usersRoles { get; set; }
        [JsonIgnore]
        public IEnumerable<AdviceMedical> AdviceMedicalSender { get; set; }
       /* [JsonIgnore]
        [InverseProperty("ReceiverUser")]
        public IEnumerable<AdviceMedical> AdviceMedicalReceiver { get; set; }*/


    }
}
