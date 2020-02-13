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
        public bool Exists(int id);
        Task<IEnumerable<Article>> GetAll();
        IEnumerable<Article> GetAllByEtagere(int etagereId);
        IEnumerable<Article> GetAllBySecteur(int secteurId);
        Task<Article> FindById(int articleId);
        decimal GetAveragePriceBySecteur(int secteurId);
    }
}
