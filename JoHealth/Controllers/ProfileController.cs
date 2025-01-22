using JoHealth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

public class ProfileController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public ProfileController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Get logged-in user ID and roles
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return NotFound();

        // Determine role
        var isPatient = await _userManager.IsInRoleAsync(user, "Patient");
        var isDoctor = await _userManager.IsInRoleAsync(user, "Doctor");
        var isPharmacist = await _userManager.IsInRoleAsync(user, "Pharmacist");

        if (isPatient)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == userId);
            ViewData["Role"] = "Patient";
            return View(patient);
        }
        else if (isDoctor)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == userId);
            ViewData["Role"] = "Doctor";
            return View(doctor);
        }
        else if (isPharmacist)
        {
            var pharmacist = await _context.Pharmacists.FirstOrDefaultAsync(ph => ph.Id == userId);
            ViewData["Role"] = "Pharmacist";
            return View(pharmacist);
        }

        return View("Error");
    }
    [HttpGet]
    public async Task<IActionResult> PatientRecords()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var records = await _context.NewRecords
            .Where(r => r.PatientId == userId)
            .ToListAsync();

        return View(records);
    }

    [HttpGet]
    public async Task<IActionResult> DoctorRecords()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var records = await _context.NewRecords
            .Where(r => r.DoctorId == userId)
            .ToListAsync();

        return View(records);
    }

    [HttpGet]
    public async Task<IActionResult> PharmacistRecords()
    {
        var records = await _context.NewRecords
            .Where(r => r.IsApprovedByDoctor && !r.IsAdministeredByPharmacist)
            .ToListAsync();

        return View(records);
    }

    [HttpGet]
    public async Task<IActionResult> PatientProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isPatient = User.IsInRole("Patient");
        var isDoctor = User.IsInRole("Doctor");
        var isPharmacist = User.IsInRole("Pharmacist");

        if (isPatient)
        {
            // Redirect to patient profile edit
            return RedirectToAction("Edit", "Patients");
        }
        else if (isDoctor)
        {
            // Redirect to doctor profile edit
            return RedirectToAction("Edit", "Doctors");
        }
        else if (isPharmacist)
        {
            // Redirect to pharmacist profile edit
            return RedirectToAction("Edit", "Pharmacists");
        }

        return View("Error");
    }

}
