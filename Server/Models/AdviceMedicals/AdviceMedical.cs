using Newtonsoft.Json;
using Server.Models.MedicalOrder;
using Server.Models.UserAccount;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.AdviceMedicals
{
    public class AdviceMedical
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("MedicalOrdres")]
        public Guid OrdreMedicalId { get; set; }
        public MedicalOrdres MedicalOrdres { get; set; }



        [ForeignKey("User")]
        public string transmitterUserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime DateSendMessage { get; set; }
    
        [DefaultValue(0)]
        public StatusViewReceiver StatusViewReceiver { get; set; }
    }
}
