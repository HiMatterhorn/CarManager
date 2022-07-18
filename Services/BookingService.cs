﻿using AmiFlota.Data;
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
    public class BookingService : IBookingService
    {
        private readonly AmiFlotaContext _db;
        private readonly UserManager<ApplicationUserModel> _userManager;

        public BookingService(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public async Task<AvailableCarsVM> GetAvailableCars(DateTime startDate, DateTime endDate)
        {

            List<CarModel> availableCars = await GetCarsInDates(startDate, endDate);

            AvailableCarsVM availableCarsVM = new AvailableCarsVM()
            {
                AvailableCars = availableCars,
                StartDate = startDate,
                EndDate = endDate,
            };

            return availableCarsVM;
        }

        public async Task<List<CarModel>> GetAllCars()
        {
            List<CarModel> cars = await _db.Cars.ToListAsync();

            return cars;
        }

        public async Task<List<CarModel>> GetCarsInDates(DateTime startDate, DateTime endDate)
        {
            List<CarModel> availableCars = new List<CarModel>();
            IEnumerable<CarModel> cars = await GetAllCars();

            foreach (var car in cars)
            {
                var bookings = _db.Bookings
                    .Where(x => x.CarVIN.Equals(car.VIN))
                    .Where(s => s.StartDate <= endDate)
                    .Where(e => e.EndDate >= startDate).ToList();


                if (bookings.Count() == 0)
                {
                    availableCars.Add(car);
                }
            };
            return availableCars;
        }

        public IEnumerable<CarModel> GetCarByVIN(string VIN)
        {
            return _db.Cars.Where(x => x.VIN.Equals(VIN));
        }

        public void BookCar(BookingVM bookingVM)
        {
            //TODO GetCarVinByRegistrationNumber
            BookingModel bookingModel = new BookingModel()
            {
                //TODO Convert bookingVM -> bookingModel
                UserId = GetUserIdByName(bookingVM.UserName),
                StartDate = bookingVM.StartDate,
                EndDate = bookingVM.EndDate,
                CarVIN = GetVinByRegistrationNumber(bookingVM.RegistrationNumber),
                ProjectCost = bookingVM.ProjectCost,
                Destination = bookingVM.Destination,
                isApproved = bookingVM.isApproved,
            };

            //Validate booking
            bool validatationResult = ValidateBooking(bookingModel);

            //Save to database
            if (validatationResult)
            {
                _db.Bookings.Add(bookingModel);
                _db.SaveChanges();
            }

            else
            {
                //TODO Booking not validated successfully
                Console.WriteLine("booking not validated");
            }
        }

        public bool ValidateBooking(BookingModel bookingModel)
        {
            //TODO Check if carVin exists in database?
            var bookings = _db.Bookings
                .Where(x => x.CarVIN.Equals(bookingModel.CarVIN))
                .Where(s => s.StartDate <= bookingModel.StartDate)
                .Where(e => e.EndDate >= bookingModel.StartDate).ToList();

            if (bookings.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BookingModel>> GetPendingBookingsByUserId(string userId)
        {
            try
            {
                var results = await _db.Bookings.
                Where(x => x.UserId == userId).
                Where(a => a.isApproved == false).
                ToListAsync();
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookingModel>> GetApprovedBookingsByUserId(string userId)
        {
            try
            {
                var results = await _db.Bookings.
                Where(x => x.UserId == userId).
                Where(a => a.isApproved == true).
                ToListAsync();
                return results;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<BookingVM> BookingsByCarVIN(string carVIN)
        {
            try
            {
                return _db.Bookings.Where(x => x.CarVIN == carVIN).ToList().Select(c => new BookingVM()
                {
                    UserName = GetUserNameById(c.UserId),
                    RegistrationNumber = GetRegistrationNumberByCarVin(c.CarVIN),
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Destination = c.Destination,
                    ProjectCost = c.ProjectCost,
                    isApproved = c.isApproved,
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetRegistrationNumberByCarVin(string carVIN)
        {
            try
            {
                var result = _db.Cars.FirstOrDefault(x => x.VIN == carVIN);
                return result.RegistrationNumber;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetVinByRegistrationNumber(string registrationNumber)
        {
            try
            {
                var result = _db.Cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber);
                return result.VIN;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetUserIdByName(string userName)
        {
            try
            {
                var result = _db.Users.FirstOrDefault(x => x.UserName == userName);
                return result.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetUserNameById(string userId)
        {
            try
            {
                var result = _db.Users.FirstOrDefault(x => x.Id == userId);
                return result.UserName;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
