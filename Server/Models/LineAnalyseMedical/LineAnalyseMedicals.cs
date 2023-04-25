using Server.Models.Analyse;
using Server.Models.MedicalAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Server.Models.ResultAnalyses;
using Newtonsoft.Json;
using Server.Models.SpecialisteAnalyses;

namespace Server.Models.LineAnalyseMedical
{
    public class LineAnalyseMedicals
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string description { get; set; }
        public string? Instruction { get; set; }
        [Required]
        public StatusAnalyse Status { get; set; }

        public DateTime? DateValidation { get; set; }
        [ForeignKey("Analyses")]
        public Guid IdAnalyse { get; set; }
        public Analyses Analyses { get; set; }
        [ForeignKey("SpecialisteAnalyse")]
        public Guid? IdSpecialisteAnalyse { get; set; }
        public SpecialisteAnalyse SpecialisteAnalyse { get; set; }
        [JsonIgnore]
        public IEnumerable<ResultAnalyse> ResultAnalyse { get; set; }
    }
}
