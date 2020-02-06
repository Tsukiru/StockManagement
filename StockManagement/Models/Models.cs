using System;
using System.Collections.Generic;
using System.Text;

namespace TPBibliotheque
{
    public class Article
    {
        public int Id { get; set; }
        public string libelle { get; set; }
        public string SKU { get; set; }
        public DateTime DateSortie { get; set; }
        public decimal PrixInitial { get; set; }
        public decimal Poids { get; set; }
        public ICollection<PositionMagasin> PositionMagasin { get; set; }
    }

    public class PositionMagasin
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int EtagereId { get; set; }
        public Etagere Etagere { get; set; }
        public int Quantite { get; set; }
    }

    public class Etagere
    {
        public int Id { get; set; }
        public decimal PoidMaximum { get; set; }
        public int SecteurId { get; set; }
        public Secteur Secteur { get; set; }
        public ICollection<PositionMagasin> PositionMagasin { get; set; }
    }

    public class Secteur
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Etagere> Etageres { get; set; }

    }
}
