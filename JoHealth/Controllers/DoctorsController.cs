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


        [HttpGet]
        public async Task<IActionResult> Appointments()
        {
            var doctorId = _userManager.GetUserId(User); // Get logged-in doctor's ID
            var records = await _context.Records
                .Where(r => r.DoctorId.ToString() == doctorId)
                .ToListAsync();

            return View(records);
        }
    }
}
