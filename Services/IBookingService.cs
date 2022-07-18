﻿using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface IBookingService
    {
        public Task<List<CarModel>> GetAllCars();
        public IEnumerable<CarModel> GetCarByVIN(string VIN);
        public Task<AvailableCarsVM> GetAvailableCars (DateTime startDate, DateTime endDate);
        public void BookCar(BookingVM bookingVM);
        public Task<IEnumerable<BookingModel>> GetPendingBookingsByUserId(string userId);
        public Task<IEnumerable<BookingModel>> GetApprovedBookingsByUserId(string userId);
        public List<BookingVM> BookingsByCarVIN(string carVIN);
        public string GetRegistrationNumberByCarVin(string carVIN);
        public string GetUserIdByName(string userName);
    }
}
