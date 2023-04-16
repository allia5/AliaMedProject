using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UpdateFileMedicalDto
    {
        public string AppointmentId { get; set; }
        public string FileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sexe Sexe { get; set; }
        public List<chronicDiseasesDto> ChronicDiseases { get; set; }
    }
}
