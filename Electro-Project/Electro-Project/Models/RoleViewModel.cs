using System.ComponentModel.DataAnnotations;

namespace Electro_Project.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }

    }
}
