using JoHealth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Get logged-in user ID
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        string firstName = "User"; // Default value
        string imageUrl = "~/img/placeholder-profile.png"; // Default image

        // Check if the user is a Patient, Doctor, or Pharmacist
        if (await _context.Patients.AnyAsync(p => p.Id == userId))
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == userId);
            firstName = patient?.FirstName ?? firstName;
            imageUrl = patient?.ImageUrl ?? imageUrl;
        }
        else if (await _context.Doctors.AnyAsync(d => d.Id == userId))
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == userId);
            firstName = doctor?.FirstName ?? firstName;
            imageUrl = doctor?.ImageUrl ?? imageUrl;
        }
        else if (await _context.Pharmacists.AnyAsync(ph => ph.Id == userId))
        {
            var pharmacist = await _context.Pharmacists.FirstOrDefaultAsync(ph => ph.Id == userId);
            firstName = pharmacist?.FirstName ?? firstName;
            imageUrl = pharmacist?.ImageUrl ?? imageUrl;
        }

        // Fetch the top 3 doctors and articles
        var doctors = await _context.Doctors.OrderBy(d => d.Id).Take(3).ToListAsync();       

        // Pass data to the view
        ViewBag.FirstName = firstName;
        ViewBag.ImageUrl = imageUrl;
        return View(doctors);
    }
}
