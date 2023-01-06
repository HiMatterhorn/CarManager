using AmiFlota.Contracts;
using AmiFlota.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


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
            return Role.Equals(UserRole.Admin.ToString()) 
                || Role.Equals(UserRole.Manager.ToString());
        }
    }


}
