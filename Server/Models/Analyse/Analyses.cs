using Newtonsoft.Json;
using Server.Models.MedicalOrder;
using Server.Models.Pharmacist;
using Server.Models.ResultAnalyses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;


namespace Server.Models.Analyse
{
    public class Analyses
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string QrCode { get; set; }
        public string Instruction { get; set; }
        [Required]
        public StatusAnalyse Status { get; set; }
        [ForeignKey("Pharmacists")]
        public Guid IdPharmacist { get; set; }
        public Pharmacists Pharmacists { get; set; }
        [ForeignKey("MedicalOrdres")]
        public Guid IdOrdreMedical { get; set; }
        public MedicalOrdres MedicalOrdres { get; set; }
        [JsonIgnore]
        public IEnumerable<ResultAnalyse> ResultAnalyse { get; set; }

    }
}
