using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public interface IEtagereRepository
    {
        void Insert(Etagere etagere);
        void Update(Etagere etagere);
        void Remove(Etagere etagere);
        Task Save();
        Task<Etagere> FindById(int EtagereId);
        Task<IEnumerable<Etagere>> GetAll();
        public bool Exists(int id);
    }
}
