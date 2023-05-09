
using System.ComponentModel.DataAnnotations;


namespace Portal.Application.Dtos
{
    public  class LoginVM
    {
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "invalid email")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MinLength(10, ErrorMessage = "Min Len 10")]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
