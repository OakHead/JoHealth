using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JoHealth.Models;
using System.Threading.Tasks;
using global::JoHealth.Models;

namespace JoHealth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register(string role = "Patient") // Default to Patient
        {
            ViewBag.Role = role; // Pass the role to the view
            return View("CreateAcc/Register", new RegisterViewModel()); // Render the shared view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string role, RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the user
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Ensure the role exists
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }

                    // Assign the role to the user
                    await _userManager.AddToRoleAsync(user, role);

                    // Optional: Save additional user details
                    // (You can implement this in your custom user models if needed)

                    // Redirect to the RegisterConfirmation page
                    return Redirect($"/Identity/Account/RegisterConfirmation?email={user.Email}");
                }

                // Add errors if registration fails
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.Role = role; // Retain the role for the view
            return View("CreateAcc/Register", model); // Return to the shared view with errors
        }
    }
}

