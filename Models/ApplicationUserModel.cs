using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AmiFlota.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public virtual ICollection<BookingModel> BookingModels { get; set; }
        public virtual int UserForeignKey { get; set; }
    }
}
