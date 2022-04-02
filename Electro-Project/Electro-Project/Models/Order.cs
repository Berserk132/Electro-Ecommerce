using System.ComponentModel.DataAnnotations;

namespace Electro_Project.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public ApplicationUser User { get; set; }

        public Address ShippingAddress { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
