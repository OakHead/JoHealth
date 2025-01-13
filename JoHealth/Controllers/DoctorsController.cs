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
        private readonly ApplicationDbContext _context;

        public DoctorsController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
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
                // Create the doctor
                var user = new Doctor
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Specialty = model.Specialty,
                    ImageUrl = model.ImageUrl
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Redirect("/Identity/Account/Login"); // Redirect to login after successful registration
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
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
        public async Task<IActionResult> Details(String id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound(); // Return 404 if the doctor doesn't exist
            }

            return View(doctor); // Return the doctor details view
        }
    }
}
