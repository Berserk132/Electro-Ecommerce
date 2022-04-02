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

namespace Electro_Project.Controllers
{
    public class ProductsController : MainController
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context, ShoppingCart shoppingCart) : base(shoppingCart)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            ViewBag.products = _context.Products.Include(p => p.Manufacturer).ToList();
            //ViewBag.mobilesContext = _context.Mobiles.Include(p => p.Manufacturer);
            //ViewBag.laptopsContext = _context.Laptops.Include(p => p.Manufacturer);
            //return View(await shopContext.ToListAsync());
            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var laptop = _context.Laptops.FirstOrDefault(p => p.Id == id);
            if (laptop == null)
            {
                var mobile = _context.Mobiles.FirstOrDefault(p => p.Id == id);
                if (mobile != null)
                {
                    return View(mobile);
                }
                ///error
            }
            else
                return View(laptop);



            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

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
    }
}
