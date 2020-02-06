using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class Secteur
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Etagere> Etageres { get; set; }
    }
}
