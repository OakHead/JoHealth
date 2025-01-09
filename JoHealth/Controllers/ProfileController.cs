using Microsoft.AspNetCore.Mvc;
using JoHealth.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using JoHealth.Data;

namespace JoHealth.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the currently logged-in user
            var userId = _userManager.GetUserId(User);

            // Fetch the patient record from the database (assuming the Patient model is tied to IdentityUser)
            var patient = _context.Patients.FirstOrDefault(p => p.Id == userId);

            if (patient == null)
            {
                return NotFound("Patient profile not found.");
            }

            return View(patient);
        }
    }
}
