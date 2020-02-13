using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagementTest
{
    public class InitDbcontext
    {
        public static MagasinDbContext FillDbContext(MagasinDbContext myContext)
        {
            myContext.Database.EnsureDeleted();
            myContext.Database.EnsureCreated();

            myContext.AddRange(
                new[] {
                        new Secteur { Id = 1, Name = "Secteur A" },
                        new Secteur { Id = 2, Name = "Secteur B" }
                });

            myContext.AddRange(
                new[] {
                        new Etagere { Id = 1, PoidsMaximum = 15000, SecteurId = 1 },
                        new Etagere { Id = 2, PoidsMaximum = 17000, SecteurId = 1 },
                        new Etagere { Id = 3, PoidsMaximum = 15500, SecteurId = 2 },
                        new Etagere { Id = 4, PoidsMaximum = 12000, SecteurId = 2 },
                });

            myContext.AddRange(
                new[] {
                        new Article { Id = 1, Libelle = "Tablette", SKU = "123456", DateSortie = DateTime.Now, PrixInitial = 499, Poids = 499 },
                        new Article { Id = 2, Libelle = "Telephone", SKU = "789101", DateSortie = DateTime.Now, PrixInitial = 299, Poids = 258 },
                        new Article { Id = 3, Libelle = "PC", SKU = "147852", DateSortie = DateTime.Now, PrixInitial = 1566, Poids = 1890 },
                        new Article { Id = 4, Libelle = "Bureau", SKU = "258963", DateSortie = DateTime.Now, PrixInitial = 350, Poids = 9500 },
                });

            myContext.AddRange(
               new[] {
                        new PositionsMagasin { ArticleId = 1, EtagereId = 1, Quantite = 10 },
                        new PositionsMagasin { ArticleId = 2, EtagereId = 1, Quantite = 2 },
                        new PositionsMagasin { ArticleId = 1, EtagereId = 3, Quantite = 15 }
                });
            myContext.SaveChanges();
            return myContext;
        }

        public static MagasinDbContext CreateMagasinDbContext()
        {
            return (new MagasinDbContext(new DbContextOptionsBuilder<MagasinDbContext>().UseSqlite("Filename=store.db").Options));
        }
    }
}
