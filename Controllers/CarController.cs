using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmiFlota.Data;
using AmiFlota.Utilities;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AmiFlota.Services;

namespace AmiFlota.Models
{
    public class CarController : Controller
    {
        private readonly AmiFlotaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICarService _carService;

        public CarController(AmiFlotaContext context, IWebHostEnvironment webHostEnvironment, ICarService carService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _carService = carService;
        }

        // GET: CarModels
        public async Task<IActionResult> CarList()
        {
            IEnumerable<CarModel> cars = await _context.Cars.ToListAsync();

            return View(cars);
        }

        // GET: CarModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.Cars
                .FirstOrDefaultAsync(m => m.VIN == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: CarModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string photoName = "default.jpg";

                if (viewModel.PhotoPath != null)
                {
                    photoName = await _carService.UploadPhoto(viewModel.PhotoPath);
                }
                
                CarModel carModel = new CarModel()
                {
                    VIN = viewModel.VIN,
                    RegistrationNumber = viewModel.RegistrationNumber,
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    SeatsNumber = viewModel.SeatsNumber,
                    Trunk = viewModel.Trunk,
                    PhotoPath = photoName
                };
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("CarList", "Car");
            }
            return View(viewModel);
        }

        // GET: CarModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.Cars.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VIN,RegistrationNumber,Brand,Model,SeatsNumber,Trunk")] CarModel carModel)
        {
            if (id != carModel.VIN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.VIN))
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
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.Cars
                .FirstOrDefaultAsync(m => m.VIN == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var carModel = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(carModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(string id)
        {
            return _context.Cars.Any(e => e.VIN == id);
        }
    }
}
