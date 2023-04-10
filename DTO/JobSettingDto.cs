using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobSettingDto
    {
        public string IdJob { get; set; }
        public DateTime startTime { get; set; }
        public DateTime EndTime { get; set; }
        public int processingMinutes { get; set; }
        public StatusServiceDoctor statusService { get; set; }
        public StatusReservationDoctor StatusReservation { get; set; }
        public int NumberPatientAccepted { get; set; }

    }
    public enum StatusReservationDoctor
    {
        Ready = 1,
        NotReady = 0
    }
    public enum StatusServiceDoctor
    {
        InService = 1,
        OutService = 0
    }

}
