using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Services;

namespace StockManagement.Controllers
{
    public class SecteursController : Controller
    {
        private readonly ISecteurRepository _repo;

        public SecteursController(ISecteurRepository repo)
        {
            _repo = repo;
        }

        // GET: Secteurs
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll());
        }

        // GET: Secteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secteur = await _repo.FindById((int)id);
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
                _repo.Insert(secteur);
                await _repo.Save();
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

            var secteur = await _repo.FindById((int)id);
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
                    _repo.Update(secteur);
                    await _repo.Save();
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

            var secteur = await _repo.FindById((int)id);
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
            var secteur = await _repo.FindById(id);
            _repo.Remove(secteur);
            await _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool SecteurExists(int id)
        {
            return _repo.Exists(id);
        }
    }
}
