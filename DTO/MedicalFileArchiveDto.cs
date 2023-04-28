using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MedicalFileArchiveDto
    {
       public  InformationFileMedical informationFileMedical { get; set; }
        public List<MedicalOrdresDto> medicalOrdres { get; set; }

    }
    public class MedicalOrdresDto
    {
        DoctorInformationDto doctorInformation { get; set; }    
        public MedicalOrdreDetails medicalOrdreDetails { get; set; }


    }

    public class MedicalOrdreDetails
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public DateTime DateValidation { get; set; }
        public List<PrescriptionLineInformationDto> prescriptionLinesInformation { get; set; }
        public List<RadioLineInformationDto>  radioLinesInformation { get; set; }
        public List<AnalyseLineInformationDto> analyseLinesInformation { get; set; }
    }
    public class PrescriptionLineInformationDto
    {
        public string IdLinePrescription { get; set; }
        public string MedicamentName { get; set; }
        public int Quantity { get; set; }
        public StatusPrescriptionLineDto statusPrescriptionLineDto { get; set; }
        public DateTime DateValidation { get; set; }
    }
    public class RadioLineInformationDto
    {
        public string IdLineRadio { get; set; }
        public string Description { get; set; }
        public StatusRadioDto statusRadio { get; set; }

    }
    public class AnalyseLineInformationDto
    {
        public string IdLineAnalyse { get; set; }
        public string Description { get; set; }
        public StatusAnalyseDto statusAnalyse { get; set; }
    }


    public enum StatusPrescriptionLineDto
    {
        Validate = 1,
        NotValidate = 0
    }
    public enum StatusRadioDto
    {
        validate = 1,
        notValidate = 0
    }
    public enum StatusAnalyseDto
    {
        validate = 1,
        notValidate = 0
    }
}
