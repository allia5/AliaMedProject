using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SecritaryDto
    {
        public string IdSecritary { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sexe sexe { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public StatusSecritary StatusSecritary { get; set; }

    }
    public enum StatusSecritary
    {
        Active = 1,
        Block = 0,
        Deleted = -1
    }
}
