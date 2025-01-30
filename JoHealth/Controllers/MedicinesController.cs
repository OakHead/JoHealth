using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JoHealth.Data;
using JoHealth.Models;
using System.Security.Claims;

namespace JoHealth.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicinesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private static List<Medicine> Medicines = new List<Medicine>
        {
            new Medicine
            {
                Id = 1,
                Name = "Panadol",
                Description = "Pain relief medicine.",
                Price = 15.99M,
                QuantityAvailable = 50,
                ImageUrl = "/img/Panadol.jpg",
                IsOnSale = false,
                SalePrice = 0M
            },
            new Medicine
            {
                Id = 2,
                Name = "Ibuprofen",
                Description = "Painkiller for adults",
                Price = 7.99M,
                QuantityAvailable = 30,
                ImageUrl = "/img/Ibuprofen.jpg",
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get the logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user has any submitted records
            var hasSubmittedRecords = await _context.NewRecords.AnyAsync(r => r.PatientId == userId);

            // Pass the flag and pre-set medicine data to the view
            ViewData["HasSubmittedRecords"] = hasSubmittedRecords;

            // Mock medicines list or fetch it from a source
            var medicines = new List<Medicine>
    {
        new Medicine { Id = 1, Name = "Paracetamol", Price = 10.00m, SalePrice = 8.00m, IsOnSale = true, QuantityAvailable = 50, ImageUrl = "/img/Panadol.jpg" },
        new Medicine { Id = 2, Name = "Ibuprofen", Price = 15.00m, SalePrice = 0, IsOnSale = false, QuantityAvailable = 30, ImageUrl = "/img/Ibuprofen.jpg" },
        // Add more mock data here
    };

            return View(medicines);
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
        // List of approved records for pharmacists
        [HttpGet]
        public async Task<IActionResult> Pharmacy()
        {
            var approvedRecords = await _context.NewRecords
                .Where(r => r.IsApprovedByDoctor && !r.IsAdministeredByPharmacist)
                .ToListAsync();

            return View(approvedRecords);
        }

        // View the record details
        [HttpGet]
        public async Task<IActionResult> RecordDetails(int id)
        {
            var record = await _context.NewRecords.FirstOrDefaultAsync(r => r.Id == id);
            if (record == null)
            {
                TempData["ErrorMessage"] = "Record not found.";
                return RedirectToAction("Pharmacy");
            }

            return View(record);
        }

        // Approve or Deny a record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveOrDeny(int id, bool isApproved)
        {
            var record = await _context.NewRecords.FirstOrDefaultAsync(r => r.Id == id);
            if (record == null)
            {
                TempData["ErrorMessage"] = "Record not found.";
                return RedirectToAction("Pharmacy");
            }

            // Update the record status based on pharmacist's action
            record.IsAdministeredByPharmacist = isApproved;
            _context.Update(record);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = isApproved
                ? "The record has been approved successfully."
                : "The record has been denied.";

            return RedirectToAction("Pharmacy");
        }

    }
}
