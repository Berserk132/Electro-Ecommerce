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
    public class ManufacturersController : MainController
    {
        private readonly IManufactureService service;

        public ManufacturersController(IManufactureService _service, ShoppingCart shoppingCart) : base(shoppingCart)
        {
            service = _service;
        }

        // GET: Manufacturers
        public IActionResult Index()
        {
            return View(service.GetAll().ToList());
        }

        // GET: Manufacturers/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = service.GetById(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                service.Add(manufacturer);
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = service.GetById(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Manufacturer manufacturer)
        {
            if (id != manufacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Delete(id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturerExists(manufacturer.Id))
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
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = service.GetById(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var manufacturer = service.GetById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturerExists(int id)
        {
            return service.GetById(id) != null;
        }
    }
}
