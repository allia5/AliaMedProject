using Server.Models.Doctor;
using Server.Models.LineAnalyseMedical;
using Server.Models.LineRadioMedical;
using Server.Models.RadioMedical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Radiologys
{
    public class Radiology
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Services { get; set; }

        public string Materiel { get; set; }
        [ForeignKey("Doctors")]
        public Guid IdDoctor { get; set; }
        public Doctors Doctors { get; set; }
        public IEnumerable<LineRadioMedicals> RadioLines { get; set; }
    }
}
