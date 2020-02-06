using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    interface IArticleRepository
    {
        void Insert(Article article);
        void Update(Article article);
        void Remove(Article article);
        void Save();
        IEnumerable<Article> GetAll();
        IEnumerable<Article> GetAllByEtagere(int etagereId);
        IEnumerable<Article> GetAllBySecteur(int secteurId);
        Article FindById(int articleId);
        decimal GetAveragePriceBySecteur(int secteurId);
    }
}
