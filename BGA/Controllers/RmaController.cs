using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BGA.Entites;

namespace BGA.Controllers
{
    public class RmaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RmaController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Index
        public async Task<IActionResult> Index()
        {
            var repairs = await _context.Rma
                                        .OrderByDescending(r => r.Id)
                                        .Take(500)
                                        .ToListAsync();
            return View(repairs);
        }



        // GET: Rma/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Rma == null)
            {
                return NotFound();
            }

            var rma = await _context.Rma
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rma == null)
            {
                return NotFound();
            }

            return View(rma);
        }

        // GET: Rma/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SerialNumber,Name,family,Comment,Client,duration")] Rma rma)
        {
            if (ModelState.IsValid)
            {
                rma.LocalDate = DateTime.Now;
                _context.Add(rma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rma);
        }

        // GET: Rma/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Rma == null)
            {
                return NotFound();
            }

            var rma = await _context.Rma.FindAsync(id);
            if (rma == null)
            {
                return NotFound();
            }
            return View(rma);
        }

        // POST: Rma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,SerialNumber,Name,family,Comment,Client,duration,LocalDate")] Rma rma)
        {
            if (id != rma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RmaExists(rma.Id))
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
            return View(rma);
        }

        // GET: Rma/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Rma == null)
            {
                return NotFound();
            }

            var rma = await _context.Rma
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rma == null)
            {
                return NotFound();
            }

            return View(rma);
        }

        [HttpGet]
        public IActionResult Exists(string serialNumber)
        {
            var exists = _context.Rma.Any(r => r.SerialNumber == serialNumber);
            return Json(new { exists = exists });
        }

        [HttpGet]
        public IActionResult FilterBySerialNumber(string serialNumber)
        {
            var rma = _context.Rma.Where(r => r.SerialNumber == serialNumber).ToList();
            return View("Index", rma);
        }


        // POST: Rma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Rma == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rma'  is null.");
            }
            var rma = await _context.Rma.FindAsync(id);
            if (rma != null)
            {
                _context.Rma.Remove(rma);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RmaExists(long id)
        {
            return (_context.Rma?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
