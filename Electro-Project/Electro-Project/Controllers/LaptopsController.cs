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
using Electro_Project.Controllers.BaseController;
using Electro_Project.Models.Cart;

namespace Electro_Project.Controllers
{
    public class LaptopsController : MainController
    {

        private ILaptopService service { get; set; }
        private IManufactureService manufacturerService { get; set; }

        public LaptopsController(ILaptopService _service, IManufactureService _manufacturerService, ShoppingCart shoppingCart) : base(shoppingCart)
        {
            service = _service;
            manufacturerService = _manufacturerService;
        }

        // GET: Laptops
        public IActionResult Index()
        {
            var shopContext = service.GetAll();
            return View(shopContext.ToList());
        }

        // GET: Laptops/Details/5
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
        public async Task<IActionResult> Create([Bind("Ram,RamType,GPU,OS,Color,ScreenSize,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                service.Add(laptop);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerID"] = new SelectList(manufacturerService.GetAll(), "Id", "Id", laptop.ManufacturerID);
            return View(laptop);
        }

        // GET: Laptops/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("Ram,RamType,GPU,OS,Color,ScreenSize,CPU,Width,Height,Thickness,Weight,Id,Name,Price,ManufacturerID,Warranty,UnitInStock,Description")] Laptop laptop)
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
        public IActionResult DeleteConfirmed(int id)
        {

            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopExists(int id)
        {
            return service.GetById(id) != null;
        }
    }
}
