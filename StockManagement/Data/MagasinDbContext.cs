using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace TPBibliotheque
{
    public class MagasinDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // création de la classe de configuration pour l'entité "Student"
            var articleEntity = modelBuilder.Entity<Article>();
            // définition du mapping
            articleEntity.HasKey(a => a.Id); // définition du champ clé

            // création de la classe de configuration pour l'entité "Teacher"
            var etagereEntity = modelBuilder.Entity<Etagere>();
            // définition du mapping
            etagereEntity.HasKey(e => e.Id);
           
            //création des relations

            etagereEntity
                .HasOne(s => s.Secteur)
                .WithMany(e => e.Etageres)
                .HasForeignKey(s => s.SecteurId);

            // création de la classe de configuration pour l'entité "ApprenticeLink"
            var positionMagasin = modelBuilder.Entity<PositionMagasin>();
            //définition du mapping n n
            positionMagasin.HasKey(pm => new { pm.EtagereId, pm.ArticleId }); // définition d'une clé composée
            positionMagasin
                .HasOne(pm => pm.Etagere)
                .WithMany(e => e.PositionMagasin)
                .HasForeignKey(pm => pm.EtagereId);
            positionMagasin
                .HasOne(pm => pm.Article)
                .WithMany(a => a.PositionMagasin)
                .HasForeignKey(pm => pm.ArticleId);

            // Secteur 
            var secteurEntity = modelBuilder.Entity<Secteur>();
                secteurEntity.HasKey(s => s.Id);
                secteurEntity.Property(s => s.Name).HasMaxLength(256).IsRequired();


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // définition de la base de données à utiliser ainsi que de la chaine de connexion

            optionsBuilder.UseSqlite("Filename=test.db");
            /*SqlLite ou SqlServer (direct sur vs) */
            base.OnConfiguring(optionsBuilder);
        }

    }
}


