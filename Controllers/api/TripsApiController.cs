using AmiFlota.Dto;
using AmiFlota.Models.ViewModels;
using AmiFlota.Services;
using AmiFlota.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Controllers.api
{
    [Route("api/Trip")]
    [ApiController]
    public class TripsApiController : Controller
    {
        private readonly ITripService _tripService;

        public TripsApiController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        [Route("GetAllStartLocations")]
        public async Task<IActionResult> GetAllStartLocations()
        {
            CommonResponse<List<string>> commonResponse;
            try
            {
                var result = await _tripService.GetAllStartLocations();
                commonResponse = new CommonResponse<List<string>>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<List<string>>(message: e.Message);
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetAllEndLocations")]
        public async Task<IActionResult> GetAllEndLocations()
        {
            CommonResponse<List<string>> commonResponse;
            try
            {
                var result = await _tripService.GetAllEndLocations();
                commonResponse = new CommonResponse<List<string>>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<List<string>>(message: e.Message);
            }

            return Ok(commonResponse);
        }
    }
}
