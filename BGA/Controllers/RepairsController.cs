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
    public class RepairsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepairsController(ApplicationDbContext context)
        {
            _context = context;
        }



        // POST: Repairs/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,SerialNumber,Name,Analysis,Comment,LocationComponent,Defect,Client,TesterProcess,Machine,RepairMethod,Pass,Fail")] Repair repair)
        {
            if (ModelState.IsValid)
            {
                repair.LocalDate = DateTime.Now;

                _context.Add(repair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repair);
        }

        [HttpGet]
        public IActionResult Exists(string serialNumber)
        {
            var exists = _context.Repair.Any(r => r.SerialNumber == serialNumber);
            return Json(new { exists = exists });
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            var repairs = await _context.Repair
                                        .OrderByDescending(r => r.Id)
                                        .Take(500)
                                        .ToListAsync();
            return View(repairs);
        }


        // GET: Control
        public async Task<IActionResult> Control()
        {
            var repairs = await _context.Repair
                                        .OrderByDescending(r => r.Id)
                                        .Take(500)
                                        .ToListAsync();
            return View(repairs);
        }



        [HttpGet]
        public IActionResult FilterBySerialNumber(string serialNumber)
        {
            var repairs = _context.Repair.Where(r => r.SerialNumber == serialNumber).ToList();
            return View("Index", repairs);
        }

        [HttpGet]
        public IActionResult FilterBySerialNumberControl(string serialNumber)
        {
            var repairs = _context.Repair.Where(r => r.SerialNumber == serialNumber).ToList();
            return View("Control", repairs);
        }


        [HttpPost]
        public IActionResult UpdatePass(int id, string pass)
        {
            var repair = _context.Repair.Find(id);
            if (repair != null)
            {
                repair.Pass = pass;
                _context.SaveChanges();
            }
            return RedirectToAction("Control");
        }



        // GET: Repairs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Repair == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        // GET: Repairs/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Repairs/Add
        public IActionResult Add()
        {
            return View();
        }

        /*       // POST: Repairs/Create
               [HttpPost]
               [ValidateAntiForgeryToken]
               public async Task<IActionResult> Create([Bind("Id,SerialNumber,Name,Analysis,Comment,LocationComponent,Defect,Client,TesterProcess,Machine,RepairMethod,Pass,Fail")] Repair repair)
               {
                   if (ModelState.IsValid)
                   {
                       // Sprawdzenie, czy suma napraw nie przekracza 8
                       if (!CountRepairs(repair))
                       {
                           repair.LocalDate = DateTime.Now;

                           _context.Add(repair);
                           await _context.SaveChangesAsync();
                           return RedirectToAction(nameof(Index));
                       }
                       else
                       {
                           ModelState.AddModelError(string.Empty, "Suma napraw dla danego numeru seryjnego przekracza 8.");
                           throw new Exception("Suma napraw dla danego numeru seryjnego przekracza 8."); // Rzucenie wyjątku
                       }
                   }
                   return View(repair);
               }*/


        // Słownik przechowujący czas ostatniego dodania rekordu dla danego numeru seryjnego
        private static readonly Dictionary<string, DateTime> _ostatnieDodania = new Dictionary<string, DateTime>();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SerialNumber,Name,Analysis,Comment,LocationComponent,Defect,Client,TesterProcess,Machine,RepairMethod,Pass,Fail")] Repair repair)
        {
            if (ModelState.IsValid)
            {
                lock (_ostatnieDodania)
                {
                    if (_ostatnieDodania.TryGetValue(repair.SerialNumber, out DateTime ostatnieDodanie))
                    {
                        if ((DateTime.Now - ostatnieDodanie).TotalSeconds < 5)
                        {
                            ModelState.AddModelError(string.Empty, "Odczekaj 5 sekund przed ponownym dodaniem rekordu dla tego numeru seryjnego.");
                            return View(repair);
                        }
                    }
                    _ostatnieDodania[repair.SerialNumber] = DateTime.Now;
                }

                // Sprawdzenie, czy suma napraw nie przekracza 8
                if (!CountRepairs(repair))
                {
                    repair.LocalDate = DateTime.Now;

                    _context.Add(repair);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Suma napraw dla danego numeru seryjnego przekracza 8.");
                    return View(repair);
                }
            }
            return View(repair);
        }








        // Metoda do zliczania napraw dla danego numeru seryjnego
        private bool CountRepairs(Repair newRepair)
        {
            var repairList = _context.Repair
                .Where(r => r.SerialNumber == newRepair.SerialNumber)
                .ToList();

            var countReplacement = repairList
                .Count(repair => repair.RepairMethod == "COMPONENT_REPLACEMENT");

            var countSoldering = repairList
                .Count(repair => repair.RepairMethod == "SOLDERING_COMPONENTS");

            var countRemoval = repairList
                .Count(repair => repair.RepairMethod == "COMPONENT_REMOVAL");

            return countRemoval + countSoldering + (countReplacement * 2) >= 8;
        }


        // GET: Repairs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Repair == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair.FindAsync(id);
            if (repair == null)
            {
                return NotFound();
            }
            return View(repair);
        }

        // GET: Repairs/Edit/5
        public async Task<IActionResult> ControlEdit(long? id)
        {
            if (id == null || _context.Repair == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair.FindAsync(id);
            if (repair == null)
            {
                return NotFound();
            }
            return View(repair);
        }



        // POST: Repairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ControlEdit(long id, [Bind("Id,SerialNumber,Name,Analysis,Comment,LocationComponent,Defect,Client,TesterProcess,Machine,RepairMethod,LocalDate,Pass,Fail")] Repair repair)
        {
            if (id != repair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairExists(repair.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Control));
            }
            return View(repair);
        }






        // POST: Repairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,SerialNumber,Name,Analysis,Comment,LocationComponent,Defect,Client,TesterProcess,Machine,RepairMethod,LocalDate,Pass,Fail")] Repair repair)
        {
            if (id != repair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairExists(repair.Id))
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
            return View(repair);
        }

        // GET: Repairs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Repair == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Repair == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Repair'  is null.");
            }
            var repair = await _context.Repair.FindAsync(id);
            if (repair != null)
            {
                _context.Repair.Remove(repair);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairExists(long id)
        {
            return (_context.Repair?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
