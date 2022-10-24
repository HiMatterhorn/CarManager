using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public class TripService:ITripService
    {

        private readonly AmiFlotaContext _db;
        private readonly UserManager<ApplicationUserModel> _userManager;

        public TripService(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<int> CreateInspection(TripVM tripVM)
        {
            TripModel tripModel = new TripModel()
            {
            //TODO Add car inspection
               Id = tripVM.Id,
               StartKm = tripVM.StartKm,
               EndKm = tripVM.EndKm, 
               Cost = tripVM.Cost,
               CostRemarks = tripVM.CostRemarks,
               Active = tripVM.Active,
            };

            //TODO Validate new inspection
           
            //Save to database
                _db.Trips.Add(tripModel);
            return await _db.SaveChangesAsync();
        }
    }
}
