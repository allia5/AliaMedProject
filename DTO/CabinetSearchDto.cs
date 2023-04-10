using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CabinetSearchDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Services { get; set; }
        public string Adress { get; set; }
        public byte[] Image { get; set; }
        public JobSearchDto JobSearchDto { get; set; }
    }
}
