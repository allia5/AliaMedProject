using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PatientAppoimentInformationDto
    {
        public string Id { get; set; }
        public DateTime DateAppoiment { get; set; }
        public int AppoimentCount { get; set; }
        public StatusPlaningDto Status { get; set; }

    }
    public enum StatusPlaningDto
    {
        absent = -1,
        Still = 0,
        Treated = 1,
        passed = 2,
        Delayed = 3
    }
}
