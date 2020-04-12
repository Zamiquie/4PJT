using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model.ProductModel
{
    public class LotsModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("ArrivalDate")]
        public DateTime ArrivalDate { get; set; }
        [BsonElement("PeremptionDate")]
        public DateTime PeremptionDate { get; set; }
        [BsonElement("Stock")]
        public int Stock { get; set; }
    }
}
