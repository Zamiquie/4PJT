using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Produit
    {
        public Guid ID { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public float Poids { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public DateTime PeremptionDate { get; set; }
        public Stock stock { get; set; }
        public Gamme Gamme { get; set; }
        public Categorie Categorie { get; set; }
        public Commentaire Commentaire {get; set;}



    }
}
