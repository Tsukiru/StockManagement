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
    public class SecteursController : Controller
    {
        private readonly MagasinDbContext _context;

        public SecteursController(MagasinDbContext context)
        {
            _context = context;
        }

        // GET: Secteurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Secteurs.ToListAsync());
        }

        // GET: Secteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secteur = await _context.Secteurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secteur == null)
            {
                return NotFound();
            }

            return View(secteur);
        }

        // GET: Secteurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Secteurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Secteur secteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(secteur);
        }

        // GET: Secteurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secteur = await _context.Secteurs.FindAsync(id);
            if (secteur == null)
            {
                return NotFound();
            }
            return View(secteur);
        }

        // POST: Secteurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Secteur secteur)
        {
            if (id != secteur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecteurExists(secteur.Id))
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
            return View(secteur);
        }

        // GET: Secteurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secteur = await _context.Secteurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secteur == null)
            {
                return NotFound();
            }

            return View(secteur);
        }

        // POST: Secteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secteur = await _context.Secteurs.FindAsync(id);
            _context.Secteurs.Remove(secteur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecteurExists(int id)
        {
            return _context.Secteurs.Any(e => e.Id == id);
        }
    }
}
