using Server.Models.Pharmacist;
using Server.Models.Prescriptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.PrescriptionLine
{
    public class PrescriptionLines
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string MedicamentName { get; set; }

        public string Description { get; set; }
        [Required]
        public int Dosage { get; set; }
        [Required]
        public StatusPrescriptionLine StatusPrescriptionLine { get; set; }

        public DateTime DateValidation { get; set; }
        [ForeignKey("Prescription")]
        public Guid IdPrescription { get; set; }
        public Prescription Prescription { get; set; }
        [ForeignKey("Pharmacists")]
        public Guid IdPharmacist { get; set; }
        public Pharmacists Pharmacists { get; set; }

    }
}
