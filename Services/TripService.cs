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
                BookingRefId = tripVM.BookingId,
                StartTimestampUTC = DateTime.UtcNow,
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
            tripModel.EndTimestampUTC = DateTime.UtcNow;

            return _db.SaveChanges();
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

        public List<CalendarVM> TripsByCarVinList(List<string> selectedCars)
        {
            try
            {
                // NOTE var result = lista.Where(a => listb.Any(b => string.Compare(a,b,true) == 0));
                var results = (from b in _db.Bookings
                                .ToList()
                                .Where(a => selectedCars.Any(y => string.Compare(a.CarVIN, y, true) == 0))
                                .Where(x => x.BookingStatus.Equals(BookingStatus.Finished))
                               from c in _db.Cars
                               from u in _db.Users
                               from t in _db.Trips
                               where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id) && (t.BookingRefId == b.Id)
                               select new CalendarVM()
                               {
                                   Id = b.Id,
                                   UserName = u.UserName,
                                   RegistrationNumber = c.RegistrationNumber,
                                   StartDate = t.StartTimestampUTC,
                                   EndDate = t.EndTimestampUTC,
                                   Destination = b.Destination,
                                   ProjectCost = b.ProjectCost,
                                   BookingStatus = b.BookingStatus,
                               }).ToList();

                return results;
            }

            catch (Exception)
            {
                throw;
            }
        }


    }
}
