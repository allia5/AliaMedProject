using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models.MedicalAnalysis;
using Server.Models.PrescriptionLine;
using Server.Models.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Server.Models.Pharmacist
{
    [Index(nameof(PharmacistName))]
    public class Pharmacists
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string PharmacistName { get; set; }
        [Required]
        public string AuthenticationNumber { get; set; }
        [Required]
        public string AdressPharmacist { get; set; }
        [Required]
        public Statuspharmacist status { get; set; }
        [ForeignKey("User")]
        public string idUser { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public IEnumerable<PrescriptionLines> PrescriptionLines { get; set; }
    }
}
