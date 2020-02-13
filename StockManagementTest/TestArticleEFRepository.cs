using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StockManagementTest
{
    public class TestArticleEFRepository : InitDbcontext
    {
        public TestArticleEFRepository()
        {
            var myContext = CreateMagasinDbContext();
            FillDbContext(myContext);
        }

        [Fact]
        public void NullContextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ArticleEFRepository(null));
        }

        [Fact]
        public async Task GetAllTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new ArticleEFRepository(myContext);
            var allArticles = await repo.GetAll();
            allArticles.Should().HaveCount(4);
        }

        [Fact]
        public async Task FindByIdTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new ArticleEFRepository(myContext);
            var article = await repo.FindById(3);
            article.Libelle.Should().Be("PC");
            article.Id.Should().Be(3);
        }

        [Fact]
        public async Task RemoveTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new ArticleEFRepository(myContext);
                var article = await repo.FindById(4);
                repo.Remove(article);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new ArticleEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(3);
                elements.Any(e => e.Id == 4).Should().BeFalse();
            }
        }

        [Fact]
        public async Task InsertTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new ArticleEFRepository(myContext);
                var newArticle = new Article
                {
                    Id = 5,
                    Libelle = "Clavier",
                    SKU = "854765",
                    DateSortie = DateTime.Now,
                    PrixInitial = 249,
                    Poids = 250,
                    PositionMagasin = new List<PositionsMagasin>
                    {
                        new PositionsMagasin { ArticleId = 5, EtagereId = 2, Quantite = 10 },
                        new PositionsMagasin { ArticleId = 5, EtagereId = 1, Quantite = 42 }
                    }
                };

                repo.Insert(newArticle);
                await(repo.Save());
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new ArticleEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(5);
                elements.Any(e => e.Id == 5).Should().BeTrue();
                elements.Any(e => e.Libelle == "Clavier").Should().BeTrue();
            }
        }

        [Fact]
        public async Task UpdateTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new ArticleEFRepository(myContext);
                var article = new Article
                {
                    Id = 1,
                    Libelle = "Chargeur",
                    SKU = "785625",
                    DateSortie = DateTime.Now,
                    PrixInitial = 129,
                    Poids = 76
                };

                repo.Update(article);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new ArticleEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(4);
                elements.Any(e => e.Id == 1).Should().BeTrue();
                elements.Any(e => e.Libelle == "Chargeur").Should().BeTrue();
                elements.Any(e => e.Libelle == "Tablette").Should().BeFalse();
            }
        }

        [Fact]
        public void GetAllByEtagereTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new ArticleEFRepository(myContext);
            var allArticles = repo.GetAllByEtagere(1);
            allArticles.Should().HaveCount(2);
        }

        [Fact]
        public void GetAllBySecteurTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new ArticleEFRepository(myContext);
            var allArticles = repo.GetAllBySecteur(1);
            allArticles.Should().HaveCount(2);
        }

        [Fact]
        public void GetAveragePriceBySecteurTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new ArticleEFRepository(myContext);
            var average = repo.GetAveragePriceBySecteur(1);
            average.Should().Be(399);
        }
    }
}
