using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DoctorInformationAppointmentDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Specialities { get; set; }
        public Sexe Sexe { get; set; }
        public int NumberPatientAccepted { get; set; }
        public DateTime TimeReady { get; set; }
        public DateTime TimeEnd { get; set; }
    }
}
