using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public class PositionMagasinEFRepository : IPositionMagasinRepository
    {

        private readonly MagasinDbContext context;

        public PositionMagasinEFRepository(MagasinDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PositionsMagasin> FindById(int id)
        {
            PositionsMagasin positionMagasin = await context.PositionsMagasin.FindAsync(id);
            return positionMagasin;
        }

        public async Task<IEnumerable<PositionsMagasin>> GetAll()
        {
            List<PositionsMagasin> positionsMagasin = await context.PositionsMagasin.ToListAsync();
            return positionsMagasin;
        }

        public bool Exists(int id)
        {
            return context.PositionsMagasin.Any(e => e.ArticleId == id);
        }

        public void Insert(PositionsMagasin positionMagasin)
        {
            context.PositionsMagasin.Add(positionMagasin);
        }

        public void Remove(PositionsMagasin positionMagasin)
        {
            context.PositionsMagasin.Remove(positionMagasin);
        }

        public void Update(PositionsMagasin positionMagasin)
        {
            context.PositionsMagasin.Update(positionMagasin);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

    }
}
