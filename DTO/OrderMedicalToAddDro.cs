using System;
using System.Collections.Generic;
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
        public string FileId { get; set;}
        public string Summary { get; set; }
       public StatusVisibility Visibility { get; set; }
        public AnalyseToAddDto AnalyseToAdd { get; set; }
        public RadioToAddDto RadioToAdd { get; set; }
        public PrescriptionDto Prescription { get; set; }
    }
    public enum StatusVisibility
    {
        Public =1,
        Privet =0
    }
    public class PrescriptionDto 
    {
        //  [JsonConverter(typeof(ByteArrayConverter))]
        [Required]
        public byte[] PrescriptionFile { get; set; }
        public string Instruction { get; set; }
        public List<PrescriptionLineDto> prescriptionLines { get; set; }
    }

    public class PrescriptionLineDto
    {

        public string MedicamentName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

    }
    public class RadioToAddDto
    {
        //  [JsonConverter(typeof(ByteArrayConverter))]
        [Required]
        public byte[] FileMedicalRadio { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
    }

    public class AnalyseToAddDto
    {
        // [JsonConverter(typeof(ByteArrayConverter))]
        [Required]
        public byte[] FileMedicalAnalyse { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
    }
}

public class ByteArrayConverter : JsonConverter<byte[]>
{
    public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        short[] sByteArray = JsonSerializer.Deserialize<short[]>(ref reader);
        byte[] value = new byte[sByteArray.Length];
        for (int i = 0; i < sByteArray.Length; i++)
        {
            value[i] = (byte)sByteArray[i];
        }

        return value;
    }

    public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var val in value)
        {
            writer.WriteNumberValue(val);
        }

        writer.WriteEndArray();
    }
}