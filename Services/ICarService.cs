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
    }
}
