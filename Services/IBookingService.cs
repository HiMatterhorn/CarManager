using AmiFlota.Dto;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface IBookingService
    {
        public Task<List<CarModel>> GetAllCars();
        public Task<AvailableCarsVM> GetAvailableCars(string startDate, string endDate);
        public void BookCar(BookingVM bookingVM);
        //public Task<IEnumerable<BookingModel>> GetPendingBookingsByUserId(string userId);
        public Task<IEnumerable<BookingVM>> GetPendingBookingsByUserId(string userId);
        public Task<IEnumerable<BookingVM>> GetAllPendingBookings();
        // public Task<IEnumerable<BookingModel>> GetApprovedBookingsByUserId(string userId);
        public Task<IEnumerable<BookingVM>> GetApprovedBookingsByUserId(string userId);
        public Task<IEnumerable<BookingVM>> GetAllApprovedBookings();
        public Task<IEnumerable<ActiveBookingVM>> GetActiveBookingsByUserId(string userId);
        public Task<IEnumerable<ActiveBookingVM>> GetAllActiveBookings();
        /*        public List<BookingVM> BookingsByCarVIN(string carVIN);*/
        public List<CalendarVM> AllBookingsByCarVinList(List<string> selectedCars);
        public List<CalendarVM> CurrentBookingsByCarVinList(List<string> carVIN);
        public List<CalendarVM> FinishedBookingsByCarVinList(List<string> carVIN);
        public string GetRegistrationNumberByCarVin(string carVIN);
        public string GetUserIdByName(string userName);

        public Task<int> ConfirmEvent(int id);
        public Task<int> RejectEvent(int id);
        public Task<int> DeleteEvent(int id);
        public Task<int> AutoConfirmBooking(double hours);
        public Task<int> AutoCancelBooking();

        public Task<int> TakeCar(int bookingId);
        public Task<int> ReturnCar(int bookingId);
        public BookingVM GetBookingById(int id);
    }
}
