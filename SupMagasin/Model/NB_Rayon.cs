using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class NB_Rayon
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
