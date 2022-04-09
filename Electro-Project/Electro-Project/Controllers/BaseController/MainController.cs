using Electro_Project.Areas.Identity.Data;
using Electro_Project.Models.Cart;
using Electro_Project.Models.Services;
using Electro_Project.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Electro_Project.Controllers.BaseController
{
    public class MainController : Controller
    {
        private readonly ShoppingCart ShoppingCart;
        protected IWishListService wishListService { get; set; }
        protected readonly UserManager<AppUser> userManager;
        protected ClaimsIdentity claimsIdentity { get; set; }

        public MainController(ShoppingCart _shoppingCart, UserManager<AppUser> _userManager, IWishListService _wishListService)
        {
            ShoppingCart = _shoppingCart;
            userManager = _userManager;
            wishListService = _wishListService;
        }

        public  override void OnActionExecuting(ActionExecutingContext context)
        {

            ViewBag.ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCart = ShoppingCart,
                ShoppingCartTotal = ShoppingCart.GetShoppingCartTotal(),
            };

          //  var user = await userManager.GetUserAsync(User);
            try
            {
                claimsIdentity = User.Identity as ClaimsIdentity;
                string UserID = claimsIdentity.Claims.First().ToString().Split("nameidentifier: ")[1];
                ViewBag.WishList = (wishListService.GetByUserId(UserID)).Select(w => w.PID).ToList();
            }
            catch (Exception e)
            {
                ViewBag.WishList = new List<int>();
            }




            base.OnActionExecuting(context);
        }

    }
}
