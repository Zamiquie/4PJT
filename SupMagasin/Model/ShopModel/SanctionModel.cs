using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model.ShopModel
{
    public class SanctionModel
    {
        
        [BsonElement("Date")]
        public DateTime fdate { get; set; }
        [BsonElement("Nature")]
        public string Nature { get; set; }
        [BsonElement("Commentary")]
        public string Commentary { get; set; }
        [BsonElement("Sanction")]
        public Sanction Sanction { get; set; }
    }
}
