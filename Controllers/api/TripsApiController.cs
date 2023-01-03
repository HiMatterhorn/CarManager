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
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TripsApiController(ITripService tripService, IHttpContextAccessor httpContextAccessor)
        {
            _tripService = tripService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetAllStartLocations")]
        public IActionResult GetAllStartLocations()
        {
            CommonResponse<List<string>> commonResponse = new CommonResponse<List<string>>();
            try
            {
                commonResponse.Data = _tripService.GetAllStartLocations();
                commonResponse.Status = ResponseMessage.success_code;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = ResponseMessage.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetAllEndLocations")]
        public IActionResult GetAllEndLocations()
        {
            CommonResponse<List<string>> commonResponse = new CommonResponse<List<string>>();
            try
            {
                commonResponse.Data = _tripService.GetAllEndLocations();
                commonResponse.Status = ResponseMessage.success_code;
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
