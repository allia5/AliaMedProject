using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KeysAppoimentInformationDoctor
    {
        public string CabinetId { get; set; }
        public DateTime DateAppoiment { get; set; }

    }
    public class KeysAppoimentInformationSecretary: KeysAppoimentInformationDoctor
    {
        public string IdDoctor { get;  set; }

    }
}
