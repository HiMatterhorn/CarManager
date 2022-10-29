using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmiFlota.Controllers
{
    public class TripController : Controller
    {

        private readonly ITripService _tripService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userId;
        private readonly string userName;
        public TripController(ITripService tripService, IHttpContextAccessor httpContextAccessor)
        {
            _tripService = tripService;
            _httpContextAccessor = httpContextAccessor;
            userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public PartialViewResult _TripStartModal(int bookingId)
        {
            TripVM viewModel = new TripVM()
            {
                BookingId = bookingId,
            };
            return PartialView("_TripStartModal", viewModel);
        }

        [HttpGet]
        public PartialViewResult _TripEndModal(int bookingId)
        {
            TripVM viewModel = _tripService.GetTripByBookingId(bookingId);

            return PartialView("_TripEndModal", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTrip(TripVM viewModel)
        {
            if (ModelState.IsValid)
            {
                _tripService.CreateTrip(viewModel);
                return RedirectToAction("UserDashboard", "Booking");
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTrip(TripVM viewModel)
        {
            if (ModelState.IsValid)
            {
                _tripService.UpdateTrip(viewModel);
                return RedirectToAction("UserDashboard", "Booking");
            }
            return View(viewModel);
        }


    }
}
