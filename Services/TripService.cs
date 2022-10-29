using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public class TripService : ITripService
    {

        private readonly AmiFlotaContext _db;
        private readonly UserManager<ApplicationUserModel> _userManager;

        public TripService(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public void CreateTrip(TripVM tripVM) //ASYNC return?
        {
            TripModel tripModel = new TripModel()
            {
                Id = tripVM.Id,
                StartKm = tripVM.StartKm,
                EndKm = tripVM.EndKm,
                Cost = tripVM.Cost,
                CostRemarks = tripVM.CostRemarks,
                Active = tripVM.Active,
                BookingRefId = tripVM.BookingId
            };

            //Validate data

            //Save to database
            _db.Trips.Add(tripModel);
            _db.SaveChanges();
        }

        public void UpdateTrip(TripVM tripVM) //ASYNC return?
        {
            TripModel model = _db.Trips.FirstOrDefault(x => x.Id == tripVM.Id);
            model.EndKm = tripVM.EndKm;
            model.Cost = tripVM.Cost;
            model.CostRemarks = tripVM.CostRemarks;
            model.Active = false;
            _db.SaveChanges();
        }

        public TripVM GetTripByBookingId(int bookingId)
        {
            return _db.Trips.Where(x => x.Id == bookingId).ToList().Select(m => new TripVM()
            {
                Id = m.Id,
                BookingId = m.BookingRefId,
                StartKm = m.StartKm,
                EndKm = m.EndKm,
                Cost = m.Cost,
                CostRemarks = m.CostRemarks,
                Active = m.Active
            }).SingleOrDefault();
        }


    }
}
