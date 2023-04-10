using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SecretaryCabinetInformationDto
    {
        public CabinetInformationAppointmentDto CabinetInformation { get; set; }
        public List<DoctorInformationAppointmentDto> ListDoctorInformation { get; set; }

    }
}
