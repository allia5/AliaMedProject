using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InformationAnalyseResultDto
    {
        public PatientInformationDto PatientInformation { get; set; }
        public DoctorInformationDto DoctorInformation { get; set; }
        public FileMedicalInformation FileMedicalInformation { get; set; }

    }
    public class InformationAnalyseDto
        {
        public string IdAnalyse { get; set; }   
        public List<LinesAnalyseDto> LinesAnalyse { get; set; }

        }
    public class LinesAnalyseDto
    {
        public string IdLineAnalyse { get;  set; }
        public string Instruction { get; set; }
        public string Description { get; set; }
}
