using Electro_Project.Areas.Identity.Data;
using Electro_Project.Controllers.BaseController;
using Electro_Project.Models;
using Electro_Project.Models.Cart;
using Electro_Project.Models.Context;
using Electro_Project.Models.Services;
using Electro_Project.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Electro_Project.Controllers
{
    [Authorize]
    public class OrdersController : MainController
    {
        private readonly ShopContext context;
        private readonly ShoppingCart shoppingCart;
        private readonly UserManager<AppUser> userManager;
        private readonly IOrdersService ordersService;
        private readonly IHttpClientFactory clientFactory;

        public OrdersController(ShopContext _context,
            ShoppingCart _shoppingCart,
            UserManager<AppUser> _userManager,
            IOrdersService _ordersService,
            IHttpClientFactory _clientFactory) : base(_shoppingCart)
        {
            context = _context;
            shoppingCart = _shoppingCart;
            userManager = _userManager;
            ordersService = _ordersService;
            clientFactory = _clientFactory;
        }

        
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            var orders = ordersService.GetOrdersByUserIdAndRole(userId, userEmailAddress);

            return View(orders);
        }
        
        public async Task<IActionResult> FinalizeOrder()
        {

            var user = await userManager.GetUserAsync(User);

            var response = new CheckoutVM()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal(),
                ShippingAddress = new Address()
            };

            return View(response);
        }


        [HttpPost]
        
        public async Task<IActionResult> FinalizeOrder(Address address)
        {


            var items = shoppingCart.GetShoppingCartItems();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            var Address = address;

            ordersService.StoreOrder(items, userId, userEmailAddress, Address);

            await shoppingCart.ClearShoppingCartAsync();

            return RedirectToAction(nameof(ShoppingCart));
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
        
        public IActionResult RemoveItemFromShoppingCart(int id)
        {
            var item = context.Products.SingleOrDefault(P => P.Id == id);

            if (item != null)
            {
                shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
