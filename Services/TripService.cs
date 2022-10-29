using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AmiFlota.Utilities.Enums;

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

        public int StartTrip(TripVM tripVM) //ASYNC return?
        {
            CreateTrip(tripVM);
            ChangeBookingStatus(tripVM.BookingId, BookingStatus.Active);

            //TODO ???
            return 1;
        }

        public int CreateTrip(TripVM tripVM)
        {
            TripModel tripModel = new TripModel()
            {
                Id = tripVM.Id,
                StartKm = tripVM.StartKm,
                EndKm = tripVM.EndKm,
                Cost = tripVM.Cost,
                CostRemarks = tripVM.CostRemarks,
                BookingRefId = tripVM.BookingId
            };

            //Validate data

            //Save to database
            _db.Trips.Add(tripModel);
            return _db.SaveChanges();
        }



        public int FinishTrip(TripVM tripVM) //ASYNC return?
        {
            UpdateTrip(tripVM);
            ChangeBookingStatus(tripVM.BookingId, BookingStatus.Finished);

            //TODO ???
            return 1;
        }

        public int UpdateTrip(TripVM tripVM)
        {
            TripModel tripModel = _db.Trips.FirstOrDefault(x => x.Id == tripVM.Id);
            tripModel.EndKm = tripVM.EndKm;
            tripModel.Cost = tripVM.Cost;
            tripModel.CostRemarks = tripVM.CostRemarks;

            return  _db.SaveChanges();
        }


        public int ChangeBookingStatus(int bookingId, BookingStatus newBookingStatus)
        {
            var booking = _db.Bookings.FirstOrDefault(x => x.Id == bookingId);
            if (booking != null)
            {
                booking.BookingStatus = newBookingStatus;
                return _db.SaveChanges();
            }

            return 0;
        }

        public TripVM GetTripByBookingId(int bookingId)
        {
            return _db.Trips.Where(x => x.BookingRefId == bookingId).ToList().Select(m => new TripVM()
            {
                Id = m.Id,
                BookingId = m.BookingRefId,
                StartKm = m.StartKm,
                EndKm = m.EndKm,
                Cost = m.Cost,
                CostRemarks = m.CostRemarks,
            }).SingleOrDefault();
        }


    }
}
