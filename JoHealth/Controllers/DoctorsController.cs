﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoHealth.Data;
using JoHealth.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace JoHealth.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DoctorsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Doctor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Doctor model)
        {
            if (ModelState.IsValid)
            {
                var user = new Doctor
                {
                    UserName = model.Email, // IdentityUser property
                    Email = model.Email, // IdentityUser property
                    FirstName = model.FirstName, // Custom property
                    LastName = model.LastName, // Custom property
                    Specialty = model.Specialty, // Custom property
                    ImageUrl = model.ImageUrl // Custom property
                };

                // Create the user
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Check if the "Doctor" role exists
                    var roleExists = await _context.Roles.AnyAsync(r => r.Name == "Doctor");
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Doctor")); // Create the role if it doesn't exist
                    }

                    // Assign the "Doctor" role to the user
                    await _userManager.AddToRoleAsync(user, "Doctor");

                    // Redirect to confirmation page
                    return Redirect($"/Identity/Account/RegisterConfirmation?email={user.Email}");
                }

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

        public IActionResult Create()
        {
            return View(); // Returns the Create view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Specialty,ImageUrl")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                doctor.Id = Guid.NewGuid().ToString();
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Booking
        public async Task<IActionResult> Booking()
        {
            ViewData["ActiveNav"] = "Booking"; // Set the active navigation
            var doctors = await _context.Doctors.ToListAsync(); // Fetch all doctors
            return View(doctors);
        }

        // GET: Doctors/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(string id) // Use the IdentityUser's Id
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor); // Pass the doctor object to the view
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public async Task<IActionResult> Appointments()
        {
            // Get the logged-in doctor's ID
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch only the records assigned to this doctor
            var records = await _context.NewRecords
                .Where(r => r.DoctorId == doctorId)
                .ToListAsync();

            return View(records); // Pass filtered records to the view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRecord(int recordId)
        {
            var record = await _context.NewRecords.FirstOrDefaultAsync(r => r.Id == recordId);
            if (record != null)
            {
                record.IsApprovedByDoctor = true;
                _context.Update(record);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Record approved successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Record not found!";
            }

            // Redirect back to Appointments view
            return RedirectToAction("Details", "Appointments", new { id = recordId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRecord(int recordId, string prescriptions)
        {
            var record = await _context.NewRecords.FirstOrDefaultAsync(r => r.Id == recordId);
            if (record == null)
            {
                TempData["ErrorMessage"] = "The record does not exist.";
                return RedirectToAction("Details", "Appointments");
            }

            // Update prescriptions
            record.Prescriptions = prescriptions;
            _context.Update(record);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Prescriptions have been updated.";
            return RedirectToAction("Details", "Appointments", new { id = recordId });
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == userId);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor); // Pass patient entity directly to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(Doctor model, IFormFile ImageFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch the user from the database
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == userId);
            if (doctor == null)
            {
                TempData["ErrorMessage"] = "Failed to save changes.";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // Update fields
                doctor.FirstName = model.FirstName;
                doctor.LastName = model.LastName;

                // Handle image upload
                if (ImageFile != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    doctor.ImageUrl = $"/img/{ImageFile.FileName}";
                }

                // Save changes to the database
                _context.Update(doctor);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to save changes.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
