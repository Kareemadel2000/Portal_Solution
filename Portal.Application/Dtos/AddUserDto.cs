using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Dtos
{
    public class AddUserDto
    {
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "invalid email")]

        public string Email { get; set; }

        [Required(ErrorMessage = "User Name Required")]
        [MaxLength(50, ErrorMessage = "Max Len 50")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [Compare("Password", ErrorMessage = "Password not Match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Role Is Required")]
        public string RoleId { get; set; }

    }
}
