using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PatientInformationDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sexe Sexe { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalNumber { get; set; }
        public string NumberPhone { get; set; }

    }
}
