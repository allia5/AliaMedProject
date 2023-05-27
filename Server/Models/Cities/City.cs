using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models.CabinetMedicals;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Cities
{

    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public IEnumerable<CabinetMedical> CabinetMedical { get; set; }
             

    }
}
