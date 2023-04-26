using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AnalyseResultToAdd
    {
        
            public string IdLineAnalyse { get; set; }
            [Required]
            public byte[] FileUpload { get; set; }
        
    }
}
