using AmiFlota.Data;
using AmiFlota.Dto;
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
            ChangeBookingStatus(tripVM.BookingId, BookingStatus.OnTheWay);

            //TODO ???
            return 1;
        }

        public int CreateTrip(TripVM tripVM)
        {
            TripModel tripModel = new TripModel()
            {
                Id = tripVM.Id,
                StartKm = tripVM.StartKm,
                StartLocation = tripVM.StartLocation,
                EndKm = tripVM.EndKm,
                EndLocation = tripVM.EndLocation,
                Cost = tripVM.Cost,
                CostRemarks = tripVM.CostRemarks,
                BookingRefId = tripVM.BookingId,
                StartTimestampUTC = DateTime.UtcNow,
                Active = true
            };

            //Validate data

            //Save to database
            _db.Trips.Add(tripModel);
            return _db.SaveChanges();
        }



        public int FinishTrip(TripVM tripVM) //ASYNC return?
        {
            UpdateTrip(tripVM);
            ChangeBookingStatus(tripVM.BookingId, BookingStatus.Active);

            //TODO ???
            return 1;
        }

        public int UpdateTrip(TripVM tripVM)
        {
            TripModel tripModel = _db.Trips.FirstOrDefault(x => x.Id == tripVM.Id);
            tripModel.EndKm = tripVM.EndKm;
            tripModel.EndLocation = tripVM.EndLocation;
            tripModel.Cost = tripVM.Cost;
            tripModel.CostRemarks = tripVM.CostRemarks;
            tripModel.EndTimestampUTC = DateTime.UtcNow;
            tripModel.Active = false;

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

        public TripVM GetActiveTripByBookingId(int bookingId)
        {
            return _db.Trips.Where(x => x.BookingRefId == bookingId && x.Active == true).ToList().Select(m => new TripVM()
            {
                Id = m.Id,
                BookingId = m.BookingRefId,
                StartKm = m.StartKm,
                EndKm = m.EndKm,
                Cost = m.Cost,
                CostRemarks = m.CostRemarks,
            }).SingleOrDefault();
        }

        public List<string> GetAllStartLocations()
        {
            var result = _db.Trips.Select(x => x.StartLocation).Distinct().ToList();
            /*            var dtoLocations = new dtoLocation()
                        {
                            Locations = result
                        };*/

            return result;
        }

        public List<string> GetAllEndLocations()
        {
            var result = _db.Trips.Select(x => x.EndLocation).Distinct().ToList();
            /*            var dtoLocations = new dtoLocation()
                        {
                            Locations = result
                        };*/

            return result;
        }

        //TODO DELETE
        /*        public List<BookingVM> TripsByCarVinList(List<string> selectedCars)
                {
                    try
                    {
                        // NOTE var result = lista.Where(a => listb.Any(b => string.Compare(a,b,true) == 0));

                        //TODO Show start and final destination
                        var results = (from b in _db.Bookings
                                        .ToList()
                                        .Where(a => selectedCars.Any(y => string.Compare(a.CarVIN, y, true) == 0))
                                        .Where(x => x.BookingStatus.Equals(BookingStatus.Finished))
                                       from c in _db.Cars
                                       from u in _db.Users
                                       from t in _db.Trips
                                       where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id) && (t.BookingRefId == b.Id)
                                       select new BookingVM()
                                       {
                                           Id = b.Id,
                                           UserName = u.UserName,
                                           RegistrationNumber = c.RegistrationNumber,
                                           StartDate = t.StartTimestampUTC,
                                           EndDate = t.EndTimestampUTC,
                                           Description = b.Description,
                                           ProjectCost = b.ProjectCost,
                                           BookingStatus = b.BookingStatus,
                                       }).ToList();

                        return results;
                    }

                    catch (Exception)
                    {
                        throw;
                    }
                }*/

        public List<TripVM> TripsHistoryByCarVin(string vin)
        {
            try
            {
                var results = (from b in _db.Bookings
                               .Where(x => x.CarVIN.Equals(vin))
                               from u in _db.Users
                               from t in _db.Trips
                               where b.UserId.Equals(u.Id) && t.BookingRefId == b.Id
                               select new TripVM()
                               {
                                   Id = t.Id,
                                   Active = t.Active,
                                   StartKm = t.StartKm,
                                   StartLocation = t.StartLocation,
                                   EndKm = t.EndKm,
                                   EndLocation = t.EndLocation,
                                   Project = t.Project,
                                   Cost = t.Cost,
                                   CostRemarks = t.CostRemarks,
                                   User = u.UserName,
                               }).OrderByDescending(y => y.StartKm).ToList();
                return results;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<TripVM> TripsHistoryByBookingId(int id)
        {
            try
            {
                var results = (from b in _db.Bookings
                               .Where(x => x.Id == id)
                               from u in _db.Users
                               from t in _db.Trips
                               where b.UserId.Equals(u.Id) && t.BookingRefId == b.Id
                               select new TripVM()
                               {
                                   Id = t.Id,
                                   Active = t.Active,
                                   StartKm = t.StartKm,
                                   StartLocation = t.StartLocation,
                                   EndKm = t.EndKm,
                                   EndLocation = t.EndLocation,
                                   Project = t.Project,
                                   Cost = t.Cost,
                                   CostRemarks = t.CostRemarks,
                                   User = u.UserName,
                               }).OrderByDescending(y => y.StartKm).ToList();
                return results;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public uint HighestMileageValue()
        {
            /*            var highestStartMileage = _db.Trips.Max(x => x.StartKm);
                        var highestEndMileage = _db.Trips.Max(x => x.EndKm);*/

            uint highestvalue = 500;

            return highestvalue;


        }
    }
}
