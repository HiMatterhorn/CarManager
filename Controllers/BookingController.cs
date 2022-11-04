using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
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

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userId;
        private readonly string userName;
        public BookingController(IBookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
            userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public IActionResult Search()
        {
            return View();
        }

        public async Task<PartialViewResult> FilterCars(string startDate, string endDate)
        {
            AvailableCarsVM carList = await _bookingService.GetAvailableCars(startDate, endDate);
            return PartialView("_FilteredCarsView", carList);
        }

        public async Task<PartialViewResult> PendingBookingsCurrentUser()
        {
            var pendingBookingsList = await _bookingService.GetPendingBookingsByUserId(userId);
            return PartialView("_PendingBookings", pendingBookingsList);
        }

        public async Task<PartialViewResult> ApprovedBookingsCurrentUser()
        {
            var approvedBookingsList = await _bookingService.GetApprovedBookingsByUserId(userId);
            return PartialView("_ApprovedBookings", approvedBookingsList);
        }

        public PartialViewResult ActiveBookingsCurrentUser()
        {
            var activeBookingViewList = _bookingService.GetActiveBookingsByUserId(userId);

            return PartialView("_ActiveBookings", activeBookingViewList);
        }

        //GET - create
        public PartialViewResult _BookingDetailsModal(string VIN, string startDate, string endDate)
        {
            BookingVM viewModel = new BookingVM()
            {
                UserName = userName,
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
            await _bookingService.AutoConfirmBooking(3);
            return View();
        }


        public async Task<IActionResult> Calendar()
        {
            await _bookingService.AutoConfirmBooking(3);
            List<CarModel> viewModel = await _bookingService.GetAllCars();

            return View(viewModel);
        }

    }
}
