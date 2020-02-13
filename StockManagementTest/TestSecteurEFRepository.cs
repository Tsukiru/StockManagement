using FluentAssertions;
using StockManagement.Models;
using StockManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockManagementTest
{
    public class TestSecteurEFRepository : InitDbcontext
    {
        public TestSecteurEFRepository()
        {
            var myContext = CreateMagasinDbContext();
            FillDbContext(myContext);
        }

        [Fact]
        public void NullContextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SecteurEFRepository(null));
        }

        [Fact]
        public async Task GetAllTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new SecteurEFRepository(myContext);
            var allSecteurs = await repo.GetAll();
            allSecteurs.Should().HaveCount(4);
        }

        [Fact]
        public void ExistsTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new SecteurEFRepository(myContext);
            var exists = repo.Exists(1);
            exists.Should().BeTrue();
        }


        [Fact]
        public async Task FindByIdTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new SecteurEFRepository(myContext);
            var secteur = await repo.FindById(2);
            secteur.Name.Should().Be("Secteur B");
            secteur.Id.Should().Be(2);
        }

        [Fact]
        public async Task RemoveTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new SecteurEFRepository(myContext);
                var secteur = await repo.FindById(1);
                repo.Remove(secteur);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new SecteurEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(1);
                elements.Any(e => e.Id == 1).Should().BeFalse();
            }
        }

        [Fact]
        public async Task InsertTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new SecteurEFRepository(myContext);
                var newSecteur = new Secteur
                {
                    Id = 3,
                    Name = "Secteur C"
                };

                repo.Insert(newSecteur);
                await (repo.Save());
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new SecteurEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(3);
                elements.Any(e => e.Id == 3).Should().BeTrue();
                elements.Any(e => e.Name == "Secteur C").Should().BeTrue();
            }
        }

        [Fact]
        public async Task UpdateTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new SecteurEFRepository(myContext);
                var secteur = new Secteur
                {
                    Id = 1,
                    Name = "Secteur Z"
                };

                repo.Update(secteur);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new SecteurEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(2);
                elements.Any(e => e.Id == 1).Should().BeTrue();
                elements.Any(e => e.Name == "Secteur Z").Should().BeTrue();
                elements.Any(e => e.Name  == "Secteur A").Should().BeFalse();
                elements.Any(e => e.Id == 1).Should().BeTrue();
            }
        }
    }
}
