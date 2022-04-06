using Electro_Project.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_Project.Models
{
    public class WishList
    {
       [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public List<Product> Products { get; set; }

    }
}
