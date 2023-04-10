using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RegistreAccountDto : LoginAccountDto
    {
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmePassword { get; set; }
        [Required(ErrorMessage = "LastName Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "FirstName Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field Is Required")]
        public Sexe Sexe { get; set; }

        [Required(ErrorMessage = "DateOFBirth Is Required")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "NationalNumber Is Required")]
        [StringLength(18)]
        public string NationalNumber { get; set; }
        [Required(ErrorMessage = "PhoneNumber Is Required")]
        public string PhoneNumber { get; set; }

    }
    public enum Sexe
    {
        Male = 1,
        woman = 2
    }
}
