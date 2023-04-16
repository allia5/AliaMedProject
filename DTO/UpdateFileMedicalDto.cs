using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UpdateFileMedicalDto
    {
        
        public string AppointmentId { get; set; }
        
        public string FileId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Sexe Sexe { get; set; }
        public List<chronicDiseasesDto> ChronicDiseases { get; set; }
    }
}
