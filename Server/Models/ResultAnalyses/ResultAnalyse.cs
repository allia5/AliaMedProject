using Server.Models.Analyse;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ResultAnalyses
{
    public class ResultAnalyse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string ReferenceFile { get; set; }
        [Required]
        public FileType FileType { get; set; }
        [ForeignKey("Analyses")]
        public Guid IdAnalyse { get; set; }
        public Analyses Analyses { get; set; }
    }
}
