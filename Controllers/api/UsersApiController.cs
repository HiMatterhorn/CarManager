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

        public UsersApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            CommonResponse<List<dtoUser>> commonResponse;
            try
            {
                var result = await _userService.GetAllUsers();
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
