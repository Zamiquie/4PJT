using MongoDB.Bson.Serialization.Attributes;

namespace SupMagasin.Model
{
    public class Fournisseur
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("Denomination")]
        public string Denomination { get; set; }
        [BsonElement("Adress")]
        public string Adress { get; set; }
        [BsonElement("City")]
        public string City { get; set; }
        [BsonElement("Postal_Code")]
        public string PostalCode { get; set; }
    }
}