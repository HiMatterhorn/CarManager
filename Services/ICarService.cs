using AmiFlota.Models;
using System.Collections.Generic;

namespace AmiFlota.Services
{
    public interface ICarService
    {
        public IEnumerable<CarModel> GetAllCars();
    }
}
