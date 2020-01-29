using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Vente
    {
        public int ID { get; set; }
        public DateTime VenteDate { get; set; }
        public TelephoneUser TelephoneUser { get; set; }
        public float MontantTTC { get; set; }
        public List<ProduitVente> ProduitVente { get; set; }
        public bool isPayed { get; set; }
    }
}
