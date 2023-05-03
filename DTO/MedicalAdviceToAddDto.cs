using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MedicalAdviceToAddDto
    {
        public string OrdreMedicalId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
