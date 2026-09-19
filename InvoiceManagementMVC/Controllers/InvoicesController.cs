using InvoiceManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementMVC.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoicesContext _context;

        public InvoicesController(InvoicesContext context)
        {
            _context = context;
        }


        // GET: Invoices
        public async Task<IActionResult> Index(string? search)
        {
            var query = _context.Invoices
                .Include(invoice => invoice.InvoiceItems)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                if (int.TryParse(search, out int invoiceNumber))
                {
                    query = query.Where(
                        invoice => invoice.InvoiceNumber == invoiceNumber
                    );
                }
                else
                {
                    query = query.Where(
                        invoice => invoice.InvoiceItems
                            .Any(item =>
                                item.Title != null &&
                                item.Title.Contains(search)
                            )
                    );
                }
            }

            var invoices = await query
                .OrderByDescending(invoice => invoice.DateOfIssue)
                .ToListAsync();

            ViewBag.Search = search;

            ViewBag.TotalInvoices = await _context.Invoices.CountAsync();

            ViewBag.TotalItems = await _context.InvoiceItems.CountAsync();

            var allInvoices = await _context.Invoices
                .Include(invoice => invoice.InvoiceItems)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.TotalValue = allInvoices
                .Sum(invoice => invoice.Total() ?? 0);

            return View(invoices);
        }


        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(invoice => invoice.InvoiceItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    invoice => invoice.InvoiceNumber == id
                );

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }


        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("InvoiceNumber,DateOfIssue")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(invoice);
        }


        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }


        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("InvoiceNumber,DateOfIssue")] Invoice invoice)
        {
            if (id != invoice.InvoiceNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InvoiceExistsAsync(
                        invoice.InvoiceNumber))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(invoice);
        }


        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    invoice => invoice.InvoiceNumber == id
                );

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }


        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices
                .Include(invoice => invoice.InvoiceItems)
                .FirstOrDefaultAsync(
                    invoice => invoice.InvoiceNumber == id
                );

            if (invoice != null)
            {
                _context.InvoiceItems.RemoveRange(
                    invoice.InvoiceItems
                );

                _context.Invoices.Remove(invoice);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private Task<bool> InvoiceExistsAsync(int id)
        {
            return _context.Invoices
                .AnyAsync(invoice =>
                    invoice.InvoiceNumber == id
                );
        }
    }
}