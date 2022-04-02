using System.ComponentModel.DataAnnotations;

namespace Electro_Project.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }

        public Product Product { get; set; }

        public decimal getTotal()
		{
            return Product.Price * Amount;
		}
    }
}
