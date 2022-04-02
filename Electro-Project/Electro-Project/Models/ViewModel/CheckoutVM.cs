using Electro_Project.Models.Cart;

namespace Electro_Project.Models.ViewModel
{
	public class CheckoutVM
	{
		public ShoppingCart ShoppingCart { get; set; }

		public double ShoppingCartTotal { get; set; }

		public Address ShippingAddress { get; set; }
	}
}
