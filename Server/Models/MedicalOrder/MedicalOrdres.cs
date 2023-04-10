using Newtonsoft.Json;
using Server.Models.Analyse;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.fileMedical;
using Server.Models.Prescriptions;
using Server.Models.RadioMedical;
using Server.Models.secretary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.MedicalOrder
{
    public class MedicalOrdres
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string summary { get; set; }
        [Required]
        public StatuseOrdreMedical Status { get; set; }
        public DateTime DateValidation { get; set; }
        [ForeignKey("Doctors")]
        public Guid IdDoctor { get; set; }
        public Doctors Doctors { get; set; }
        [ForeignKey("fileMedicals")]
        public Guid IdFileMedical { get; set; }
        public fileMedicals fileMedicals { get; set; }
        [ForeignKey("Secretarys")]
        public Guid IdSecritary { get; set; }
        public Secretarys Secretarys { get; set; }
        [ForeignKey("CabinetMedical")]
        public Guid IdCabinetMedical { get; set; }
        public CabinetMedical CabinetMedical { get; set; }
        [JsonIgnore]
        public IEnumerable<Prescription> prescriptions { get; set; }
        [JsonIgnore]
        public IEnumerable<Analyses> Analyses { get; set; }
        [JsonIgnore]
        public IEnumerable<Radio> Radios { get; set; }
    }
}
