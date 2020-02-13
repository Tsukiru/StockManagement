using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public interface ISecteurRepositiry
    {

        void Insert(Secteur secteur);
        void Update(Secteur secteur);
        void Remove(Secteur secteur);
        Task Save();
        Task<Secteur> FindById(int secteurId);
        Task<IEnumerable<Secteur>> GetAll();

        public bool Exists(int id);

    }
}
