using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models.Analyse;
using Server.Models.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.MedicalAnalysis
{
    [Index(nameof(NameMedicalAnalyse))]
    public class MedicalAnalyse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string NameMedicalAnalyse { get; set; }
        [Required]
        public string AuthenticationNumber { get; set; }

        public string Adress { get; set; }
        [Required]

        public StatusMedicalAnalyse Status;
        [ForeignKey("User")]
        public string IdUser { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public IEnumerable<Analyses> Analyses { get; set; }
    }
}
