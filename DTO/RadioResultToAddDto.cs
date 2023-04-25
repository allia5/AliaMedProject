using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RadioResultToAddDto
    {
        public string IdLineRadio { get; set; }
        [Required]
        public byte[] FileUpload { get; set; }
    }
}
