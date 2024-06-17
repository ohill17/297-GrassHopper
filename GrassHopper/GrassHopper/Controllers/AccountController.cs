using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GrassHopper.Models;
using Microsoft.Extensions.Options;
using GrassHopper.Data;
using System.Web;

namespace Grasshopper.Controllers
{
    public class AccountController : Controller
	{
		private UserManager<AppUser> userManager;
		private SignInManager<AppUser> signInManager;
        private readonly AppSettings _appSettings;

        public AccountController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr, IOptions<AppSettings> appSettings)
		{
			userManager = userMngr;
			signInManager = signInMngr;
            _appSettings = appSettings.Value;
        }

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUser { UserName = model.Username };
				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> LogOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult LogIn(string returnURL = "")
		{
			var model = new LoginVM { ReturnUrl = returnURL };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LoginVM model)
		{
            model.FacebookAppId = _appSettings.FacebookAppId;
            model.FacebookAppSecret = _appSettings.FacebookAppSecret;

            if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(
					model.Username, model.Password, isPersistent: model.RememberMe,
					lockoutOnFailure: false);

				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.ReturnUrl) &&
						Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}
			ModelState.AddModelError("", "Invalid username/password.");

            return View(model);
		}

		public IActionResult FacebookLogin()
		{
            ReviewsVM reviewsVM = new()
            {
                FacebookAppId = _appSettings.FacebookAppId,
                FacebookAppSecret = _appSettings.FacebookAppSecret,
                FacebookRedirectUri = _appSettings.FacebookRedirectUri
            };

            //checking if a user is logged in
            if (User.IsInRole("Admin"))
            {
                ViewData["IsAdmin"] = "true";
            }
            else
            {
                ViewData["IsAdmin"] = "false";
            }

            return View(reviewsVM);
        }

        public IActionResult ChangePassword()
		{
			var model = new ChangePasswordVM
			{
				Username = User.Identity?.Name ?? ""
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
		{
			if (ModelState.IsValid)
			{
				AppUser user = await userManager.FindByNameAsync(model.Username);
				var result = await userManager.ChangePasswordAsync(user,
					model.OldPassword, model.NewPassword);

				if (result.Succeeded)
				{
					TempData["message"] = "Password changed successfully";
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(model);
		}
	}
}