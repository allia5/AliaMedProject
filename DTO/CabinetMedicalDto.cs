using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CabinetMedicalDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string JobTime { get; set; }
        [Required]
        public string Services { get; set; }
        public StatusCabinet Status { get; set; }

        public string mapAdress { get; set; }
    }

    public enum StatusCabinet
    {
        Open = 1,
        Close = 0
    }
}
