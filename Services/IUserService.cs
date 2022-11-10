using AmiFlota.Dto;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Services
{
    public interface IUserService
    {
        public List<dtoUser> GetAllUsers();
    }
}
