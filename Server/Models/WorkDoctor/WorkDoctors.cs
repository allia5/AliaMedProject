using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.WorkDoctor
{
    public class WorkDoctors
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime ReadyTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TimeOfConsultation { get; set; }
        public StatusService statusServcie { get; set; }
        public StatusReservation statusReservation { get; set; }
        public StatusWork StatusWork { get; set; }
        public DateTime DateInvitation { get; set; }
        public int NbPatientAvailble { get; set; }
        [ForeignKey("Doctors")]
        public Guid IdDoctor { get; set; }
        public Doctors Doctors { get; set; }
        [ForeignKey("CabinetMedical")]
        public Guid IdCabinet { get; set; }
        public CabinetMedical CabinetMedical { get; set; }
    }
}
