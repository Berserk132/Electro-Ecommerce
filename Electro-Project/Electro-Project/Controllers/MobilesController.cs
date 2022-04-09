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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Electro_Project.Areas.Identity.Data;
using System.Security.Claims;
using Electro_Project.Helpers.Pagging;


namespace Electro_Project.Controllers
{
    public class MobilesController : MainController
    {
        private readonly ShopContext _context;
        private IMobileService service;
        private IManufactureService manufacturerService { get; set; }

        private IWebHostEnvironment hostingEnvironment { get; set; }
        private IMediaService mediaService { get; set; }


        public MobilesController(IMobileService _mobileService, IManufactureService _manufacturerService, ShoppingCart shoppingCart, IWebHostEnvironment _hostingEnvironment, IMediaService _mediaService, IWishListService _wishListService, UserManager<AppUser> _userManager) : base(shoppingCart, _userManager, _wishListService)

        {
            service = _mobileService;
            manufacturerService = _manufacturerService;
            hostingEnvironment = _hostingEnvironment;
            mediaService = _mediaService;

        }

        // GET: Mobiles
        public async Task<IActionResult> Index()
        {
        
            var mobiles = service.GetAll();
            paginatedList = PaginatedList<Mobile>.Create(mobiles, PageIndex, PageSize);
            return View(paginatedList);
        }

        public IActionResult IncreamentIndex()
        {
            PageIndex += 1;

            return RedirectToAction("Index");
        }

        public IActionResult DecreamentIndex()
        {
            PageIndex -= 1;

            return RedirectToAction("Index");

        }

        // GET: Mobiles/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var mobile = service.GetById(id);

            if (mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        // GET: Mobiles/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Ram,GPU,OS,Color,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Mobile mobile, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {

                service.Add(mobile);

                #region ImageToFile
                foreach (IFormFile file in files)
                {
                    mediaService.Add(new Media() { ImageURL = file.FileName, ProductID = mobile.Id }, file);
                }
                #endregion
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", mobile.ManufacturerID);
            return View(mobile);
        }

        // GET: Mobiles/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobile = service.GetById(id);
            if (mobile == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", mobile.ManufacturerID);
            return View(mobile);
        }

        // POST: Mobiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Ram,GPU,OS,Color,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Mobile mobile, List<IFormFile> files)
        {
            if (id != mobile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(id, mobile);
                    foreach (IFormFile file in files)
                    {
                        mediaService.Add(new Media() { ImageURL = file.FileName, ProductID = mobile.Id }, file);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileExists(mobile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", mobile.ManufacturerID);
            return View(mobile);
        }

        // GET: Mobiles/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobile = service.GetById(id);

            if (mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        // POST: Mobiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MobileExists(int id)
        {
            return service.GetById(id) != null;
        }

        List<Mobile> matched;
        public IActionResult Filter(decimal pricemin, decimal pricemax, List<string> OS, List<string> Ram, List<string> Color, string Sort)
        {
            var mobiles = service.GetAll().Where(C => C.Price < pricemax && C.Price > pricemin);
            matched = new List<Mobile>();
            foreach (var item in mobiles)
            {
                if ((OS.Count > 0 ? OS : new List<string>() { item.OS.ToString() }).Contains(item.OS.ToString()))
                    if ((Ram.Count > 0 ? Ram : new List<string>() { item.Ram.ToString() }).Contains(item.Ram.ToString()))
                        if ((Color.Count > 0 ? Color : new List<string>() { item.Color.ToString() }).Contains(item.Color.ToString()))
                            matched.Add(item);
            }

            if (Sort == "Low Price to High")
            {
                matched = matched.OrderBy(C => C.Price).ToList();
            }
            else if (Sort == "High Price to Low")
            {
                matched = matched.OrderByDescending(C => C.Price).ToList();

            }
            else if (Sort == "Popularity")
            {
                matched = matched.OrderByDescending(C => C.Reviews.Count > 0 ? (C.Reviews.Sum(R => R.starsCount)) / C.Reviews.Count : 0).ToList();
            }

            ViewBag.Min = pricemin;
            ViewBag.Max = pricemax;
            ViewBag.OS = OS;
            ViewBag.Ram = Ram;
            ViewBag.Color = Color;

            paginatedList = PaginatedList<Mobile>.Create(mobiles, PageIndex, PageSize);

            return View("index", paginatedList);
        }


    }
}
