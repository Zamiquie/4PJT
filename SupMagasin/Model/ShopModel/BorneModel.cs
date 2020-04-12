using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class BorneModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("position")]
        public string Position { get; set; }
        [BsonElement("etat_Borne")]
        public EtatBorne EtatBorne { get; set; }
    }
}
