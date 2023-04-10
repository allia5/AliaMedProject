using System.ComponentModel.DataAnnotations;

namespace Server.Models.ChronicDiseases
{
    public class ChronicDisease
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NameChronicDiseases { get; set; }
    }
}
