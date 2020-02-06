using StockManagement.Data;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public class ArticleEFRepository : IArticleRepository
    {
        private readonly MagasinDbContext context;

        public ArticleEFRepository(MagasinDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Article FindById(int id)
        {
            Article article = context.Articles.Where(m => m.Id == id).First();
            return article;
        }

        public IEnumerable<Article> GetAll()
        {
            List<Article> articles = context.Articles.ToList();
            return articles;
        }

        public IEnumerable<Article> GetAllByEtagere(int EtagereId)
        {
            List<Article> articles = context.Articles.Where(m => m.PositionMagasin.Any(p => p.EtagereId == EtagereId)).ToList();
            return articles;
        }

        public IEnumerable<Article> GetAllBySecteur(int SecteurId)
        {
            List<Article> articles = context.Articles.Where(m => m.PositionMagasin.Any(p => p.Etagere.SecteurId == SecteurId)).ToList();
            return articles;
        }

        public decimal GetAveragePriceBySecteur(int SecteurId)
        {
            decimal AveragePrice = 0;

            List<Article> articlesBySecteur = (List<Article>)(GetAllBySecteur(SecteurId));

            foreach (Article article in articlesBySecteur)
            {
                AveragePrice += article.PrixInitial;
            }
            AveragePrice /= articlesBySecteur.Count;
            return AveragePrice;
        }

        public void Insert(Article article)
        {
            context.Articles.Add(article);
        }

        public void Remove(Article article)
        {
            context.Articles.Remove(article);
        }

        public void Update(Article article)
        {
            context.Articles.Update(article);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
