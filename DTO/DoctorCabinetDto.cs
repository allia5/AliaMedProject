using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DoctorCabinetDto
    {
        public string IdDoc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public StatusWorkDoctor StatusWork { get; set; }
        public List<string> Specialities { get; set; }
        public JobSettingDto JobSettingDto { get; set; }

    }
}
