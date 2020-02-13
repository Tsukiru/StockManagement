using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement.Controllers
{
    public class EtageresController : Controller
    {
        private readonly MagasinDbContext _context;

        public EtageresController(MagasinDbContext context)
        {
            _context = context;
        }

        // GET: Etageres
        public async Task<IActionResult> Index()
        {
            var magasinDbContext = _context.Etageres.Include(e => e.Secteur);
            return View(await magasinDbContext.ToListAsync());
        }

        // GET: Etageres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etagere = await _context.Etageres
                .Include(e => e.Secteur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etagere == null)
            {
                return NotFound();
            }

            return View(etagere);
        }

        // GET: Etageres/Create
        public IActionResult Create()
        {
            ViewData["SecteurId"] = new SelectList(_context.Secteurs, "Id", "Name");
            return View();
        }

        // POST: Etageres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PoidsMaximum,SecteurId")] Etagere etagere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etagere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SecteurId"] = new SelectList(_context.Secteurs, "Id", "Name", etagere.SecteurId);
            return View(etagere);
        }

        // GET: Etageres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etagere = await _context.Etageres.FindAsync(id);
            if (etagere == null)
            {
                return NotFound();
            }
            ViewData["SecteurId"] = new SelectList(_context.Secteurs, "Id", "Name", etagere.SecteurId);
            return View(etagere);
        }

        // POST: Etageres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PoidsMaximum,SecteurId")] Etagere etagere)
        {
            if (id != etagere.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etagere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtagereExists(etagere.Id))
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
            ViewData["SecteurId"] = new SelectList(_context.Secteurs, "Id", "Name", etagere.SecteurId);
            return View(etagere);
        }

        // GET: Etageres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etagere = await _context.Etageres
                .Include(e => e.Secteur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etagere == null)
            {
                return NotFound();
            }

            return View(etagere);
        }

        // POST: Etageres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etagere = await _context.Etageres.FindAsync(id);
            _context.Etageres.Remove(etagere);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtagereExists(int id)
        {
            return _context.Etageres.Any(e => e.Id == id);
        }
    }
}
