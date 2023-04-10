using Server.Models.Doctor;
using Server.Models.Specialites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.SpecialtieDoctor
{
    public class SpecialtiesDoctor
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Doctors")]
        public Guid IdDoctor { get; set; }
        public Doctors Doctors { get; set; }
        [ForeignKey("Specialite")]
        public int SpecialitesId { get; set; }
        public Specialite Specialite { get; set; }
    }
}
