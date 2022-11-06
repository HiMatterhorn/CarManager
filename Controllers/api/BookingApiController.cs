using AmiFlota.Dto;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using AmiFlota.Utilities;

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

        //TODO Delete if won't be used
/*        [HttpGet]
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
        }*/

        //TODO Test method overload for checkboxes in calendar view
        [HttpPost]
        [Route("GetCalendarDataForCarList")]
        public IActionResult GetCalendarDataForCarList([FromBody] dtoVIN dtoSelectedCars)
        {


            CommonResponse<List<CalendarVM>> commonResponse = new CommonResponse<List<CalendarVM>>();
            try
            {
                commonResponse.dataenum = _bookingService.AllBookingsByCarVinList(dtoSelectedCars.Selected);
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
        [Route("GetCalendarDataById")]
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


        [HttpGet]
        [Route("ConfirmEvent")]
        public async Task<IActionResult> ConfirmEvent(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                var result = await _bookingService.ConfirmEvent(id);
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
        [Route("RejectEvent")]
        public async Task<IActionResult> RejectEvent(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                var result = await _bookingService.RejectEvent(id);
                if (result > 0)
                {
                    commonResponse.status = ApiResponses.success_code;
                    commonResponse.message = ApiResponses.bookingRejected;
                }
                else
                {
                    commonResponse.status = ApiResponses.failure_code;
                    commonResponse.message = ApiResponses.bookingRejectionError;
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

        [HttpGet]
        [Route("TakeCar")]
        public async Task<IActionResult> TakeCar(int bookingId)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = await _bookingService.TakeCar(bookingId);
                commonResponse.message = commonResponse.status == 1 ? ApiResponses.carTaken : ApiResponses.carTakingError;
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = ApiResponses.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ReturnCar")]
        public async Task<IActionResult> ReturnCar(int bookingId)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = await _bookingService.ReturnCar(bookingId);
                commonResponse.message = commonResponse.status == 1 ? ApiResponses.carReturn : ApiResponses.carReturningError;
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
