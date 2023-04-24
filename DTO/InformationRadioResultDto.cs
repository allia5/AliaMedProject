using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InformationRadioResultDto
    {
        public PatientInformationDto PatientInformation { get; set; }
        public DoctorInformationDto DoctorInformation { get; set; } 
        public FileMedicalInformation FileMedicalInformation { get; set; }
        public RadioInformation RadioInformation { get; set; }


    }
    public class FileMedicalInformation
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sexe Sexe { get; set; }
      
        public List<string> chronicDiseases { get; set; }
    }
    public class RadioInformation
    {
        public string Id { get; set; }
        public List<LineRadioMedicalResultDto> linesRadioMedicals { get; set; }
      
    }
    public class LineRadioMedicalResultDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
       
        public string Instruction { get; set; }
    }
}
