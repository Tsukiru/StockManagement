using Microsoft.EntityFrameworkCore;
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

        public async Task<Article> FindById(int id)
        {
            Article article = await context.Articles.FindAsync(id);
            return article;
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            List<Article> articles = await context.Articles.ToListAsync();
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

        public bool Exists(int id)
        {
            return context.Articles.Any(e => e.Id == id);
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

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
