using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MedicalAdviceDoctorDto
    {
        public InformationFileMedical FileMedicalInformation { get; set; }
        public MedicalOrdreDetails MedicalOrdreDetails { get; set;}
        public int CountMessageNotViewed { get; set; }
    }
}
