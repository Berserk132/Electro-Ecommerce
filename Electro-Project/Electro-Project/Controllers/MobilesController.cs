﻿#nullable disable
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

namespace Electro_Project.Controllers
{
    public class MobilesController : MainController
    {
        private readonly ShopContext _context;
        private IMobileService service;
        private IManufactureService manufacturerService { get; set; }

        private IWebHostEnvironment hostingEnvironment { get; set; }
        private IMediaService mediaService { get; set; }

        public MobilesController(IMobileService _mobileService, IManufactureService _manufacturerService, ShoppingCart shoppingCart, IWebHostEnvironment _hostingEnvironment, IMediaService _mediaService) : base(shoppingCart)
        {
            service = _mobileService;
            manufacturerService = _manufacturerService;
            hostingEnvironment = _hostingEnvironment;
            mediaService = _mediaService;

        }

        // GET: Mobiles
        public IActionResult Index()
        {
            var shopContext = service.GetAll();
            return View(shopContext.ToList());
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
        public async Task<IActionResult> Create([Bind("Ram,GPU,OS,Color,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Mobile mobile, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {

                service.Add(mobile);

                #region ImageToFile
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                foreach (IFormFile file in files)
                {
                    if (file.Length > 0)
                    {
                        string filePath = Path.Combine(uploads, file.FileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        mediaService.Add(new Media() { ImageURL = file.FileName, ProductID = mobile.Id });
                    }
                }
                #endregion
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", mobile.ManufacturerID);
            return View(mobile);
        }

        // GET: Mobiles/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("Ram,GPU,OS,Color,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Mobile mobile)
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
                    await _context.SaveChangesAsync();
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
        public IActionResult Filter(decimal pricemin, decimal pricemax, List<string> OS, List<string> Ram, List<string> Color,string Sort)
        {
            var shopContext = service.GetAll().Where(C => C.Price < pricemax && C.Price > pricemin);
            matched = new List<Mobile>();
            foreach (var item in shopContext)
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
            { }

            ViewBag.Min = pricemin;
            ViewBag.Max = pricemax;
            ViewBag.OS = OS;
            ViewBag.Ram = Ram;
            ViewBag.Color = Color;
            return View("index", matched);
        }

     
    }
}
