using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models.Analyse;
using Server.Models.LineAnalyseMedical;
using Server.Models.MedicalsAnalysisClinic;
using Server.Models.Pharmacys;
using Server.Models.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.SpecialisteAnalyses
{

    public class SpecialisteAnalyse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string AuthenticationNumber { get; set; }

        [Required]

        public StatusSpecialisteAnalyse Status { get; set; }
        [ForeignKey("User")]
        public string IdUser { get; set; }
        public User User { get; set; }
        [ForeignKey("MedicalAnalysisClinic")]
        public Guid MedicalAnalyseClinicId { get; set; }
        public MedicalAnalysisClinic MedicalAnalysisClinic { get; set; }
        [JsonIgnore]
        public IEnumerable<LineAnalyseMedicals> LinesAnalyses { get; set; }
    }
}
