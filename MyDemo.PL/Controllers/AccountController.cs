using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDemo.DAL.Models;
using MyDemo.PL.Helpers;
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

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region ForgetPassword

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); //==> Token will be valid just for this User only one time

                    //shape of Link => https://{Request.schema}/Account/ResetPassword?email=mahmoud@gmail.com&tokend=212sfsdfsfdsd

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token} , Request.Scheme);


                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = user.Email,
                        Body = passwordResetLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                    
                }
                ModelState.AddModelError(string.Empty, "Email is not existed");
            }
            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);

                if (user is not null)
                {
					var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

					if (result.Succeeded)
						return RedirectToAction(nameof(Login));

					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
            }
            return View(model);
		}

        #endregion

        #region Access Denied
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
