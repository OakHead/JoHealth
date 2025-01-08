using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JoHealth.Data;
using JoHealth.Models;

namespace JoHealth.Controllers
{
    public class MedicinesController : Controller
    {
        /* private readonly ApplicationDbContext _context;

        public MedicinesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Medicines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicines.ToListAsync());
        }

        // GET: Medicines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // GET: Medicines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,QuantityAvailable,ImageUrl,IsOnSale,SalePrice")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }

        // GET: Medicines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,QuantityAvailable,ImageUrl,IsOnSale,SalePrice")] Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.Id == id);
        }
        */
        private static List<Medicine> Medicines = new List<Medicine>
        {
            new Medicine
            {
                Id = 1,
                Name = "Panadol",
                Description = "Pain relief medicine.",
                Price = 15.99M,
                QuantityAvailable = 50,
                ImageUrl = "/img/panadol.jpg",
                IsOnSale = false,
                SalePrice = 0M
            },
            new Medicine
            {
                Id = 2,
                Name = "Bodrex Herbal",
                Description = "Herbal supplement for cold and flu.",
                Price = 7.99M,
                QuantityAvailable = 30,
                ImageUrl = "/img/bodrex.jpg",
                IsOnSale = true,
                SalePrice = 5.99M
            },
            new Medicine
            {
                Id = 3,
                Name = "Konidin",
                Description = "Cough relief medicine.",
                Price = 5.99M,
                QuantityAvailable = 20,
                ImageUrl = "/img/konidin.jpg",
                IsOnSale = false,
                SalePrice = 0M
            }
        };
        public IActionResult Index()
        {
            return View(Medicines); // Pass the medicines list to the view
        }

        // Medicine Details
        public IActionResult Details(int id)
        {
            var medicine = Medicines.FirstOrDefault(m => m.Id == id);
            if (medicine == null) return NotFound();
            return View(medicine);
        }

        // Add to Cart
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var medicine = Medicines.FirstOrDefault(m => m.Id == id);
            if (medicine != null && medicine.QuantityAvailable > 0)
            {
                medicine.QuantityAvailable--; // Reduce stock quantity
                // Simulate adding to cart (you can use session or database here)
                TempData["SuccessMessage"] = $"{medicine.Name} added to cart!";
            }
            return RedirectToAction("Details", new { id });
        }
        public IActionResult Cart()
        {
            // Simulate a cart (use session or database for persistence)
            var cartItems = Medicines.Where(m => m.QuantityAvailable < 50).ToList(); // Items added to cart
            return View(cartItems);
        }
    }
}
