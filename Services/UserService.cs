using AmiFlota.Data;
using AmiFlota.Dto;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public class UserService : IUserService
    {

        private readonly AmiFlotaContext _db;
        private readonly UserManager<ApplicationUserModel> _userManager;

        public UserService(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public List<dtoUser> GetAllUsers()
        {
            var result = (from u in _db.Users.ToList()
                         select new dtoUser()
                         {
                             Id = u.Id,
                             Name = u.UserName
                         }).ToList();

            return result;
        }

    }
}
