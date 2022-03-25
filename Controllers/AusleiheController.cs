using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Buecherei.Data;
using Buecherei.Models;

namespace Buecherei.Controllers
{
    public class AusleiheController : Controller
    {
        private readonly BuechereiDBContext _context;

        public AusleiheController(BuechereiDBContext context)
        {
            _context = context;
        }

        // GET: Ausleihe
        public async Task<IActionResult> Index(string searchString)
        {
            var buechereiDBContext = _context.Ausleihe.Include(a => a.Buch).Include(a => a.SchülerIn);

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = _context.Ausleihe.Include(a => a.Buch).Include(a => a.SchülerIn).Where(s => s.Buchnummer!.Contains(searchString));
                ViewData["delFilter"] = "true";
                return View(await search.ToListAsync());
            }

            ViewData["delFilter"] = "false";
            return View(await buechereiDBContext.ToListAsync());
        }

        public async Task<IActionResult> AddRetourDate(int id)
        {
            var entry = _context.Ausleihe.Where(a => a.Zaehler == id).FirstOrDefault();
            entry.Retourdatum = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult GetBook(string id)
        {
            var book = _context.Buch.Where(a => a.Buchnummer == id).FirstOrDefault();
            return Json(book);
        }

        public JsonResult GetStudent(int id)
        {
            var student = _context.SchuelerIn.Where(a => a.Ausweisnummer == id).FirstOrDefault();
            return Json(student);
        }

        // GET: Ausleihe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausleihe = await _context.Ausleihe
                .Include(a => a.Buch)
                .Include(a => a.SchülerIn)
                .FirstOrDefaultAsync(m => m.Zaehler == id);
            if (ausleihe == null)
            {
                return NotFound();
            }

            return View(ausleihe);
        }

        // GET: Ausleihe/Create
        public IActionResult Create(int? ausweisnummer, string nachname, string vorname, string datetime)
        {
            if (ausweisnummer != null && !String.IsNullOrEmpty(nachname) && !String.IsNullOrEmpty(vorname) && !String.IsNullOrEmpty(datetime))
            {
                ViewData["ausweisnummer"] = ausweisnummer.ToString();
                ViewData["nachname"] = nachname;
                ViewData["vorname"] = vorname;
                ViewData["datetime"] = datetime;
            } else
            {
                ViewData["ausweisnummer"] = "0";
                ViewData["nachname"] = "";
                ViewData["vorname"] = "";
                ViewData["datetime"] = "";
            }

            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer");
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname");
            return View();
        }

        // POST: Ausleihe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Zaehler,Buchnummer,Ausweisnummer,Ausleihdatum,Retourdatum")] Ausleihe ausleihe)
        {

            System.Threading.Thread.Sleep(1000);

            if (ModelState.IsValid)
            {
                _context.Add(ausleihe);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer", ausleihe.Buchnummer);
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname", ausleihe.Ausweisnummer);
            return View(ausleihe);
        }

        public void CreateForJavascript([Bind("Zaehler,Buchnummer,Ausweisnummer,Ausleihdatum,Retourdatum")] Ausleihe ausleihe)
        {

            System.Threading.Thread.Sleep(1000);

            if (ModelState.IsValid)
            {
                _context.Add(ausleihe);
                _context.SaveChangesAsync();
            }
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer", ausleihe.Buchnummer);
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname", ausleihe.Ausweisnummer);
        }

        // GET: Ausleihe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausleihe = await _context.Ausleihe.FindAsync(id);
            if (ausleihe == null)
            {
                return NotFound();
            }
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer", ausleihe.Buchnummer);
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname", ausleihe.Ausweisnummer);
            return View(ausleihe);
        }

        // POST: Ausleihe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Zaehler,Buchnummer,Ausweisnummer,Ausleihdatum,Retourdatum")] Ausleihe ausleihe)
        {
            if (id != ausleihe.Zaehler)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ausleihe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AusleiheExists(ausleihe.Zaehler))
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
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer", ausleihe.Buchnummer);
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname", ausleihe.Ausweisnummer);
            return View(ausleihe);
        }

        // GET: Ausleihe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausleihe = await _context.Ausleihe
                .Include(a => a.Buch)
                .Include(a => a.SchülerIn)
                .FirstOrDefaultAsync(m => m.Zaehler == id);
            if (ausleihe == null)
            {
                return NotFound();
            }

            return View(ausleihe);
        }

        // POST: Ausleihe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ausleihe = await _context.Ausleihe.FindAsync(id);
            _context.Ausleihe.Remove(ausleihe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AusleiheExists(int id)
        {
            return _context.Ausleihe.Any(e => e.Zaehler == id);
        }
    }
}
