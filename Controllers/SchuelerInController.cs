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
    public class SchuelerInController : Controller
    {
        private readonly BuechereiDBContext _context;

        public SchuelerInController(BuechereiDBContext context)
        {
            _context = context;
        }

        // GET: SchuelerIn
        public async Task<IActionResult> Index()
        {
            return View(await _context.SchuelerIn.ToListAsync());
        }

        // GET: SchuelerIn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schuelerIn = await _context.SchuelerIn
                .FirstOrDefaultAsync(m => m.Ausweisnummer == id);
            if (schuelerIn == null)
            {
                return NotFound();
            }

            return View(schuelerIn);
        }

        // GET: SchuelerIn/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchuelerIn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ausweisnummer,Nachname,Vorname")] SchuelerIn schuelerIn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schuelerIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schuelerIn);
        }

        public void CreateNewSchuelerIn(int ausweisnummer, string nachname, string vorname)
        {
            SchuelerIn schuelerIn = new SchuelerIn()
            {
                Ausweisnummer = ausweisnummer,
                Nachname = nachname,
                Vorname = vorname
            };

            if (ModelState.IsValid)
            {
                _context.Add(schuelerIn);
                _context.SaveChangesAsync();
            }
        }

        // GET: SchuelerIn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schuelerIn = await _context.SchuelerIn.FindAsync(id);
            if (schuelerIn == null)
            {
                return NotFound();
            }
            return View(schuelerIn);
        }

        // POST: SchuelerIn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ausweisnummer,Nachname,Vorname")] SchuelerIn schuelerIn)
        {
            if (id != schuelerIn.Ausweisnummer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schuelerIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchuelerInExists(schuelerIn.Ausweisnummer))
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
            return View(schuelerIn);
        }

        // GET: SchuelerIn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schuelerIn = await _context.SchuelerIn
                .FirstOrDefaultAsync(m => m.Ausweisnummer == id);
            if (schuelerIn == null)
            {
                return NotFound();
            }

            return View(schuelerIn);
        }

        // POST: SchuelerIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schuelerIn = await _context.SchuelerIn.FindAsync(id);
            _context.SchuelerIn.Remove(schuelerIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchuelerInExists(int id)
        {
            return _context.SchuelerIn.Any(e => e.Ausweisnummer == id);
        }
    }
}
