using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JoHealth.Models;
using System.Threading.Tasks;

namespace JoHealth.Controllers
{
    public class PatientsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public PatientsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Patient());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Patient model)
        {
            if (ModelState.IsValid)
            {
                // Create the user
                var user = new Patient
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
                    return Redirect("/Identity/Account/Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}
