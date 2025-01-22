using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoHealth.Data;
using JoHealth.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

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
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ImageUrl = model.ImageUrl,
                    BloodType = model.BloodType,
                    Weight = model.Weight
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Patient"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Patient"));
                    }

                    await _userManager.AddToRoleAsync(user, "Patient");
                    return Redirect($"/Identity/Account/RegisterConfirmation?email={user.Email}");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Logs the user out
            return RedirectToAction("Index", "Home"); // Redirect to the home page
        }

        [HttpGet]
        public IActionResult SubmitRecord()
        {
            ViewBag.Doctors = _context.Doctors.Select(d => new
            {
                Id = d.Id,
                FullName = $"{d.FirstName} {d.LastName}"
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRecord(NewRecord model)
        {
            if (ModelState.IsValid)
            {
                // Add CreatedAt timestamp
                model.CreatedAt = DateTime.Now;

                // Save the record to the database
              //  _context.NewRecord.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); // Redirect to home or a success page
            }

            return View(model); // Return the form with validation errors if any
        }

    }
}
