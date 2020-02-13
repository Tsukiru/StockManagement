using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public class EtagereEFRepository : IEtagereRepository
    {
        private readonly MagasinDbContext context;

        public EtagereEFRepository(MagasinDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Etagere> FindById(int id)
        {
            Etagere etagere = await context.Etageres.Include(e => e.Secteur).FirstOrDefaultAsync(m => m.Id == id);
            return etagere;
        }

        public async Task<IEnumerable<Etagere>> GetAll()
        {
            List<Etagere> etageres = await context.Etageres.Include(e => e.Secteur).ToListAsync();
            return etageres;
        }

        public bool Exists(int id)
        {
            return context.Etageres.Any(e => e.Id == id);
        }

        public void Insert(Etagere etagere)
        {
            context.Etageres.Add(etagere);
        }

        public void Remove(Etagere etagere)
        {
            context.Etageres.Remove(etagere);
        }

        public void Update(Etagere etagere)
        {
            context.Etageres.Update(etagere);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
