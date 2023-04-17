using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class OrdreMedicalDto
    {
        public string OrdreId { get; set; }
        public string Summary { get; set; }
        public byte[] ResultFileMedicalAnalyse { get; set; }
        public byte[] ResultFileMedicalRadio { get; set; }
        public List<PrescriptionLine> Lines { get; set; }
        public DoctorInformationDto DoctorInformation { get; set; }
        public PatientInformationDto PatientInformation { get; set; }
        public CabinetInformationAppointmentDto cabinetInformation { get; set; }
    }
}
