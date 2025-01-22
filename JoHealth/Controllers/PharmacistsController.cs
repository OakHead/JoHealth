using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JoHealth.Models;
using Microsoft.EntityFrameworkCore;
using JoHealth.Data;

public class PharmacistsController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public PharmacistsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new Pharmacist());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Pharmacist model)
    {
        if (ModelState.IsValid)
        {
            var user = new Pharmacist
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roleExists = await _context.Roles.AnyAsync(r => r.Name == "Pharmacist");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Pharmacist")); // Create the role if it doesn't exist
                }

                await _userManager.AddToRoleAsync(user, "Pharmacist");
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
}
