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
        [DataType(DataType.Upload)]

        public byte[]? FileResult { get; set; }
        [Required]
        public string fileType { get; set; }
        [ForeignKey("Radio")]
        public Guid IdRadio { get; set; }
        public Radio Radio { get; set; }
    }
}
