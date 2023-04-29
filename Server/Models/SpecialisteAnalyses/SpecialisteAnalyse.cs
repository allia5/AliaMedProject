using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models.Analyse;
using Server.Models.LineAnalyseMedical;
using Server.Models.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.SpecialisteAnalyses
{
    [Index(nameof(NameMedicalAnalyse))]
    public class SpecialisteAnalyse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string NameMedicalAnalyse { get; set; }
        [Required]
        public string AuthenticationNumber { get; set; }

        public string Adress { get; set; }
        [Required]

        public StatusSpecialisteAnalyse Status;
        [ForeignKey("User")]
        public string IdUser { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public IEnumerable<LineAnalyseMedicals> LinesAnalyses { get; set; }
    }
}
