using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GH.Models;
using Microsoft.AspNetCore.Authorization;

namespace GH.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUserModel> userMngr,
            RoleManager<IdentityRole> roleMngr)
        {
            _userManager = userMngr;
            _roleManager = roleMngr;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUserModel> users = new List<AppUserModel>();
            foreach (AppUserModel user in _userManager.Users.ToList())
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = _roleManager.Roles
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View("../Account/Register");
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUserModel { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("../Account/Register", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUserModel user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded) // if failed
                {
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. "
                    + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUserModel user = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUserModel user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            // TODO: Do something with the result or get rid of the bogus if
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

    }
}