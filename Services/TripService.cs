﻿using AmiFlota.Contracts;
using AmiFlota.Data;
using AmiFlota.Dto;
using AmiFlota.Enums;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public int StartTrip(TripStartVM tripStartVM) //ASYNC return?
        {
            CreateTrip(tripStartVM);
            ChangeBookingStatus(tripStartVM.BookingId, BookingStatus.OnTheWay);

            //TODO ???
            return 1;
        }

        public int CreateTrip(TripStartVM tripStartVM)
        {
            TripModel tripModel = new TripModel()
            {
                Id = tripStartVM.Id,
                StartKm = tripStartVM.StartKm,
                StartLocation = tripStartVM.StartLocation,
                BookingRefId = tripStartVM.BookingId,
                StartTimestampUTC = DateTime.UtcNow,
                Active = true
            };
            //Validate data

            //Save to database
            _db.Trips.Add(tripModel);
            return _db.SaveChanges();
        }



        public int FinishTrip(TripEndVM tripEndVM) //ASYNC return?
        {
            UpdateTrip(tripEndVM);
            ChangeBookingStatus(tripEndVM.BookingId, BookingStatus.Active);

            //TODO ???
            return 1;
        }

        public int UpdateTrip(TripEndVM tripEndVM)
        {
            TripModel tripModel = _db.Trips.FirstOrDefault(x => x.Id == tripEndVM.Id);
            tripModel.EndKm = tripEndVM.EndKm;
            tripModel.EndLocation = tripEndVM.EndLocation;
            tripModel.Project = tripEndVM.Project;
            tripModel.Cost = tripEndVM.Cost;
            tripModel.CostRemarks = tripEndVM.CostRemarks;
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

        public TripEndVM GetActiveTripByBookingId(int bookingId)
        {
            return _db.Trips.Where(x => x.BookingRefId == bookingId && x.Active == true).ToList().Select(m => new TripEndVM()
            {
                Id = m.Id,
                BookingId = m.BookingRefId,
                EndKm = m.EndKm,
                Cost = m.Cost,
                CostRemarks = m.CostRemarks,
            }).SingleOrDefault();
        }

        public async Task<List<string>> GetAllStartLocations()
        {
            var result = await _db.Trips.Select(x => x.StartLocation).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<string>> GetAllEndLocations()
        {
            var result = await _db.Trips.Select(x => x.EndLocation).Distinct().ToListAsync();
            return result;
        }

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

        public uint HighestMileageValue(int bookingId)
        {

            var booking = _db.Bookings.Where(b => b.Id == bookingId).FirstOrDefault();

            var highestStartValue = _db.Bookings.Where(v => v.CarVIN.Equals(booking.CarVIN))
                                        .Join(_db.Trips, i => i.Id, r => r.BookingRefId, (i, r) => r)
                                        .OrderByDescending(x => x.StartKm).FirstOrDefault();

            var highestEndValue = _db.Bookings.Where(v => v.CarVIN.Equals(booking.CarVIN))
                                    .Join(_db.Trips, i => i.Id, r => r.BookingRefId, (i, r) => r)
                                    .OrderByDescending(x => x.StartKm).FirstOrDefault();

            if(highestStartValue == null || highestEndValue == null)
            {
                return 0;
            }
            else
            {
                return highestStartValue.StartKm > highestEndValue.EndKm ? highestStartValue.StartKm : highestEndValue.EndKm;
            }
            

/*            if (highestStartValue.StartKm > highestEndValue.EndKm)
            {
                return highestStartValue.StartKm;
            }
            else
            {
                return highestEndValue.EndKm;
            }*/

        }
    }
}
