using Newtonsoft.Json;
using Server.Models.SpecialtieDoctor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Specialites
{
    public class Specialite
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public string NameSpecialite { get; set; }
        [JsonIgnore]
        public IEnumerable<SpecialtiesDoctor> specialtiesDoctors { get; set; }
    }
}
