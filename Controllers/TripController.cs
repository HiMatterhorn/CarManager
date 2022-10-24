using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public PartialViewResult _TripStartModal(int id)
        {
            TripVM viewModel = new TripVM()
            {
                BookingId = id,
            };
            return PartialView("_TripStartModal", viewModel);
        }
    }
}
