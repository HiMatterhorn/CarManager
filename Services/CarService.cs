using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public class CarService : ICarService
    {

        private readonly AmiFlotaContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarService(AmiFlotaContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IEnumerable<CarModel> GetAllCars()
        {
            IEnumerable<CarModel> cars = _db.Cars;
            return cars;
        }

        public async Task<string> UploadPhoto(IFormFile photoPath)
        {
            string folder = "images/cars/";
            string fileName = Guid.NewGuid().ToString() + "_" + photoPath.FileName;
            string serverPath = Path.Combine(_webHostEnvironment.WebRootPath, folder + fileName);

            await photoPath.CopyToAsync(new FileStream(serverPath, FileMode.Create));
            return fileName;
        }

        public async Task<CarModel> GetCarByVIN(string vin)
        {
            return await _db.Cars.FirstOrDefaultAsync(c => c.VIN.Equals(vin));
        }

        public async Task<int> AddCar(CarModel carModel)
        {
            _db.Cars.Add(carModel);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> UpdateCar(CarModel newData)
        {
            CarModel car = await _db.Cars.FirstOrDefaultAsync(c => c.VIN.Equals(newData.VIN));
            car.VIN = newData.VIN;
            car.RegistrationNumber = newData.RegistrationNumber;
            car.Brand = newData.Brand;
            car.Model = newData.Model;
            car.SeatsNumber = newData.SeatsNumber;
            car.Doors = newData.Doors;
            car.Trunk = newData.Trunk;
            car.HorsePower = newData.HorsePower;
            car.Engine = newData.Engine;
            car.TyreSize = newData.TyreSize;
            car.Fuel = newData.Fuel;
            car.CardPin = newData.CardPin;
            car.Insurance = newData.Insurance;
            car.TechnicalReview = newData.TechnicalReview;
            car.PhotoPath = newData.PhotoPath;

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteCar(string vin)
        {
            var deleteCar = await _db.Cars.FirstOrDefaultAsync(c => c.VIN.Equals(vin));
            _db.Cars.Remove(deleteCar);
            return await _db.SaveChangesAsync();
        }


    }
}
