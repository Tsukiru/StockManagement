using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public interface IPositionMagasinRepository
    {
        void Insert(PositionsMagasin positionsMagasin);
        void Update(PositionsMagasin positionsMagasin);
        void Remove(PositionsMagasin positionsMagasin);
        Task Save();
        Task<PositionsMagasin> FindById(int positionsMagasinId);
        Task<IEnumerable<PositionsMagasin>> GetAll();

        public bool Exists(int ArticleId, int EtagereId);
    }
}
