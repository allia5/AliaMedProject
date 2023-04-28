﻿using Newtonsoft.Json;
using Server.Models.MedicalOrder;
using Server.Models.UserAccount;
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



        [ForeignKey("TransmitterUser")]
        public string TransmitterUserId { get; set; }

        [ForeignKey("ReceiverUser")]
        public string ReceiverUserId { get; set; }

        [JsonIgnore]
        public User TransmitterUser { get; set; }

        [JsonIgnore]
        public User ReceiverUser { get; set; }

        [Required]
        public string Message { get; set; }
        public StatusAdviceMedcial StatusAdviceMedcial { get; set; }
    }
}