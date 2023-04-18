﻿using Newtonsoft.Json;
using Server.Models.MedicalAnalysis;
using Server.Models.MedicalOrder;
using Server.Models.Pharmacist;
using Server.Models.ResultAnalyses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;


namespace Server.Models.Analyse
{
    public class Analyses
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string description { get; set; }
        [DataType(DataType.Upload)]

        public byte[]? FileAnalyse { get; set; }
        [Required]
        public string QrCode { get; set; }
        public string? Instruction { get; set; }
        [Required]
        public StatusAnalyse Status { get; set; }
        [Required]
        public DateTime? DateValidation { get; set; }
        [ForeignKey("MedicalAnalyse")]
        public Guid? IdMedicalAnalyse { get; set; }
        public MedicalAnalyse MedicalAnalyse { get; set; }
        [ForeignKey("MedicalOrdres")]
        public Guid IdOrdreMedical { get; set; }
        public MedicalOrdres MedicalOrdres { get; set; }
        [JsonIgnore]
        public IEnumerable<ResultAnalyse> ResultAnalyse { get; set; }

    }
}
