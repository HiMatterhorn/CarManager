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
using AmiFlota.Contracts;

namespace AmiFlota.Models
{
    public class CarController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICarService _carService;
        private readonly ITripService _tripService;

        public CarController(ICarService carService, ITripService tripService, IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _tripService = tripService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult CarList()
        {
            IEnumerable<CarModel> cars = _carService.GetAllCars();

            return View(cars);
        }

        public async Task<IActionResult> _CarDetailsModal(string vin)
        {
            var car = await _carService.GetCarByVIN(vin);
            var trips = _tripService.TripsHistoryByCarVin(vin);
            CarDetailsVM viewModel = new CarDetailsVM
            {
                Car = car,
                TripsHistory = trips
            };
            return PartialView("_CarDetailsModal", viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string photoName = "default.jpg";

                //TODO Multiple photos - separate table with reference
                //TODO Create folder when doesn't exist

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
                    HorsePower = viewModel.HorsePower,
                    Engine = viewModel.Engine,
                    TyreSize = viewModel.TyreSize,
                    Fuel = viewModel.Fuel,
                    CardPin = viewModel.CardPin,
                    Insurance = viewModel.Insurance,
                    TechnicalReview = viewModel.TechnicalReview,
                    PhotoPath = photoName
                };
                await _carService.AddCar(carModel);
                return RedirectToAction("CarList", "Car");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string vin)
        {
            CarModel viewModel = await _carService.GetCarByVIN(vin);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                await _carService.UpdateCar(carModel);
                return RedirectToAction("CarList", "Car");
            }

            return View(carModel);
        }

        public async Task<IActionResult> Delete(string vin)
        {
            CarModel carModel = await _carService.GetCarByVIN(vin);
            return View(carModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string vin)
        {
            await _carService.DeleteCar(vin);
            return RedirectToAction("CarList", "Car");
        }

    }
}
