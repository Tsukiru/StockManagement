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
    public class PositionsMagasinsController : Controller
    {
        private readonly MagasinDbContext _context;

        public PositionsMagasinsController(MagasinDbContext context)
        {
            _context = context;
        }

        // GET: PositionsMagasins
        public async Task<IActionResult> Index()
        {
            var magasinDbContext = _context.PositionsMagasin.Include(p => p.Article).Include(p => p.Etagere);
            return View(await magasinDbContext.ToListAsync());
        }

        // GET: PositionsMagasins/Details/5
        public async Task<IActionResult> Details(int? EtagereId, int? ArticleId)
        {
            if (EtagereId == null || ArticleId == null)
            {
                return NotFound();
            }

            var positionsMagasin = await _context.PositionsMagasin
                .Include(p => p.Article)
                .Include(p => p.Etagere)
                .FirstOrDefaultAsync(m => m.EtagereId == EtagereId && m.ArticleId == ArticleId);
            if (positionsMagasin == null)
            {
                return NotFound();
            }

            return View(positionsMagasin);
        }

        // GET: PositionsMagasins/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Libelle");
            ViewData["EtagereId"] = new SelectList(_context.Etageres, "Id", "Id");
            return View();
        }

        // POST: PositionsMagasins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,EtagereId,Quantite")] PositionsMagasin positionsMagasin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(positionsMagasin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Libelle", positionsMagasin.ArticleId);
            ViewData["EtagereId"] = new SelectList(_context.Etageres, "Id", "Id", positionsMagasin.EtagereId);
            return View(positionsMagasin);
        }

        // GET: PositionsMagasins/Edit/5
        public async Task<IActionResult> Edit(int? EtagereId, int? ArticleId)
        {
            if (EtagereId == null || ArticleId == null)
            {
                return NotFound();
            }

            var positionsMagasin = await _context.PositionsMagasin
                .FirstOrDefaultAsync(m => m.EtagereId == EtagereId && m.ArticleId == ArticleId); ;
            if (positionsMagasin == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Libelle", positionsMagasin.ArticleId);
            ViewData["EtagereId"] = new SelectList(_context.Etageres, "Id", "Id", positionsMagasin.EtagereId);
            return View(positionsMagasin);
        }

        // POST: PositionsMagasins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int EtagereId, int ArticleId, [Bind("ArticleId,EtagereId,Quantite")] PositionsMagasin positionsMagasin)
        {
            if (EtagereId != positionsMagasin.EtagereId || ArticleId != positionsMagasin.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(positionsMagasin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionsMagasinExists(positionsMagasin.EtagereId, positionsMagasin.ArticleId))
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
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Libelle", positionsMagasin.ArticleId);
            ViewData["EtagereId"] = new SelectList(_context.Etageres, "Id", "Id", positionsMagasin.EtagereId);
            return View(positionsMagasin);
        }

        // GET: PositionsMagasins/Delete/5
        public async Task<IActionResult> Delete(int? EtagereId, int? ArticleId)
        {
            if (EtagereId == null || ArticleId == null)
            {
                return NotFound();
            }
            var positionsMagasin = await _context.PositionsMagasin
                .Include(p => p.Article)
                .Include(p => p.Etagere)
                .FirstOrDefaultAsync(m => m.EtagereId == EtagereId && m.ArticleId == ArticleId);
            if (positionsMagasin == null)
            {
                return NotFound();
            }
            return View(positionsMagasin);
        }

        // POST: PositionsMagasins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? EtagereId, int? ArticleId)
        {
            var positionsMagasin = await _context.PositionsMagasin.FirstOrDefaultAsync(m => m.EtagereId == EtagereId && m.ArticleId == ArticleId);
            _context.PositionsMagasin.Remove(positionsMagasin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionsMagasinExists(int? EtagereId, int? ArticleId)
        {
            return _context.PositionsMagasin.Any(m => m.EtagereId == EtagereId && m.ArticleId == ArticleId);
        }
    }
}
