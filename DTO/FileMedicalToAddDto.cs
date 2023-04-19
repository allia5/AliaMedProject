using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FileMedicalToAddDto
    {
        public string IdAppointment { get; set; }
        [Required(ErrorMessage ="Field First Name Is Required")]
        
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Field DateBirth Is Required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Field sexe Is Required")]
        public Sexe Sexe { get; set; }
        public List<chronicDiseasesDto> chronicDiseases { get; set; }

    }
}
