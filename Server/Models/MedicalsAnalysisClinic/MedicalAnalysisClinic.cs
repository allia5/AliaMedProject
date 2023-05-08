using Newtonsoft.Json;
using Server.Models.SpecialisteAnalyses;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.MedicalsAnalysisClinic
{
    public class MedicalAnalysisClinic
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string MedicalAnalysisName { get; set; }
        [Required]
        public string AdressMedicalAnalysis { get; set; }
        [Required]
        public string NumberPhone { get; set; }
        [Required]
        public string Services { get; set; }
        [Required]
        public string EmailMedicalAnalysis { get; set; }
        [JsonIgnore]
        public IEnumerable<SpecialisteAnalyse> SpecialisteAnalyses { get; set; }
    }
}
