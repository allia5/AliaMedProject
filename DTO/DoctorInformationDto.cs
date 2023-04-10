using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DoctorInformationDto
    {

        public string IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sexe Sexe { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public List<string> Specialities { get; set; }

    }
}
