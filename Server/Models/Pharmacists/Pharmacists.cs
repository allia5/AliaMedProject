using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models.MedicalAnalysis;
using Server.Models.MedicalsAnalysisClinic;
using Server.Models.Pharmacys;
using Server.Models.PrescriptionLine;
using Server.Models.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Server.Models.Pharmacist
{
  
    public class Pharmacists
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string AuthenticationNumber { get; set; }
        [Required]
        public Statuspharmacist status { get; set; }
        [ForeignKey("User")]
        public string idUser { get; set; }
        public User User { get; set; }
        [ForeignKey("Pharmacy")]
        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set;}

        [JsonIgnore]
        public IEnumerable<PrescriptionLines> PrescriptionLines { get; set; }
    }
}
