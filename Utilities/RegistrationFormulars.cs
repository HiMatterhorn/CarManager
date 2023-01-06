using AmiFlota.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace AmiFlota.Utilities
{
    public class RegistrationFormulars
    {
        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {

            if (isAdmin)
            {
                return new List<SelectListItem> {
                new SelectListItem { Value = UserRole.Admin.ToString(), Text=UserRole.Admin.ToString()},
                new SelectListItem { Value = UserRole.Manager.ToString(), Text = UserRole.Manager.ToString() },
                new SelectListItem { Value = UserRole.User.ToString(), Text = UserRole.User.ToString() }
            };
            }

            else
            {
                return new List<SelectListItem> {
                    //TODO Delete option to create admin user
                        new SelectListItem { Value = UserRole.Admin.ToString(), Text=UserRole.Admin.ToString()},
                        new SelectListItem { Value = UserRole.Manager.ToString(), Text = UserRole.Manager.ToString() },
                        new SelectListItem { Value = UserRole.User.ToString(), Text = UserRole.User.ToString() }
                    };

            };
        }
    }
}
