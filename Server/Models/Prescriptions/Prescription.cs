using Newtonsoft.Json;
using Server.Models.MedicalOrder;
using Server.Models.PrescriptionLine;
using Server.Models.RadioMedical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Prescriptions
{
    public class Prescription
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string qrCode { get; set; }
        [DataType(DataType.Upload)]

        public byte[]? FilePrescription { get; set; }
        public string instruction { get; set; }
        [ForeignKey("MedicalOrdres")]
        public Guid IdMedicalOrdre { get; set; }
        public MedicalOrdres MedicalOrdres { get; set; }
        [JsonIgnore]
        public IEnumerable<PrescriptionLines> prescriptionLines { get; set; }
    }
}
