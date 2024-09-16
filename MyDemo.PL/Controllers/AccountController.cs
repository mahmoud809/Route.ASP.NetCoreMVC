﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDemo.DAL.Models;
using MyDemo.PL.ViewModels;
using System.Threading.Tasks;

namespace MyDemo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region Register
        //Account / Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) //1.Server Side Validation Then 2. CLient side
            {
                //Manual Mapping because it's simple mapping
                var mappedUser = new ApplicationUser()
                {
                    FName = model.FName,
                    LName = model.LName,
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    IsAgree = model.IsAgree
                    
                };

                var result = await _userManager.CreateAsync(mappedUser , model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model);
        }
		#endregion

		#region Login

		public IActionResult Login()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        return RedirectToAction("Index" , "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Password");
                }
                ModelState.AddModelError(string.Empty, "Email is not exited");
            }
            return View(model);
		}
		
        
        #endregion
	}
}
