using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Magasin
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("adress")]
        public string Adress { get; set; }
        [BsonElement("postal_code")]
        public string Postal_Code { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("idResponsable")]
        public Employe IdResponsable { get; set; }
        [BsonElement("NbRayon")]
        public int NbRayon { get; set; }
        [BsonElement("date_creation")]
        public DateTime DateCreation { get; set; }


    }
}
