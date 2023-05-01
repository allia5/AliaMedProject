using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{


    public class MedicalFileArchivePatientDto
    {
        public InformationFileMedical informationFileMedical { get; set; }
        public List<MedicalOrdresPatientDto> medicalOrdres { get; set; }

    }
    public class MedicalOrdresPatientDto
    {
        public DoctorInformationDto doctorInformation { get; set; }
        public MedicalOrdreDetails medicalOrdreDetails { get; set; }
        public List<AdviceMedicalDto> adviceMedicalsDto { get; set; }

    }




    public class AdviceMedicalDto
    {
        public string Id { get; set; }
        public string FullNameSender { get; set; }
        public string FullNameReceiver { get; set; }
        public string Message { get; set; }
        public DateTime DateSend { get; set; }
        public StatusViewReceiverDto StatusViewReceiver { get; set; }

    }
    public enum StatusViewReceiverDto
    {
        WatchIt = 1,
        NotWatchIt = 0
    }
}
