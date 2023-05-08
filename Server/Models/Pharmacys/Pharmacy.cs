using Newtonsoft.Json;
using Server.Models.Pharmacist;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Pharmacys
{
    public class Pharmacy
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string PharmacistName { get; set; }
        [Required]
        public string AdressPharmacist { get; set; }
        [Required]
        public string NumberPhone { get; set; }
        [Required]
        public string Services { get; set; }
        [Required]
        public string EmailPharmacy { get; set; }
        [JsonIgnore]
        public IEnumerable<Pharmacists> Pharmacists { get; set; }
    }
}
