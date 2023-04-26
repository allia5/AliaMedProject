using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InformationPrescriptionResultDto
    {
        public PatientInformationDto PatientInformation { get; set; }
        public DoctorInformationDto DoctorInformation { get; set; }
        public FileMedicalInformation FileMedicalInformation { get; set; }
        public PrescriptionInfromationDto prescriptionInfromationDto { get; set; }
    }
    public class PrescriptionInfromationDto
    {
        public string IdPrescription { get; set; }
        public string Instruction { get; set; }
        public DateTime DateRelease { get; set; }
        public List<LinePrescriptionDto> linePrescriptionDtos { get; set; }
    }
    public class LinePrescriptionDto
    {
        public string IdLine { get; set; }
        public string MedicamentName { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
    }

}
