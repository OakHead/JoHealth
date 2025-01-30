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
                // Validate the doctor exists
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == model.DoctorId);
                if (doctor == null)
                {
                    TempData["ErrorMessage"] = "The selected doctor does not exist.";
                    return RedirectToAction("Details", "Doctors", new { id = model.DoctorId });
                }

                // Get the logged-in patient's ID
                var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.PatientId = patientId;

                // Save the record
                _context.NewRecords.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Your record has been successfully submitted!";
                return RedirectToAction("Details", "Doctors", new { id = model.DoctorId });
            }

            TempData["ErrorMessage"] = "There was a problem submitting your record. Please try again.";
            return RedirectToAction("Details", "Doctors", new { id = model.DoctorId });
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == userId);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient); // Pass patient entity directly to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(Patient model, IFormFile ImageFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == userId);

            if (patient == null)
            {
                return NotFound();
            }

            // Update fields
            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;

            // Handle image upload
            if (ImageFile != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                patient.ImageUrl = $"/img/{ImageFile.FileName}";
            }

            _context.Update(patient);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Index", "Profile");
        }

    }
}
