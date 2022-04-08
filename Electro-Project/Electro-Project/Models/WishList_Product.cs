using Electro_Project.Areas.Identity.Data;
using Electro_Project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_Project
{
    public class WishList_Product
    {
        [ForeignKey("AppUser")]
        public string UserID { get; set; }

        public AppUser AppUser { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey("Product")]
        public int PID { get; set; }
    }
}
