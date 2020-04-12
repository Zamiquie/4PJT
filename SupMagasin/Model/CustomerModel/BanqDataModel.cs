using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model.CustomerModel
{
    public class BanqDataModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Id { get; set; }
        [BsonElement("CardNum")]
        public string CardNum { get; set; }
        [BsonElement("Owner")]
        public string Owner { get; set; }
        [BsonElement("Key")]
        public string Key { get; set; }
        [BsonElement("RIB")]
        public string RIB { get; set; }
        [BsonElement("PeremptionDate")]
        public DateTime PeremptionDate { get; set; }
    }
}
