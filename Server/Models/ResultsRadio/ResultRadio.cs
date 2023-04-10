using Server.Models.Analyse;
using Server.Models.RadioMedical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ResultsRadio
{
    public class ResultRadio
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string ReferenceFile { get; set; }
        [Required]
        public FileType FileType { get; set; }
        [ForeignKey("Radio")]
        public Guid IdRadio { get; set; }
        public Radio Radio { get; set; }
    }
}
