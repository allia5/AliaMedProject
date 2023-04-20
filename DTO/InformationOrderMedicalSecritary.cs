using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InformationOrderMedicalSecritary
    {
       public InformationOrdreMedical informationOrdreMedical { get; set; }
        public PatientInformationDto PatientInformation { get; set; }
        public InformationFileMedical informationFile { get; set; }
    }
    public class InformationFileMedical
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
        public Sexe sexe { get; set; }
    }
    public class InformationOrdreMedical
    {
        public string Id { get; set;}
        public DateTime DateCreate { get; set; }
        public StatusOrdreMedicalDto statusOrdreMedical { get; set; }
        public string Summary { get; set; }
   
    }
    public enum StatusOrdreMedicalDto
    {
        validate = 1,
        NotValidate = 0
    }
}
