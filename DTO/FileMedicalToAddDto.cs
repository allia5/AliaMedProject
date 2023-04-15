using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FileMedicalToAddDto
    {
        public string IdAppointment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sexe Sexe { get; set; }
        public List<chronicDiseasesDto> chronicDiseases { get; set; }

    }
}
