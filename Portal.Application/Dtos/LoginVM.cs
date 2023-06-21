
using System.ComponentModel.DataAnnotations;


namespace Portal.Application.Dtos
{
    public  class LoginVM
    {
      
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
