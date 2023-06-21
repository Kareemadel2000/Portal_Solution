using System.ComponentModel.DataAnnotations;

namespace Portal.Application.Dtos
{
    public class RoleVM
    {
        [Required]
        public string Name { get; set; }
    }
}
