using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Admin
{
    public class Admins
    {
        [Key]

        public Guid Id { get; set; }
        [ForeignKey("CabinetMedical")]
        public Guid IdCabinet { get; set; }
        public CabinetMedical CabinetMedical { get; set; }
        [ForeignKey("Doctors")]
        public Guid IdDoctor { get; set; }
        public Doctors Doctors { get; set; }
    }
}
