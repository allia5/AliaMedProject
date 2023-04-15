using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FileMedicalMainPatientDto
    {
        public PatientInformationDto MainUser { get; set; }   
        public List<FileMedicalPatientDto> fileMedicals { get; set; }   

    }
    public class FileMedicalPatientDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sexe Sexe { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int CountOrderMedical { get; set; }
        public DoctorInformationDto Doctor { get; set; }
       public List<chronicDiseasesDto> chronicDiseases { get; set; }


    }
    public class chronicDiseasesDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
