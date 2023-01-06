using AmiFlota.Contracts;
using AmiFlota.Data;
using AmiFlota.Entities;
using AmiFlota.Enums;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;


namespace AmiFlota.Controllers
{
    public class AccountController : Controller
    {
        private readonly AmiFlotaContext _db;
        private readonly IUserData _userData;
        UserManager<ApplicationUserModel> _userManager;
        SignInManager<ApplicationUserModel> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        

        public AccountController(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager, RoleManager<IdentityRole> roleManager, IUserData userData)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userData = userData;
        }

        public async Task<IActionResult> Register()
        {
            var adminList = _userManager.GetUsersInRoleAsync(UserRole.Admin.ToString()).GetAwaiter().GetResult().ToList();

            if (!_roleManager.RoleExistsAsync(UserRole.Admin.ToString()).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Manager.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(UserRole.User.ToString()));
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUserModel
                {
                    UserName = model.Name,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    if(!_userData.IsAdminUser())
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        TempData["newAdminSignedUp"] = user.UserName;
                    }

                    return RedirectToAction("Search", "Booking");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            //TODO Redirection
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    //Set Seesion Data
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    HttpContext.Session.SetString("userEmailAssigned", user.Email);

                    //Retrieve Session Dataprivate r
                    // var userEmail = HttpContext.Session.GetString("userEmailAssigned");

                    return RedirectToAction("Search", "Booking");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }


}
