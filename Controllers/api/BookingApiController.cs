using AmiFlota.Dto;
using AmiFlota.Enums;
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

        [HttpPost]
        [Route("GetCalendarDataForCarList")]
        public IActionResult GetCalendarDataForCarList([FromBody] dtoVIN dtoSelectedCars)
        {
            CommonResponse<List<CalendarVM>> commonResponse; /*= new CommonResponse<List<CalendarVM>>();*/
            try
            {
                var result = _bookingService.AllBookingsByCarVinList(dtoSelectedCars.Selected);
                commonResponse = new CommonResponse<List<CalendarVM>>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<List<CalendarVM>>();
            }
            return Ok(commonResponse);


            var test = new CommonResponse<string>("test");
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
                    commonResponse.Status = ResponseMessage.success_code;
                    commonResponse.Message = ResponseMessage.bookingConfirmed;
                }
                else
                {
                    commonResponse.Status = ResponseMessage.failure_code;
                    commonResponse.Message = ResponseMessage.bookingConfirmationError;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = ResponseMessage.failure_code;
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
                    commonResponse.Status = ResponseMessage.success_code;
                    commonResponse.Message = ResponseMessage.bookingRejected;
                }
                else
                {
                    commonResponse.Status = ResponseMessage.failure_code;
                    commonResponse.Message = ResponseMessage.bookingRejectionError;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = ResponseMessage.failure_code;
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
                commonResponse.Status = await _bookingService.DeleteEvent(id);
                commonResponse.Message = commonResponse.Status == 1 ? ResponseMessage.bookingDeleted : ResponseMessage.bookingDeleteError;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = ResponseMessage.failure_code;
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
                commonResponse.Status = await _bookingService.TakeCar(bookingId);
                commonResponse.Message = commonResponse.Status == 1 ? ResponseMessage.carTaken : ResponseMessage.carTakingError;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = ResponseMessage.failure_code;
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
                commonResponse.Status = await _bookingService.ReturnCar(bookingId);
                commonResponse.Message = commonResponse.Status == 1 ? ResponseMessage.carReturn : ResponseMessage.carReturningError;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = ResponseMessage.failure_code;
            }

            return Ok(commonResponse);
        }
    }
}
