using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestMongo
{
    class Models
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("adress")]
        public string adress { get; set; }
        [BsonElement("city")]
        public string city { get; set; }
        [BsonElement("postal_code")]
        public string Postal_Code { get; set; }
    }
}
