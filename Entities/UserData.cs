using AmiFlota.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Entities
{
    public class UserData : IUserData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public UserData(IHttpContextAccessor httpContextAccessor)
        {
            Id = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Name = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            Role = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        public bool IsPriviledgedUser()
        {
            return Role.Equals(UserRole.admin.ToString()) 
                || Role.Equals(UserRole.manager.ToString());
        }
    }


}
