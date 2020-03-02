using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Produit
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("designation")]
        public string Designation { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("poids")]
        public float Poids { get; set; }
        [BsonElement("fournisseurs")]
        public Fournisseur Fournisseur { get; set; }
        [BsonElement("date_peremption")]
        public DateTime PeremptionDate { get; set; }
        [BsonElement("qt_stock")]
        public Stock stock { get; set; }
        [BsonElement("id_gamme_produit")]
        public Gamme Gamme { get; set; }
        [BsonElement("id_categorie")]
        public List<Categorie> Categorie { get; set; }
        [BsonElement("id_commentaire")]
        public List<Commentaire> Commentaire {get; set;}
    }
}
