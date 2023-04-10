using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AppointmentInformationDto
    {
        public string Id { get; set; }
        public DateTime DateAppoiment { get; set; }
        public int CountOfPatient { get; set; }
        public DoctorInformationAppointmentDto DoctorInformation { get; set; }
        public CabinetInformationAppointmentDto CabinetInformation { get; set; }
    }
}
