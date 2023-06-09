﻿using AmiFlota.Contracts;
using AmiFlota.Data;
using AmiFlota.Entities;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AmiFlota.Controllers
{
    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;
        private readonly ITripService _tripService;
        private readonly IUserData _userData;
        private int hoursToAutoConfirm = 3;
        private bool autoConfirm = false;
        private bool autoCancellation = false;

        public BookingController(IBookingService bookingService, ITripService tripService, IUserData userData)
        {
            _bookingService = bookingService;
            _tripService = tripService;
            _userData = userData;
        }


        public IActionResult Search()
        {
            return View();
        }

        public async Task<PartialViewResult> FilterCars(string startDate, string endDate)
        {
            AvailableCarsVM carList = await _bookingService.GetAvailableCars(startDate, endDate);

            if (carList.AvailableCars.Count > 0)
            {
                return PartialView("_FilteredCarsView", carList);
            }
            else
            {
                IEnumerable<BookingVM> notConfirmedBookingsList = await _bookingService.GetNotConfirmedBookings(startDate, endDate);
                return PartialView("_NoCarsAvailable", notConfirmedBookingsList);
            }
        }

        public async Task<PartialViewResult> PendingBookingsCurrentUser()
        {
            if (_userData.IsPriviledgedUser())
            {
                var pendingAllBookingsList = await _bookingService.GetAllPendingBookings();
                return PartialView("_PendingBookings", pendingAllBookingsList);
            }
            else
            {
                var pendingBookingsList = await _bookingService.GetPendingBookingsByUserId(_userData.Id);
                return PartialView("_PendingBookings", pendingBookingsList);
            }

        }

        public async Task<PartialViewResult> ApprovedBookingsCurrentUser()
        {
            if (_userData.IsPriviledgedUser())
            {
                var approvedAllBookingsList = await _bookingService.GetAllApprovedBookings();
                return PartialView("_ApprovedBookings", approvedAllBookingsList);
            }
            else
            {
                var approvedBookingsList = await _bookingService.GetApprovedBookingsByUserId(_userData.Id);
                return PartialView("_ApprovedBookings", approvedBookingsList);
            }
        }

        public async Task<PartialViewResult> ActiveBookingsCurrentUser()
        {
            if (_userData.IsPriviledgedUser())
            {
                var activeAllBookingsList = await _bookingService.GetAllActiveBookings();
                return PartialView("_ActiveBookings", activeAllBookingsList);
            }
            else
            {
                var activeBookingsList = await _bookingService.GetActiveBookingsByUserId(_userData.Id);
                return PartialView("_ActiveBookings", activeBookingsList);
            }
        }

        //GET - create
        public PartialViewResult _BookingDetailsModal(string VIN, string startDate, string endDate)
        {
            BookingVM viewModel = new BookingVM()
            {
                UserName = _userData.Name,
                UserId = _userData.Id,
                RegistrationNumber = _bookingService.GetRegistrationNumberByCarVin(VIN),
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate),
            };
            return PartialView("_BookingDetailsModal", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult _BookingDetailsModal(BookingVM viewModel)
        {
            if (ModelState.IsValid)
            {
                _bookingService.BookCar(viewModel);
                return (RedirectToAction("UserDashboard"));
            }
            return View(viewModel);
        }



        public async Task<IActionResult> UserDashboard()
        {
            if(autoConfirm) await _bookingService.AutoConfirmBooking(hoursToAutoConfirm);
            if(autoCancellation) await _bookingService.AutoCancelBooking();
            return View();
        }


        public async Task<IActionResult> Calendar()
        {
            if (autoConfirm) await _bookingService.AutoConfirmBooking(hoursToAutoConfirm);
            if (autoCancellation) await _bookingService.AutoCancelBooking();
            return View();
        }

        public PartialViewResult GetCalendarDataById(int id)
        {
            var booking = _bookingService.GetBookingById(id);
            var trips = _tripService.TripsHistoryByBookingId(id);
            CalendarVM viewModel = new CalendarVM
            {
                Cars = new List<CarModel>(),
                Booking = booking,
                TripsHistory = trips,
            };
        return PartialView("_CalendarEvent", viewModel);
        }

        public async Task<IActionResult> SelectCars()
        {
            await _bookingService.AutoConfirmBooking(3);
            List<CarModel> viewModel = new List<CarModel>();
            viewModel = await _bookingService.GetAllCars();

            return PartialView("_SelectCars", viewModel);
        }

    }
}
