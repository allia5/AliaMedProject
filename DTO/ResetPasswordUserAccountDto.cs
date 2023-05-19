using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ResetPasswordUserAccountDto
    {
        [Required(ErrorMessage = " password is required.")]
        [DataType(DataType.Password)]
        [MaxLength(25)]
        [MinLength(5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmePassword { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
