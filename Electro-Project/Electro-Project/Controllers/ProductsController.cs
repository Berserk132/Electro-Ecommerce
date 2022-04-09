#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electro_Project.Models;
using Electro_Project.Models.Context;
using Electro_Project.Controllers.BaseController;
using Electro_Project.Models.Cart;
using Electro_Project.Models.Services;
using Microsoft.AspNetCore.Identity;
using Electro_Project.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Electro_Project.Controllers
{
    public class ProductsController : MainController
    {
        private IProductService productService;
        private readonly IReviewService reviewService;
        private IMediaService mediaService { get; set; }

        public ProductsController(IReviewService _reviewService, IProductService _productService, IMediaService _mediaService, ShoppingCart shoppingCart, IWishListService _wishListService, UserManager<AppUser> _userManager) : base(shoppingCart, _userManager, _wishListService)
        {
            reviewService = _reviewService;
            mediaService = _mediaService;
            productService = _productService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            ViewBag.products = productService.GetAll();


            return View();
        }


        public async Task<IActionResult> Search(string search, string category)
        {
            var products = productService.GetAll().Where(p => p.Name.Contains(search??"")).ToList();


            var controllerName = category;
            if (category == null)
                controllerName = "Laptops";

            return RedirectToAction("Index", controllerName, products);
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = productService.GetById(id);

            var controllerName = product.GetType().ToString().Split(".")[2] + "s";

            return RedirectToAction("Details", controllerName, new { id = product.Id });
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteImg(int id)
        {
            var media = mediaService.GetById(id);
            mediaService.Delete(id);

            var prodcut = productService.GetById(id);

            var controllerName = prodcut.GetType().ToString().Split(".")[2] + "s";
            return RedirectToAction("Edit", controllerName, new { id = prodcut.Id });
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(Review review)
        {

            var product = productService.GetById(review.ProductId);

            var user = await userManager.GetUserAsync(User);

            review.User = user;
            review.Product = product;

            product.Reviews.Add(review);

            var controllerName = product.GetType().ToString().Split(".")[2] + "s";

            return RedirectToAction("Details", controllerName, new { id = product.Id });
        }

        [Authorize]
        public IActionResult AddToWishList(int id)
        {
            claimsIdentity = User.Identity as ClaimsIdentity;
            string UID = claimsIdentity.Claims.First().ToString().Split("nameidentifier: ")[1];

            wishListService.AddToWishList(id, UID);

            Product product = productService.GetById(id);

            var url = HttpContext.Request.Headers["Referer"];
            if (url[0].Contains("Identity"))
                return RedirectToAction("Index");


            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
        [Authorize]
        public IActionResult RemoveFromWishList(int id)
        {
            claimsIdentity = User.Identity as ClaimsIdentity;
            string UID = claimsIdentity.Claims.First().ToString().Split("nameidentifier: ")[1];

            wishListService.RemoveFromWishList(id, UID);

            Product product = productService.GetById(id);

            var url = HttpContext.Request.Headers["Referer"];
            if (url[0].Contains("Identity"))
                return RedirectToAction("Index");


            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        [Authorize]
        public IActionResult WishList(int id)
        {
            claimsIdentity = User.Identity as ClaimsIdentity;
            string UserID = claimsIdentity.Claims.First().ToString().Split("nameidentifier: ")[1];


            var wishLists = wishListService.GetByUserId(UserID).ToList();

            return View("WishList",wishLists);
        }


        // GET: Laptops/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {

            productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //// GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Product product = productService.GetById(id);

            string controller = product.GetType().ToString().Split(".")[2] + "s";

            return RedirectToAction("Edit", controller, new { area = "" });
        }



        #region Commented
        //// GET: Products/Create
        //public IActionResult Create()
        //{
        //    ViewData["ManufacturerID"] = new SelectList(_context.Manufacturer, "Id", "Id");
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ManufacturerID"] = new SelectList(_context.Manufacturer, "Id", "Id", product.ManufacturerID);
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ManufacturerID"] = new SelectList(_context.Manufacturer, "Id", "Id", product.ManufacturerID);
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ManufacturerID"] = new SelectList(_context.Manufacturer, "Id", "Id", product.ManufacturerID);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Manufacturer)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}

        #endregion
    }
}
