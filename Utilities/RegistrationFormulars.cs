using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Utilities
{
    public class RegistrationFormulars
    {
        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {

            if (isAdmin)
            {
                return new List<SelectListItem> {
                new SelectListItem { Value = UserRole.admin.ToString(), Text=UserRole.admin.ToString()},
                new SelectListItem { Value = UserRole.manager.ToString(), Text = UserRole.manager.ToString() },
                new SelectListItem { Value = UserRole.user.ToString(), Text = UserRole.user.ToString() }
            };
            }

            else
            {
                return new List<SelectListItem> {
                    //TODO Delete option to create admin user
                        new SelectListItem { Value = UserRole.admin.ToString(), Text=UserRole.admin.ToString()},
                        new SelectListItem { Value = UserRole.manager.ToString(), Text = UserRole.manager.ToString() },
                        new SelectListItem { Value = UserRole.user.ToString(), Text = UserRole.user.ToString() }
                    };

            };
        }
    }
}
