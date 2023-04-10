using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobsDoctorDto
    {
        public string nameCabinet { get; set; }
        public string Adress { get; set; }
        public byte[] Image { get; set; }
        public string NumberPhone { get; set; }
        public string timeJob { get; set; }
        public string Services { get; set; }
        public string IdJob { get; set; }
        public StatusWorkDoctor StatusServiceDoctor { get; set; }
    }
}
