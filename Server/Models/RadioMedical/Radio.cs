using Newtonsoft.Json;
using Server.Models.Analyse;
using Server.Models.MedicalOrder;
using Server.Models.Pharmacist;
using Server.Models.Radiologys;
using Server.Models.ResultAnalyses;
using Server.Models.LineRadioMedical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.RadioMedical
{
    public class Radio
    {
        [Key]
        public Guid Id { get; set; }
        [DataType(DataType.Upload)]

        public byte[]? FileRadio { get; set; }
        [Required]
        public string? QrCode { get; set; }
       
        [ForeignKey("MedicalOrdres")]
        public Guid IdOrdreMedical { get; set; }
        public MedicalOrdres MedicalOrdres { get; set; }
        [JsonIgnore]
        public IEnumerable<LineRadioMedicals> LineRadioMedicals { get; set; }

    }
}
