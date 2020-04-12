using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class PhoneModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("Mac_Adress")]
        public string MAC { get; set; }
        [BsonElement("Mark")]
        public string Marque { get; set; }
        [BsonElement("Model")]
        public string Model { get; set; }
        
    }
}
