using Server.Models.Radiologys;
using Server.Models.RadioMedical;
using Server.Models.ResultsRadio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.LineRadioMedical
{
    public class LineRadioMedicals
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
      
        public string? Instruction { get; set; }
        [Required]
        public StatusRadio Status { get; set; }

        public DateTime? DateValidation { get; set; }
        [ForeignKey("Radiology")]
        public Guid? IdRadiology { get; set; }
        public Radiology Radiology { get; set; }
        [ForeignKey("Radio")]
        public Guid IdRadio { get; set; }
        public Radio Radio { get; set; }
        public IEnumerable<ResultRadio> resultRadios { get; set; }
    }
}
