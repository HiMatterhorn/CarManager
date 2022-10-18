using AmiFlota.Dto;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using AmiFlota.Utilities;
using AppointmentScheduler.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        //TODO Test method overload for checkboxes in calendar view
        [HttpPost]
        [Route("GetCalendarDataForCarList")]
        public IActionResult GetCalendarDataForCarList([FromBody] dtoVIN dtoSelectedCars) //[FromBody]List<string> listCarVin)
        {


            CommonResponse<List<BookingVM>> commonResponse = new CommonResponse<List<BookingVM>>();
            try
            {
                //TODO TEST
                //List<string> list = new List<string>(dtoSelectedCars.Selected);
                commonResponse.dataenum = _bookingService.BookingsByCarVinList(dtoSelectedCars.Selected);
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


        [HttpGet]
        [Route("ConfirmEvent")]
        public IActionResult ConfirmEvent(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                var result = _bookingService.ConfirmEvent(id).Result;
                if (result > 0)
                {
                    commonResponse.status = ApiResponses.success_code;
                    commonResponse.message = ApiResponses.bookingConfirmed;
                }
                else
                {
                    commonResponse.status = ApiResponses.failure_code;
                    commonResponse.message = ApiResponses.bookingConfirmationError;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = ApiResponses.failure_code;
            }

            return Ok(commonResponse);
        }


        [HttpGet]
        [Route("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = await _bookingService.DeleteEvent(id);
                commonResponse.message = commonResponse.status == 1 ? ApiResponses.bookingDeleted : ApiResponses.bookingDeleteError;
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
