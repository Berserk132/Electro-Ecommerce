using Electro_Project.Models.Cart;
using Electro_Project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Electro_Project.Controllers.BaseController
{
    public class MainController : Controller
    {
        private readonly ShoppingCart ShoppingCart;

        public MainController(ShoppingCart _shoppingCart)
        {
            ShoppingCart = _shoppingCart;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            ViewBag.ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCart = ShoppingCart,
                ShoppingCartTotal = ShoppingCart.GetShoppingCartTotal(),
            };



            base.OnActionExecuting(context);
        }

    }
}
