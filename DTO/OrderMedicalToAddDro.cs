﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderMedicalToAddDro
    {
        public string AppointmentId { get; set; }
        public string FileId { get; set; }
        public string? Summary { get; set; }
        [Required]
        public StatusVisibility Visibility { get; set; }
        public AnalyseToAddDto? AnalyseToAdd { get; set; }
        public RadioToAddDto? RadioToAdd { get; set; }
        public PrescriptionDto? Prescription { get; set; }
    }
    public enum StatusVisibility
    {
        Public = 1,
        Privet = 0
    }
    public class PrescriptionDto
    {
        //  [JsonConverter(typeof(ByteArrayConverter))]
        [Required]
        public byte[] PrescriptionFile { get; set; }
        public string? Instruction { get; set; }
        [Required]
        public List<PrescriptionLineDto> prescriptionLines { get; set; }
    }

    public class PrescriptionLineDto
    {
        [Required]
        public string MedicamentName { get; set; }
        
        public string? Description { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Quantity { get; set; }

    }
    public class RadioToAddDto
    {
        //  [JsonConverter(typeof(ByteArrayConverter))]
        [Required]
        public byte[] FileMedicalRadio { get; set; }
        [Required]
        public List<LineRadioMedicalDto> LineRadioMedicals { get; set; }

    }
    public class LineRadioMedicalDto
    {
        [Required]
        public string Description { get; set; }
        
        public string? Instruction { get; set; }
    }

    public class AnalyseToAddDto
    {
        // [JsonConverter(typeof(ByteArrayConverter))]
        [Required]
        public byte[] FileMedicalAnalyse { get; set; }
        public List<LineAnalyseMedicalDto> LineAnalyseMedicals { get; set; }
       
    }
    public class LineAnalyseMedicalDto
    {
        [Required]
        public string Description { get; set; }
       
        public string? Instruction { get; set; }
    }
}

