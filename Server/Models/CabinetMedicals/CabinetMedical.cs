using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Server.Models.Admin;
using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.secretary;
using Server.Models.WorkDoctor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Server.Models.CabinetMedicals
{
    [Index(nameof(NameCabinet))]
    public class CabinetMedical
    {
        [Key]
        public Guid Id { get; set; }

        [Required]

        public string NameCabinet { get; set; }
        [Required]
        public string Adress { get; set; }
       
        [DataType(DataType.Upload)]

        public byte[]? image { get; set; }
       // public string image { get; set; }
        [Required]
        public string MapAdress { get; set; }
        [Required]
        public string numberPhone { get; set; }
        public StatusService statusService { get; set; }
        [Required]
        public string JobTime { get; set; }
        [Required]
        public string Services { get; set; }
        [JsonIgnore]
        public IEnumerable<WorkDoctors> WorkDoctors { get; set; }
        [JsonIgnore]
        public IEnumerable<Admins> Admins { get; set; }
        [JsonIgnore]
        public IEnumerable<Secretarys> Secretarys { get; set; }
        [JsonIgnore]
        public IEnumerable<MedicalPlanning> MedicalPlanning { get; set; }
        [JsonIgnore]
        public IEnumerable<MedicalOrdres> MedicalOrder { get; set; }



    }
}
