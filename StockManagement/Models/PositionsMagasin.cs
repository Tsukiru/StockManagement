using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class PositionsMagasin
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int EtagereId { get; set; }
        public Etagere Etagere { get; set; }
        public int Quantite { get; set; }
    }
}
