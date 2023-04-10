using Server.Models.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.UserRoles
{
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public string IdUser { get; set; }
        public User User { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
