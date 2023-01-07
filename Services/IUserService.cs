using AmiFlota.Dto;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface IUserService
    {
        public List<dtoUser> GetAllUsers();
    }
}
