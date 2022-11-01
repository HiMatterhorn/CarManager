using AmiFlota.Data;
using AmiFlota.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
