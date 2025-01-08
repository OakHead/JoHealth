using Microsoft.AspNetCore.Mvc;
using JoHealth.Models;
using JoHealth.Data; // Replace with your namespace for ApplicationDbContext
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JoHealth.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
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
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound(); // Return 404 if the doctor doesn't exist
            }

            return View(doctor); // Return the doctor details view
        }
    }
}
