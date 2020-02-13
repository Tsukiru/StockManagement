using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            var ctx = (MagasinDbContext)builder.Services.CreateScope().ServiceProvider.GetRequiredService(typeof(MagasinDbContext));

            ctx.Database.EnsureCreated();

            if (!ctx.Articles.Any())
            {
                ctx.AddRange(
                    new[] {
                        new Secteur
                        {
                            Id = 1,
                            Name = "Secteur A"
                        },
                        new Secteur
                        {
                            Id = 2,
                            Name = "Secteur B"
                        }
                });

                ctx.AddRange(
                   new[] {
                        new Etagere
                        {
                            Id = 1,
                            PoidsMaximum = 15000,
                            SecteurId = 1
                        },
                        new Etagere
                        {
                            Id = 2,
                            PoidsMaximum = 17000,
                            SecteurId = 1
                        },
                        new Etagere
                        {
                            Id = 3,
                            PoidsMaximum = 15500,
                            SecteurId = 2
                        },
                        new Etagere
                        {
                            Id = 4,
                            PoidsMaximum = 12000,
                            SecteurId = 2
                        },
               });
                ctx.AddRange(
                     new[] {
                        new Article
                        {
                            Id = 1,
                            Libelle = "Tablette",
                            SKU = "123456",
                            DateSortie = DateTime.Now,
                            PrixInitial = 499,
                            Poids = 499
                        },
                        new Article
                        {
                            Id = 2,
                            Libelle = "Telephone",
                            SKU = "789101",
                            DateSortie = DateTime.Now,
                            PrixInitial = 299,
                            Poids = 258
                        },
                        new Article
                        {
                            Id = 3,
                            Libelle = "PC",
                            SKU = "147852",
                            DateSortie = DateTime.Now,
                            PrixInitial = 1566,
                            Poids = 1890
                        },
                        new Article
                        {
                            Id = 4,
                            Libelle = "Bureau",
                            SKU = "258963",
                            DateSortie = DateTime.Now,
                            PrixInitial = 350,
                            Poids = 9500
                        },
                 });
                ctx.AddRange(
                   new[] {
                        new PositionsMagasin
                        {
                            ArticleId = 1,
                            EtagereId = 1,
                            Quantite = 10
                        },
                        new PositionsMagasin
                        {
                            ArticleId = 2,
                            EtagereId = 1,
                            Quantite = 2
                        },
                        new PositionsMagasin
                        {
                            ArticleId = 1,
                            EtagereId = 3,
                            Quantite = 15
                        }
               });
                ctx.SaveChanges();
            }
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
