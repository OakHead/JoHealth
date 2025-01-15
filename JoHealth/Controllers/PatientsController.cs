using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoHealth.Data;
using JoHealth.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JoHealth.Controllers
{
    public class PatientsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PatientsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Patient()); // Render the registration form for a Patient
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Patient model)
        {
            if (ModelState.IsValid)
            {
                var user = new Patient
                {
                    UserName = model.Email, // IdentityUser property
                    Email = model.Email,    // IdentityUser property
                    FirstName = model.FirstName, // Custom property
                    LastName = model.LastName,   // Custom property
                    ImageUrl = model.ImageUrl    // Custom property
                };

                // Create the user
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Check if the "Patient" role exists
                    var roleExists = await _context.Roles.AnyAsync(r => r.Name == "Patient");
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Patient")); // Create the role if it doesn't exist
                    }

                    // Assign the "Patient" role to the user
                    await _userManager.AddToRoleAsync(user, "Patient");

                    // Redirect to the confirmation page
                    return Redirect($"/Identity/Account/RegisterConfirmation?email={user.Email}");
                }

                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model); // Return the view with validation errors
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Logs the user out
            return RedirectToAction("Index", "Home"); // Redirect to the home page
        }

    }
}
