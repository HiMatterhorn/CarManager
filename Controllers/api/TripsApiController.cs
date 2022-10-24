using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using AmiFlota.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AmiFlota.Controllers.api
{
    [Route("api/Trip")]
    [ApiController]
    public class TripsApiController : Controller
    {
        private readonly ITripService _tripService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TripsApiController(ITripService tripService, IHttpContextAccessor httpContextAccessor)
        {
            _tripService = tripService;
            _httpContextAccessor = httpContextAccessor;
        }


        //TODO Create inspection
        [HttpPost]
        [Route("CreateInspection")]
        public async Task<IActionResult> CreateInspection(TripVM tripVM)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();

            try
            {
                commonResponse.dataenum = await _tripService.CreateInspection(tripVM);
                commonResponse.status = ApiResponses.success_code;
            }
            catch (Exception e)
            {

                commonResponse.message = e.Message;
                commonResponse.status = ApiResponses.failure_code;
            }

            return Ok(commonResponse);
        }
    }
}
