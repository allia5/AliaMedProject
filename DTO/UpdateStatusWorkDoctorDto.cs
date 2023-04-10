using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UpdateStatusWorkDoctorDto
    {
        public string WorkId { get; set; }
        public StatusWorkDoctor Status { get; set; }
    }
    public enum StatusWorkDoctor
    {
        accepted = 1,
        Notaccepted = 2,
        still = 0


    }
}
