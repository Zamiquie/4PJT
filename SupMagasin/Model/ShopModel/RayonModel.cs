using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model.ShopModel
{
    public class RayonModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("NomRayon")]
        public string NomRayon { get; set; }
        [BsonElement("ListProduct")]
        public List<Produit> Products { get; set; }
    }
}
