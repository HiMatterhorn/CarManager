using AmiFlota.Data;
using AmiFlota.Models;
using System.Collections.Generic;

namespace AmiFlota.Services
{
    public class CarService : ICarService
    {

        private readonly AmiFlotaContext _db;

        public CarService(AmiFlotaContext db)
        {
            _db = db;
        }
        public IEnumerable<CarModel> GetAllCars()
        {
            IEnumerable<CarModel> cars = _db.Cars;
            return cars;
        }
    }
}
