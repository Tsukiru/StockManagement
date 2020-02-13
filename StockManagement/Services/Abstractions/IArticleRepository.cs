using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public interface IArticleRepository
    {
        void Insert(Article article);
        void Update(Article article);
        void Remove(Article article);
        Task Save();
        Task<Article> FindById(int articleId);
        Task<IEnumerable<Article>> GetAll();

        public bool Exists(int id);
        IEnumerable<Article> GetAllByEtagere(int etagereId);
        IEnumerable<Article> GetAllBySecteur(int secteurId);
        decimal GetAveragePriceBySecteur(int secteurId);
    }
}
