using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using AmiFlota.Utilities;
using AppointmentScheduler.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AmiFlota.Controllers.api
{

    [Route("api/Booking")]
    [ApiController]
    public class BookingApiController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingApiController(IBookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetCalendarDataForCar")]
        public IActionResult GetCalendarDataForCar(string carVIN)
        {
            CommonResponse<List<BookingVM>> commonResponse = new CommonResponse<List<BookingVM>>();
            try
            {
                commonResponse.dataenum = _bookingService.BookingsByCarVIN(carVIN);
                commonResponse.status = ApiResponses.success_code;
            }
            catch (Exception e)
            {

                commonResponse.message = e.Message;
                commonResponse.status = ApiResponses.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            CommonResponse<BookingVM> commonResponse = new CommonResponse<BookingVM>();
            try
            {
                commonResponse.dataenum = _bookingService.GetById(id);
                commonResponse.status = ApiResponses.success_code;
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = ApiResponses.failure_code;
            }

            return Ok(commonResponse);
        }


        /*        public IActionResult Index()
                {
                    return View();
                }*/

        /*        [HttpGet]
                [Route("GetAllAvailableCars")]
                public IActionResult GetAllAvailableCars(DateTime startDate, DateTime endDate)
                {
                    CommonResponse<List<CarModel>> commonResponse = new CommonResponse<List<CarModel>>();
                    try
                    {
                        commonResponse.dataenum = _bookingService.GetAllAvailableCars(startDate, endDate);
                        commonResponse.status = ApiResponses.success_code;
                    }
                    catch (Exception e)
                    {
                        commonResponse.message = e.Message;
                        commonResponse.status = ApiResponses.failure_code;
                    }

                    return Ok(commonResponse);
                }*/


    }
}
