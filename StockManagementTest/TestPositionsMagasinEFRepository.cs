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
    public class TestPositionsMagasinEFRepository : InitDbcontext
    {
        public TestPositionsMagasinEFRepository()
        {
            var myContext = CreateMagasinDbContext();
            FillDbContext(myContext);
        }

        [Fact]
        public void NullContextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PositionMagasinEFRepository(null));
        }

        [Fact]
        public async Task GetAllTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new PositionMagasinEFRepository(myContext);
            var allPositionsMagasin = await repo.GetAll();
            allPositionsMagasin.Should().HaveCount(3);
        }

        [Fact]
        public void ExistsTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new PositionMagasinEFRepository(myContext);
            var exists = repo.Exists(1, 1);
            exists.Should().BeTrue();
        }


        [Fact]
        public async Task FindByIdTest()
        {
            using var myContext = CreateMagasinDbContext();
            var repo = new PositionMagasinEFRepository(myContext);
            var positionMagasin = await repo.FindById(3, 1);
            positionMagasin.Quantite.Should().Be(15);
        }

        [Fact]
        public async Task RemoveTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new PositionMagasinEFRepository(myContext);
                var positionMagasin = await repo.FindById(1, 1);
                repo.Remove(positionMagasin);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new PositionMagasinEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(2);
                elements.Any(e => e.Quantite == 10).Should().BeFalse();
            }
        }

        [Fact]
        public async Task InsertTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new PositionMagasinEFRepository(myContext);
                var positionMagasin = new PositionsMagasin
                {
                    ArticleId = 4,
                    EtagereId = 3,
                    Quantite = 7
                };

                repo.Insert(positionMagasin);
                await (repo.Save());
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new PositionMagasinEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(4);
                elements.Any(e => e.Quantite == 7).Should().BeTrue();
            }
        }

        [Fact]
        public async Task UpdateTest()
        {
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new PositionMagasinEFRepository(myContext);
                var positionMagasin = new PositionsMagasin
                {
                    ArticleId = 1,
                    EtagereId = 1,
                    Quantite = 7
                };

                repo.Update(positionMagasin);
                await repo.Save();
            }
            using (var myContext = CreateMagasinDbContext())
            {
                var repo = new PositionMagasinEFRepository(myContext);
                var elements = await repo.GetAll();
                elements.Should().HaveCount(3);
                elements.Any(e => e.Quantite == 7).Should().BeTrue();
                elements.Any(e => e.Quantite  == 10).Should().BeFalse();
            }
        }
    }
}
