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
    public class TestEtagereEFRepository : InitDbcontext
    {
        public TestEtagereEFRepository()
        {
            var myContext = CreateMagasinDbContext();
            FillDbContext(myContext);
        }

        [Fact]
        public void NullContextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EtagereEFRepository(null));
        }

        [Fact]
        public async Task GetAllTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new EtagereEFRepository(myContext);
            var allEtageres = await repo.GetAll();
            allEtageres.Should().HaveCount(4);
        }

        [Fact]
        public void ExistsTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new EtagereEFRepository(myContext);
            var exists = repo.Exists(1);
            exists.Should().BeTrue();
        }


        [Fact]
        public async Task FindByIdTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new EtagereEFRepository(myContext);
            var etagere = await repo.FindById(2);
            etagere.PoidsMaximum.Should().Be(17000);
            etagere.Id.Should().Be(2);
        }

        [Fact]
        public async Task RemoveTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new EtagereEFRepository(myContext);
                var etagere = await repo.FindById(1);
                repo.Remove(etagere);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new EtagereEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(3);
                elements.Any(e => e.Id == 1).Should().BeFalse();
            }
        }

        [Fact]
        public async Task InsertTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new EtagereEFRepository(myContext);
                var newEtagere = new Etagere
                {
                    Id = 5,
                    PoidsMaximum = 22000,
                    SecteurId = 2
                };

                repo.Insert(newEtagere);
                await (repo.Save());
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new EtagereEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(5);
                elements.Any(e => e.Id == 5).Should().BeTrue();
                elements.Any(e => e.PoidsMaximum == 22000).Should().BeTrue();
            }
        }

        [Fact]
        public async Task UpdateTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new EtagereEFRepository(myContext);
                var etagere = new Etagere
                {
                    Id = 1,
                    PoidsMaximum = 26580,
                    SecteurId = 2
                };

                repo.Update(etagere);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new EtagereEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(4);
                elements.Any(e => e.Id == 1).Should().BeTrue();
                elements.Any(e => e.PoidsMaximum == 26580).Should().BeTrue();
                elements.Any(e => e.PoidsMaximum == 15000).Should().BeFalse();
                elements.Any(e => e.SecteurId == 2).Should().BeTrue();
                elements.Any(e => e.PoidsMaximum == 2).Should().BeFalse();
            }
        }
    }
}
