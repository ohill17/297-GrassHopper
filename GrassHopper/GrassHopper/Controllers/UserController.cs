using GrassHopper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrassHopper.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private UserManager<AppUser> userManager;
		private RoleManager<IdentityRole> roleManager;

		public UserController(UserManager<AppUser> userMngr,
			RoleManager<IdentityRole> roleMngr)
		{
			userManager = userMngr;
			roleManager = roleMngr;
		}

		public async Task<IActionResult> Index()
		{
			List<AppUser> users = new List<AppUser>();
			foreach (AppUser user in userManager.Users.ToList())
			{
				user.RoleNames = await userManager.GetRolesAsync(user);
				users.Add(user);
			}
			UserVM model = new UserVM
			{
				Users = users,
				Roles = roleManager.Roles.OrderBy(r => r.Name)
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			AppUser user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await userManager.DeleteAsync(user);
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
		public async Task<IActionResult> AddToRole(string id, string roleName)
		{
			IdentityRole role = await roleManager.FindByNameAsync(roleName);
			if (role == null)
			{
				TempData["message"] = "Role '" + roleName + "' does not exist. "
					+ "Click 'Create Role' button to create it.";
			}
			else
			{
				AppUser user = await userManager.FindByIdAsync(id);
				await userManager.AddToRoleAsync(user, role.Name);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromRole(string id, string roleName)
		{
			AppUser user = await userManager.FindByIdAsync(id);
			var result = await userManager.RemoveFromRoleAsync(user, roleName);
			if (result.Succeeded) { }
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRole(string id)
		{
			IdentityRole role = await roleManager.FindByIdAsync(id);
			var result = await roleManager.DeleteAsync(role);
			if (result.Succeeded) { }
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(string roleName)
		{
			var result = await roleManager.CreateAsync(new IdentityRole(roleName));
			if (result.Succeeded) { }
			return RedirectToAction("Index");
		}
	}
}
