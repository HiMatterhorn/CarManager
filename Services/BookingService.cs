using AmiFlota.Data;
using AmiFlota.Dto;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Services
{
    public class BookingService : IBookingService
    {
        private readonly AmiFlotaContext _db;
        private readonly UserManager<ApplicationUserModel> _userManager;

        private readonly DateTime nowDateTime = DateTime.UtcNow;

        public BookingService(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<CarModel>> GetAllCars()
        {

            //TODO Group by Trunk type
            List<CarModel> cars = await _db.Cars.OrderBy(x => x.Trunk).ThenBy(x => x.Brand).ThenBy(x => x.Model).ThenBy(x => x.RegistrationNumber).ToListAsync();
            return cars;
        }

        public async Task<AvailableCarsVM> GetAvailableCars(string startDate, string endDate)
        {

            DateTime searchingStartDate = DateTime.Parse(startDate);
            DateTime searchingEndDate = DateTime.Parse(endDate);

            List<CarModel> availableCars = await GetCarsInDates(searchingStartDate, searchingEndDate);

            AvailableCarsVM availableCarsVM = new AvailableCarsVM()
            {
                AvailableCars = availableCars,
                StartDate = searchingStartDate,
                EndDate = searchingEndDate,
            };

            return availableCarsVM;
        }

        public async Task<IEnumerable<BookingVM>> GetNotConfirmedBookings(string startDate, string endDate)
        {

            DateTime searchingStartDate = DateTime.Parse(startDate);
            DateTime searchingEndDate = DateTime.Parse(endDate);

            List<BookingVM> notConfirmedBookings = await GetNotConfirmedBookingsInDates(searchingStartDate, searchingEndDate);

            return notConfirmedBookings;
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
                    .Where(e => e.EndDate >= startDate)
                    .Where(b => !b.BookingStatus.Equals(BookingStatus.Finished)) 
                    .ToList();


                if (bookings.Count() == 0)
                {
                    availableCars.Add(car);
                }
            };
            return availableCars;
        }

        public async Task<List<BookingVM>> GetNotConfirmedBookingsInDates(DateTime startDate, DateTime endDate)
        {
            var notConfirmedBookings = await (from b in _db.Bookings
                                        .Where(s => (s.StartDate >= startDate && s.StartDate <= endDate) || (s.EndDate >= startDate && s.EndDate <= endDate) || (s.StartDate <= startDate && s.EndDate >= endDate))
                                        .Where(b => b.BookingStatus.Equals(BookingStatus.Pending))
                                        from c in _db.Cars
                                        from u in _db.Users
                                        where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                                        select new BookingVM
                                        {
                                            Id = b.Id,
                                            UserId = u.Id,
                                            UserName = u.UserName,
                                            RegistrationNumber = c.RegistrationNumber,
                                            PhotoPath = c.PhotoPath,
                                            StartDate = b.StartDate,
                                            EndDate = b.EndDate,
                                            Description = b.Description,
                                            ProjectCost = b.ProjectCost
                                        }).ToListAsync();


            var test = _db.Bookings.
                Include(x => x.CarModels).
                Include(y => y.ApplicationUserModels).
                Where(s => (s.StartDate >= startDate && s.StartDate <= endDate)).First();
            var test2 = test.CarModels.Fuel;
            var test3 = test.ApplicationUserModels.BookingModels;
            _db.SaveChanges();
            

            return notConfirmedBookings;
        }

        //TODO Rework it to ViewBags?
        public void BookCar(BookingVM bookingVM)
        {
            BookingModel bookingModel = new BookingModel()
            {
                UserId = bookingVM.UserId,
                StartDate = bookingVM.StartDate,
                EndDate = bookingVM.EndDate,
                CarVIN = GetVinByRegistrationNumber(bookingVM.RegistrationNumber),
                ProjectCost = bookingVM.ProjectCost,
                Description = bookingVM.Description,
                BookingStatus = BookingStatus.Pending
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
                //TODO Booking not validated successfully - exception
                Console.WriteLine("booking not validated");
            }
        }

        public bool ValidateBooking(BookingModel bookingModel)
        {
            var bookings = _db.Bookings
    .Where(x => x.CarVIN.Equals(bookingModel.CarVIN))
    .Where(s => s.StartDate <= bookingModel.StartDate)
    .Where(e => e.EndDate >= bookingModel.StartDate)
    .Where(b => !b.BookingStatus.Equals(BookingStatus.Finished))
    .ToList();

            if (bookings.Count() == 0)
            {
                return true;
            }
            return false; //TODO Notify about error
        }

        public async Task<IEnumerable<BookingVM>> GetPendingBookingsByUserId(string userId = null)
        {
            try
            {
                var results = await
                    (from b in _db.Bookings.Where(x => x.UserId == userId).Where(a => a.BookingStatus.Equals(BookingStatus.Pending))
                     from c in _db.Cars
                     from u in _db.Users
                     where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                     select new BookingVM
                     {
                         Id = b.Id,
                         UserName = u.UserName,
                         UserId = u.Id,
                         RegistrationNumber = c.RegistrationNumber,
                         PhotoPath = c.PhotoPath,
                         StartDate = b.StartDate,
                         EndDate = b.EndDate,
                         Description = b.Description,
                         ProjectCost = b.ProjectCost,
                         BookingStatus = b.BookingStatus,
                     }).ToListAsync();

                //var test = _db.Bookings.Where(x => x.Id =1 );

                //if (userId != null)
                //    test = test.Where(x => x.UserId == userId);

                //return results.ToList();

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookingVM>> GetAllPendingBookings()
        {
            try
            {
                var results = await
                    (from b in _db.Bookings.Where(a => a.BookingStatus.Equals(BookingStatus.Pending))
                     from c in _db.Cars
                     from u in _db.Users
                     where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                     select new BookingVM
                     {
                         Id = b.Id,
                         UserName = u.UserName,
                         UserId=u.Id,
                         RegistrationNumber = c.RegistrationNumber,
                         PhotoPath = c.PhotoPath,
                         StartDate = b.StartDate,
                         EndDate = b.EndDate,
                         Description = b.Description,
                         ProjectCost = b.ProjectCost,
                         BookingStatus = b.BookingStatus,
                     }).ToListAsync();

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookingVM>> GetApprovedBookingsByUserId(string userId)
        {
            try
            {
                var results = await
                    (from b in _db.Bookings.Where(x => x.UserId == userId).Where(a => a.BookingStatus.Equals(BookingStatus.Approved))
                     from c in _db.Cars
                     from u in _db.Users
                     where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                     select new BookingVM
                     {
                         Id = b.Id,
                         UserName = u.UserName,
                         UserId = u.Id,
                         RegistrationNumber = c.RegistrationNumber,
                         PhotoPath = c.PhotoPath,
                         StartDate = b.StartDate,
                         EndDate = b.EndDate,
                         Description = b.Description,
                         ProjectCost = b.ProjectCost,
                         BookingStatus = b.BookingStatus,
                     }).ToListAsync();

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookingVM>> GetAllApprovedBookings()
        {
            try
            {
                var results = await
                    (from b in _db.Bookings.Where(a => a.BookingStatus.Equals(BookingStatus.Approved))
                     from c in _db.Cars
                     from u in _db.Users
                     where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                     select new BookingVM
                     {
                         Id = b.Id,
                         UserName = u.UserName,
                         UserId=u.Id,
                         RegistrationNumber = c.RegistrationNumber,
                         PhotoPath = c.PhotoPath,
                         StartDate = b.StartDate,
                         EndDate = b.EndDate,
                         Description = b.Description,
                         ProjectCost = b.ProjectCost,
                         BookingStatus = b.BookingStatus,
                     }).ToListAsync();

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ActiveBookingVM>> GetActiveBookingsByUserId(string userId)
        {
            try
            {
                var results = await
                    (from b in _db.Bookings.Where(x => x.UserId == userId).Where(a => a.BookingStatus.Equals(BookingStatus.Active) || a.BookingStatus.Equals(BookingStatus.OnTheWay))

                     select new ActiveBookingVM
                     {
                         BookingViewModel = (from c in _db.Cars
                                             from u in _db.Users
                                             where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                                             select new BookingVM()
                                             {
                                                 Id = b.Id,
                                                 UserName = u.UserName,
                                                 UserId = u.Id,
                                                 RegistrationNumber = c.RegistrationNumber,
                                                 PhotoPath = c.PhotoPath,
                                                 StartDate = b.StartDate,
                                                 EndDate = b.EndDate,
                                                 Description = b.Description,
                                                 ProjectCost = b.ProjectCost,
                                                 BookingStatus = b.BookingStatus,
                                             }).FirstOrDefault(),
                         TripsHistory = (from t in _db.Trips
                                         where t.BookingRefId == b.Id
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
                                             CostRemarks = t.CostRemarks
                                         }).OrderByDescending(y => y.StartKm).ToList()
                     }).ToListAsync();

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ActiveBookingVM>> GetAllActiveBookings()
        {
            try
            {
                var results = await
                    (from b in _db.Bookings.Where(a => a.BookingStatus.Equals(BookingStatus.Active) || a.BookingStatus.Equals(BookingStatus.OnTheWay))

                     select new ActiveBookingVM
                     {
                         BookingViewModel = (from c in _db.Cars
                                             from u in _db.Users
                                             where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                                             select new BookingVM()
                                             {
                                                 Id = b.Id,
                                                 UserName = u.UserName,
                                                 UserId = u.Id,
                                                 RegistrationNumber = c.RegistrationNumber,
                                                 PhotoPath = c.PhotoPath,
                                                 StartDate = b.StartDate,
                                                 EndDate = b.EndDate,
                                                 Description = b.Description,
                                                 ProjectCost = b.ProjectCost,
                                                 BookingStatus = b.BookingStatus,
                                             }).FirstOrDefault(),
                         TripsHistory = (from t in _db.Trips
                                         where t.BookingRefId == b.Id
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
                                             CostRemarks = t.CostRemarks
                                         }).OrderByDescending(y => y.StartKm).ToList()
                     }).ToListAsync();

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
                var result = (from b in _db.Bookings
                              .Where(x => x.CarVIN == carVIN)
                              from u in _db.Users
                              from c in _db.Cars
                              where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                              select new BookingVM()
                              {
                                  Id = b.Id,
                                  UserName = u.UserName,
                                  UserId = u.Id,
                                  RegistrationNumber = c.RegistrationNumber,
                                  StartDate = b.StartDate,
                                  EndDate = b.EndDate,
                                  Description = b.Description,
                                  ProjectCost = b.ProjectCost,
                                  BookingStatus = b.BookingStatus,
                              }).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookingVM GetBookingById(int id)
        {
            try
            {
                var result = (from b in _db.Bookings
                              .Where(x => x.Id == id)
                              from u in _db.Users
                              from c in _db.Cars
                              where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                              select new BookingVM()
                              {
                                  Id = b.Id,
                                  UserName = u.UserName,
                                  UserId = u.Id,
                                  RegistrationNumber = c.RegistrationNumber,
                                  StartDate = b.StartDate,
                                  EndDate = b.EndDate,
                                  Description = b.Description,
                                  ProjectCost = b.ProjectCost,
                                  BookingStatus = b.BookingStatus,
                              }).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CalendarVM> AllBookingsByCarVinList(List<string> selectedCars)
        {
            try
            {
                var currentBookings = CurrentBookingsByCarVinList(selectedCars);
                var finishedBookings = FinishedBookingsByCarVinList(selectedCars);
                var mergedLists = currentBookings.Concat(finishedBookings).ToList();
                return mergedLists;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<CalendarVM> CurrentBookingsByCarVinList(List<string> selectedCars)
        {
            try
            {
                // NOTE var result = lista.Where(a => listb.Any(b => string.Compare(a,b,true) == 0));

                var results = (from b in _db.Bookings
                                   .ToList()
                                   .Where(a => selectedCars.Any(y => string.Compare(a.CarVIN, y, true) == 0))
                                   .Where(x => !x.BookingStatus.Equals(BookingStatus.Finished))

                               select new CalendarVM
                               {
                                   Booking = (from c in _db.Cars
                                              from u in _db.Users
                                              where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                                              select new BookingVM()
                                              {
                                                  Id = b.Id,
                                                  UserName = u.UserName,
                                                  UserId = u.Id,
                                                  RegistrationNumber = c.RegistrationNumber,
                                                  PhotoPath = c.PhotoPath,
                                                  StartDate = b.StartDate,
                                                  EndDate = b.EndDate,
                                                  Description = b.Description,
                                                  ProjectCost = b.ProjectCost,
                                                  BookingStatus = b.BookingStatus,
                                              }).FirstOrDefault(),
                                   TripsHistory = (from t in _db.Trips
                                                   where t.BookingRefId == b.Id
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
                                                       CostRemarks = t.CostRemarks
                                                   }).OrderByDescending(y => y.StartKm).ToList()
                               }).ToList();
                return results;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<CalendarVM> FinishedBookingsByCarVinList(List<string> selectedCars)
        {
            try
            {
                var results = (from b in _db.Bookings
                   .ToList()
                   .Where(a => selectedCars.Any(y => string.Compare(a.CarVIN, y, true) == 0))
                   .Where(x => x.BookingStatus.Equals(BookingStatus.Finished))

                               select new CalendarVM
                               {
                                   Booking = (from c in _db.Cars
                                              from u in _db.Users
                                              where b.CarVIN.Equals(c.VIN) && b.UserId.Equals(u.Id)
                                              select new BookingVM()
                                              {
                                                  Id = b.Id,
                                                  UserName = u.UserName,
                                                  UserId = u.Id,
                                                  RegistrationNumber = c.RegistrationNumber,
                                                  PhotoPath = c.PhotoPath,
                                                  StartDate = b.StartDate,
                                                  EndDate = b.EndDate,
                                                  Description = b.Description,
                                                  ProjectCost = b.ProjectCost,
                                                  BookingStatus = b.BookingStatus,
                                              }).FirstOrDefault(),
                                   TripsHistory = (from t in _db.Trips
                                                   where t.BookingRefId == b.Id
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
                                                       CostRemarks = t.CostRemarks
                                                   }).OrderByDescending(y => y.StartKm).ToList()
                               }).ToList();
                return results;
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

        public async Task<int> ConfirmEvent(int id)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (booking != null)
            {
                booking.BookingStatus = BookingStatus.Approved;
                return await _db.SaveChangesAsync();
            }

            return 0;
        }
        public async Task<int> RejectEvent(int id)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (booking != null)
            {
                booking.BookingStatus = BookingStatus.Pending;
                return await _db.SaveChangesAsync();
            }

            return 0;
        }
        public async Task<int> DeleteEvent(int id)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (booking != null)
            {
                _db.Bookings.Remove(booking);
                return await _db.SaveChangesAsync();
            }

            return 0;
        }
        public async Task<int> TakeCar(int bookingId)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId);
            if (booking != null)
            {
                booking.CarTakenUTC = nowDateTime;
                booking.BookingStatus = BookingStatus.Active;
                return await _db.SaveChangesAsync();
            }

            return 0;
        }
        public async Task<int> ReturnCar(int bookingId)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId);
            if (booking != null)
            {
                booking.CarReturnedUTC = nowDateTime;
                booking.BookingStatus = BookingStatus.Finished;
                return await _db.SaveChangesAsync();
            }

            return 0;
        }
        public async Task<int> AutoConfirmBooking(double hours)
        {

            var autoConfirmDateTime = DateTime.UtcNow.AddHours(hours);
            _db.Bookings.Where(x => x.StartDate < autoConfirmDateTime && x.StartDate > nowDateTime && x.BookingStatus.Equals(BookingStatus.Pending)).ToList()
            .ForEach(b => { b.BookingStatus = BookingStatus.Approved; });
            //TODO And not on rejected list
            return await _db.SaveChangesAsync();
        }

        public async Task<int> AutoCancelBooking()
        {
            var list = _db.Bookings.Where(x => (x.EndDate < DateTime.Now) && (x.BookingStatus.Equals(BookingStatus.Pending) || x.BookingStatus.Equals(BookingStatus.Approved))).ToList();
            list.ForEach(b => { b.BookingStatus = BookingStatus.Cancelled;});

            //TODO Notify about cancelled bookings
            return await _db.SaveChangesAsync();
        }

    }
}
