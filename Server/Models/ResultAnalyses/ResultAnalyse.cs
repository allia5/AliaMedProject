using Server.Models.Analyse;
using Server.Models.LineAnalyseMedical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ResultAnalyses
{
    public class ResultAnalyse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Upload)]

        public byte[]? AnalyseResult { get; set; }
        [Required]
        public FileType FileType { get; set; }
        [ForeignKey("Analyses")]
        public Guid IdLineAnalyse { get; set; }
        public LineAnalyseMedicals LineAnalyseMedicals { get; set; }
    }
}
