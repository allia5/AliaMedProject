using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CabinetInformationAppointmentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Services { get; set; }
        public string Adress { get; set; }
        public string AdressMap { get; set; }
        public byte[] Image { get; set; }
        public string NumberPhone { get; set; }

    }
}
