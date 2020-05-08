using MongoDB.Bson.Serialization.Attributes;
using SupMagasin.Model.ProductModel;
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
        [BsonElement("weight")]
        public float Weight { get; set; }
        [BsonElement("BuyPrice")]
        public float BuyPrice { get; set; }
        [BsonElement("SalePrice")]
        public float SalePrice { get; set; }
        [BsonElement("id_gamme_produit")]
        public Gamme Gamme { get; set; }
        [BsonElement("id_categorie")]
        public Categorie Categorie { get; set; }
        [BsonElement("id_commentaire")]
        public List<Commentaire> Commentaire {get; set;}
        [BsonElement("fournisseur")]
        public Fournisseur Fournisseur { get; set; }
        [BsonElement("Lots")]
        public List<LotsModel> Lots { get; set; }
        [BsonElement("isSaling")]
        public bool IsSaling { get; set; }
        [BsonElement("QrCode")]
        public string QrCode { get; set; }
    }
}
