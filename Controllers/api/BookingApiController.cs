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

        public BookingApiController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("GetCalendarDataForCarList")]
        public IActionResult GetCalendarDataForCarList([FromBody] dtoVIN dtoSelectedCars)
        {
            CommonResponse<List<CalendarVM>> commonResponse;
            try
            {
                var result = _bookingService.AllBookingsByCarVinList(dtoSelectedCars.Selected);
                commonResponse = new CommonResponse<List<CalendarVM>>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<List<CalendarVM>>(message: e.Message);
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ConfirmEvent")]
        public async Task<IActionResult> ConfirmEvent(int id)
        {
            CommonResponse<int> commonResponse;
            try
            {
                var result = await _bookingService.ConfirmEvent(id);
                commonResponse = new CommonResponse<int>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<int>(message:e.Message);
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("RejectEvent")]
        public async Task<IActionResult> RejectEvent(int id)
        {
            CommonResponse<int> commonResponse;
            try 
            {
                var result = await _bookingService.RejectEvent(id);
                commonResponse = new CommonResponse<int>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<int>(message: e.Message);
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            CommonResponse<int> commonResponse;
            try
            {
                var result = await _bookingService.DeleteEvent(id);
                commonResponse = new CommonResponse<int>(result);

            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<int>(message: e.Message);
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("TakeCar")]
        public async Task<IActionResult> TakeCar(int bookingId)
        {
            CommonResponse<int> commonResponse;
            try
            {
                var result = await _bookingService.TakeCar(bookingId);
                commonResponse = new CommonResponse<int>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<int>(message: e.Message);
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ReturnCar")]
        public async Task<IActionResult> ReturnCar(int bookingId)
        {
            CommonResponse<int> commonResponse;
            try
            {
                var result = await _bookingService.ReturnCar(bookingId);
                commonResponse = new CommonResponse<int>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<int>(message: e.Message);
            }

            return Ok(commonResponse);
        }
    }
}
