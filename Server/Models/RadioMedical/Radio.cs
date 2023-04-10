using Newtonsoft.Json;
using Server.Models.Analyse;
using Server.Models.MedicalOrder;
using Server.Models.Pharmacist;
using Server.Models.Radiologys;
using Server.Models.ResultAnalyses;
using Server.Models.ResultsRadio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.RadioMedical
{
    public class Radio
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string QrCode { get; set; }
        [Required]
        public string Instruction { get; set; }
        [Required]
        public StatusRadio Status { get; set; }

        public DateTime DateValidation { get; set; }
        [ForeignKey("Radiology")]
        public Guid IdRadiology { get; set; }
        public Radiology Radiology { get; set; }
        [ForeignKey("MedicalOrdres")]
        public Guid IdOrdreMedical { get; set; }
        public MedicalOrdres MedicalOrdres { get; set; }
        [JsonIgnore]
        public IEnumerable<ResultRadio> resultRadios { get; set; }

    }
}
