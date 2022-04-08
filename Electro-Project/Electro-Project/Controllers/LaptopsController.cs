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
using Electro_Project.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Electro_Project.Controllers.BaseController;
using Electro_Project.Models.Cart;
using Microsoft.AspNetCore.Hosting;

namespace Electro_Project.Controllers
{
    //[Authorize]
    public class LaptopsController : MainController
    {

        private ILaptopService service { get; set; }
        private IManufactureService manufacturerService { get; set; }
        private IMediaService mediaService { get; set; }

        private IWebHostEnvironment hostingEnvironment { get; set; }
        public LaptopsController(ILaptopService _service, IManufactureService _manufacturerService, IMediaService _mediaService, ShoppingCart shoppingCart, IWebHostEnvironment _hostingEnvironment) : base(shoppingCart)
        {
            service = _service;
            manufacturerService = _manufacturerService;
            mediaService = _mediaService;
            hostingEnvironment = _hostingEnvironment;
        }

        // GET: Laptops
        public IActionResult Index()
        {
            var shopContext = service.GetAll();
            return View(shopContext.ToList());
        }

        // GET: Laptops/Details/5
        //[AllowAnonymous]
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = service.GetById(id);

            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        //GET: Laptops/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Ram,RamType,GPU,OS,Color,ScreenSize,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Laptop laptop, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {

                service.Add(laptop);
                #region ImageToFile
                foreach (IFormFile file in files)
                {
                    mediaService.Add(new Media() { ImageURL = file.FileName, ProductID = laptop.Id }, file);
                }
                #endregion

                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", laptop.ManufacturerID);
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = service.GetById(id);
            if (laptop == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", laptop.ManufacturerID);
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Ram,RamType,GPU,OS,Color,ScreenSize,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Laptop laptop, List<IFormFile> files)
        {
            if (id != laptop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(id, laptop);
                    foreach (IFormFile file in files)
                    {
                        mediaService.Add(new Media() { ImageURL = file.FileName, ProductID = laptop.Id }, file);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopExists(laptop.Id))
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
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", laptop.ManufacturerID);
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = service.GetById(id);

            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {

            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        private bool LaptopExists(int id)
        {
            return service.GetById(id) != null;
        }
        //Queue Sheblyaat

        [HttpPost]
        public IActionResult Filter(decimal pricemin, decimal pricemax, List<string> GPU, List<string> OS, List<string> Ram, List<string> RamType, List<string> Screen, List<string> Color, string Sort)
        {
            var shopContext = service.GetAll().Where(C => C.Price < pricemax && C.Price > pricemin);
            List<Laptop> matched = new List<Laptop>();
            //Filter
            foreach (var item in shopContext)
            {
                if ((OS.Count > 0 ? OS : new List<string>() { item.OS.ToString() }).Contains(item.OS.ToString()))
                    if ((GPU.Count > 0 ? GPU : new List<string>() { item.GPU.ToString() }).Contains(item.GPU.ToString()))
                        if ((Ram.Count > 0 ? Ram : new List<string>() { item.Ram.ToString() }).Contains(item.Ram.ToString()))
                            if ((Color.Count > 0 ? Color : new List<string>() { item.Color.ToString() }).Contains(item.Color.ToString()))
                                if ((RamType.Count > 0 ? RamType : new List<string>() { item.RamType.ToString() }).Contains(item.RamType.ToString()))
                                    if ((Screen.Count > 0 ? Screen : new List<string>() { item.ScreenSize.ToString() }).Contains(item.ScreenSize.ToString()))
                                        matched.Add(item);
            }
            //Sort
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
            ViewBag.GPU = GPU;
            ViewBag.Color = Color;
            ViewBag.Screen = Screen;
            ViewBag.RamType = RamType;
            return View("Index", matched);
        }


    }
}
