using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UpdateOrdreMedicalDto
    {
        public string OrdreMedicalId { get; set; }
        public string CabinetId { get; set; }
        public string DoctorId { get; set; }
        public StatusOrdreMedicalDto  StatusOrdreMedicalToUpdate  {get;  set;}
        
    }
}
