using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using System;
using Xunit;

namespace StockManagementTest
{
    public class TestArticleEFRepository
    {
        public TestArticleEFRepository()
        {
            using (var myContext = new MagasinDbContext(new DbContextOptionsBuilder<MagasinDbContext>().UseSqlite("Filename=store.db").Options))
            {
                myContext.Database.EnsureDeleted();
                myContext.Database.EnsureCreated();

                myContext.AddRange(
                    new[]{
                        new Article { Id = 1, Libelle = "Tablette", SKU = "123456", DateSortie = DateTime.Now, PrixInitial = 499, Poids = 499 },
                        new Article { Id = 2, Libelle = "Telephone", SKU = "789101", DateSortie = DateTime.Now, PrixInitial = 299, Poids = 258 },
                        new Article { Id = 3, Libelle = "PC", SKU = "147852", DateSortie = DateTime.Now, PrixInitial = 1566, Poids = 1890 },
                        new Article { Id = 4, Libelle = "Bureau", SKU = "258963", DateSortie = DateTime.Now, PrixInitial = 350, Poids = 9500 },
                    }
                );

                myContext.SaveChanges();
            }
        }
    }
}
