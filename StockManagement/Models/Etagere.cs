using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class Etagere
    {
        public int Id { get; set; }
        public decimal PoidsMaximum { get; set; }
        public int SecteurId { get; set; }
        public Secteur Secteur { get; set; }
        public ICollection<PositionsMagasin> PositionMagasin { get; set; }
    }
}
