using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobSearchDto
    {
        public string Id { get; set; }
        public DateTime ReadyTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumberPatientAvailble { get; set; }
    }
}
