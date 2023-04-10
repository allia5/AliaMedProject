using Server.Models.ChronicDiseases;
using Server.Models.fileMedical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.FileChronicDisease
{
    public class FileChronicDiseases
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("fileMedicals")]
        public Guid IdFile { get; set; }
        public fileMedicals fileMedicals { get; set; }
        [ForeignKey("ChronicDisease")]
        public int IdChronicDisease { get; set; }
        public ChronicDisease ChronicDisease { get; set; }
    }
}
