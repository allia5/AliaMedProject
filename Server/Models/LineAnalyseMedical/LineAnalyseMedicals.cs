using Server.Models.Analyse;
using Server.Models.MedicalAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Server.Models.ResultAnalyses;
using Newtonsoft.Json;

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
        [ForeignKey("MedicalAnalyse")]
        public Guid? IdMedicalAnalyse { get; set; }
        public MedicalAnalyse MedicalAnalyse { get; set; }
        [JsonIgnore]
        public IEnumerable<ResultAnalyse> ResultAnalyse { get; set; }
    }
}
