using AmiFlota.Dto;
using AmiFlota.Models;
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
    [Route("api/Users")]
    [ApiController]
    public class UsersApiController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UsersApiController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            CommonResponse<List<dtoUser>> commonResponse = new CommonResponse<List<dtoUser>>();
            try
            {
                commonResponse.dataenum = _userService.GetAllUsers();
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
