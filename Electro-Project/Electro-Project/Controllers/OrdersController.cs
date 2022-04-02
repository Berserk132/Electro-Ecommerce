using Electro_Project.Controllers.BaseController;
using Electro_Project.Models;
using Electro_Project.Models.Cart;
using Electro_Project.Models.Context;
using Electro_Project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Electro_Project.Controllers
{
    public class OrdersController : MainController
    {
        private readonly ShopContext context;
        private readonly ShoppingCart shoppingCart;

        public OrdersController(ShopContext _context, ShoppingCart _shoppingCart) : base(_shoppingCart)
        {
            context = _context;
            shoppingCart = _shoppingCart;
        }

        public IActionResult FinalizeOrder()
        {

            var response = new CheckoutVM()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal(),
                ShippingAddress = new Address()
            };

            return View(response);
        }

        [HttpPost]
        public bool FinalizeOrder(CheckoutVM ch)
        {

            //var response = new ShoppingCartVM()
            //{
            //    ShoppingCart = shoppingCart,
            //    ShoppingCartTotal = shoppingCart.GetShoppingCartTotal(),
            //};

            //return View(response);

            return true;
        }

        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();

            shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal(),
            };

            return View(response);
        }

        public IActionResult AddItemToShoppingCart(int id)
        {
            var item = context.Products.SingleOrDefault(P => P.Id == id);

            if (item != null)
            {
                shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
