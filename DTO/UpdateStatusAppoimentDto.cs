using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UpdateStatusAppoimentDto
    {
        public string Id { get; set; }
        public StatusPlaningDto statusPlaningDto { get; set; }
    }
    public class DelayeAppoimentMedical :UpdateStatusAppoimentDto
    {
        public DateTime DateAppoiment { get; set; }
    }
}
