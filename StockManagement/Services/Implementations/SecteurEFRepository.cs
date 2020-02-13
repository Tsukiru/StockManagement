using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public class SecteurEFRepository : ISecteurRepository
    {

        private readonly MagasinDbContext context;

        public SecteurEFRepository(MagasinDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Secteur> FindById(int id)
        {
            Secteur secteur = await context.Secteurs.FindAsync(id);
            return secteur;
        }

        public async Task<IEnumerable<Secteur>> GetAll()
        {
            List<Secteur> secteurs = await context.Secteurs.ToListAsync();
            return secteurs;
        }

        public bool Exists(int id)
        {
            return context.Secteurs.Any(e => e.Id == id);
        }

        public void Insert(Secteur secteur)
        {
            context.Secteurs.Add(secteur);
        }

        public void Remove(Secteur secteur)
        {
            context.Secteurs.Remove(secteur);
        }

        public void Update(Secteur secteur)
        {
            context.Secteurs.Update(secteur);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

    }
}
