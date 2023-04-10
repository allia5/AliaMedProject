using Server.Models.fileMedical;
using Server.Models.UserAccount;
using Server.Models.Admin;

using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.Radiologys;
using Server.Models.WorkDoctor;
using Server.Models.SpecialtieDoctor;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Doctor
{
    public class Doctors
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string NumberRRPS { get; set; }
        [Required]
        public string InsuranceNumber { get; set; }
        [Required]
        public string LicenseNumber { get; set; }
        [Required]
        public StatusDoctor StatusDoctor { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }


        [JsonIgnore]
        public IEnumerable<Admins> Admin { get; set; }
        [JsonIgnore]
        public IEnumerable<fileMedicals> fileMedical { get; set; }
        [JsonIgnore]
        public IEnumerable<MedicalOrdres> MedicalOrder { get; set; }
        [JsonIgnore]
        public IEnumerable<MedicalPlanning> MedicalPlanning { get; set; }
        [JsonIgnore]
        public IEnumerable<Radiology> Radiology { get; set; }
        [JsonIgnore]
        public IEnumerable<WorkDoctors> WorkDoctor { get; set; }
        [JsonIgnore]
        public IEnumerable<SpecialtiesDoctor> SpecialtiesDoctor { get; set; }

    }
}
