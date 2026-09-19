using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceManagementMVC.Models;

namespace InvoiceManagementMVC.Controllers
{
    public class InvoiceItemsController : Controller
    {
        private readonly InvoicesContext _context;

        public InvoiceItemsController(InvoicesContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE - GET
        // =========================

        public async Task<IActionResult> Create(int invoiceNumber)
        {
            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);

            if (invoice == null)
            {
                return NotFound();
            }

            ViewBag.InvoiceNumber = invoiceNumber;

            return View();
        }

        // =========================
        // CREATE - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int invoiceNumber,
            string title,
            decimal quantity,
            decimal price)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "Item title is required.");
            }

            if (quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be greater than 0.");
            }

            if (price < 0)
            {
                ModelState.AddModelError("Price", "Price cannot be negative.");
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);

            if (invoice == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.InvoiceNumber = invoiceNumber;
                return View();
            }

            var item = new InvoiceItem
            {
                InvoiceNumber = invoiceNumber,
                Title = title.Trim(),
                Quantity = quantity,
                Price = price
            };

            _context.InvoiceItems.Add(item);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "Invoices",
                new { id = invoiceNumber }
            );
        }

        // =========================
        // EDIT - GET
        // =========================

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.InvoiceItems
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // =========================
        // EDIT - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            string title,
            decimal quantity,
            decimal price)
        {
            var item = await _context.InvoiceItems
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "Item title is required.");
            }

            if (quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be greater than 0.");
            }

            if (price < 0)
            {
                ModelState.AddModelError("Price", "Price cannot be negative.");
            }

            if (!ModelState.IsValid)
            {
                item.Title = title;
                item.Quantity = quantity;
                item.Price = price;

                return View(item);
            }

            item.Title = title.Trim();
            item.Quantity = quantity;
            item.Price = price;

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "Invoices",
                new { id = item.InvoiceNumber }
            );
        }

        // =========================
        // DELETE - GET
        // =========================

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.InvoiceItems
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // =========================
        // DELETE - POST
        // =========================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.InvoiceItems
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            int invoiceNumber = item.InvoiceNumber;

            _context.InvoiceItems.Remove(item);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "Invoices",
                new { id = invoiceNumber }
            );
        }
    }
}