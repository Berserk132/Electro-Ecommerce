using Electro_Project.Areas.Identity.Data;
using Electro_Project.Controllers.BaseController;
using Electro_Project.Models;
using Electro_Project.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Electro_Project.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class RoleController : MainController
    {
        public RoleManager<IdentityRole> RoleManager { get; }

        public RoleController(RoleManager<IdentityRole> _roleManager, ShoppingCart shoppingCart) : base(shoppingCart)
        {
            RoleManager = _roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole() { Name = roleViewModel.RoleName };
                IdentityResult result = await RoleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return RedirectToAction("index", "products");
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View();
        }
    }
}
