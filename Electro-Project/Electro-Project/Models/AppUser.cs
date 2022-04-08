using Electro_Project.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_Project.Areas.Identity.Data
{
    public class AppUser: IdentityUser
    {
        //[Required, DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
        //[Required, DataType(DataType.Password)]
        //public string Password { get; set; }
        //[Required, DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }

        public string Neckname { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public int? WishListId { get; set; }

        [ForeignKey("WishListId")]
        public WishList? wishList { get; set; }   
    }
}
