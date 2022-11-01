using AmiFlota.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface ICarService
    {
        public IEnumerable<CarModel> GetAllCars();
        public Task<string> UploadPhoto(IFormFile photoPath);
        public Task<CarModel> GetCarByVIN(string vin);
        public Task<int> AddCar(CarModel carModel);

        public Task<int> UpdateCar(CarModel newData);
        public Task<int> DeleteCar(string vin);
    }
}
