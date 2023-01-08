using AmiFlota.Contracts;
using AmiFlota.Dto;
using AmiFlota.Models;
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

        public UsersApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            CommonResponse<List<dtoUser>> commonResponse;
            try
            {
                var result = _userService.GetAllUsers();
                commonResponse = new CommonResponse<List<dtoUser>>(result);
            }
            catch (Exception e)
            {
                commonResponse = new CommonResponse<List<dtoUser>>(message: e.Message);
            }

            return Ok(commonResponse);
        }
    }
}
