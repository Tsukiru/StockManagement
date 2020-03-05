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
    public class EtageresController : Controller
    {
        private readonly IEtagereRepository _repo;
        private readonly ISecteurRepository _repoSecteur;

        public EtageresController(IEtagereRepository repo, ISecteurRepository repoSecteur)
        {
            _repo = repo;
            _repoSecteur = repoSecteur;
        }

        // GET: Etageres
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll());
        }

        // GET: Etageres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etagere = await _repo.FindById((int)id);
            if (etagere == null)
            {
                return NotFound();
            }

            return View(etagere);
        }

        // GET: Etageres/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SecteurId"] = new SelectList(await _repoSecteur.GetAll(), "Id", "Name");
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
                _repo.Insert(etagere);
                await _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SecteurId"] = new SelectList(await _repoSecteur.GetAll(), "Id", "Name", etagere.SecteurId);
            return View(etagere);
        }

        // GET: Etageres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etagere = await _repo.FindById((int)id);
            if (etagere == null)
            {
                return NotFound();
            }
            ViewData["SecteurId"] = new SelectList(await _repoSecteur.GetAll(), "Id", "Name", etagere.SecteurId);
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
                    _repo.Update(etagere);
                    await _repo.Save();
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
            ViewData["SecteurId"] = new SelectList(await _repo.GetAll(), "Id", "Name", etagere.SecteurId);
            return View(etagere);
        }

        // GET: Etageres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etagere = await _repo.FindById((int)id);
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
            var etagere = await _repo.FindById(id);
            _repo.Remove(etagere);
            await _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool EtagereExists(int id)
        {
            return _repo.Exists(id);
        }
    }
}
