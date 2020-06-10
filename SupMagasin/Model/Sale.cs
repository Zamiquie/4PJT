using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("SaleDate")]
        public DateTime SaleDate { get; set; }
        [BsonElement("IdPhone")]
        public string IdPhone { get; set; }
        [BsonElement("IdCustomer")]
        public string IdCustomer { get; set; }
        [BsonElement("IdShop")] 
        public string IdShop { get; set; }
        [BsonElement("TotalAmount")]
        public float TotalAmount { get; set; }
        [BsonElement("isPayed")]
        public bool isPayed { get; set; }
        [BsonElement("ProduitsSales")]
        public List<SaleProduct> ProduitsSales { get; set; }
    }
}
