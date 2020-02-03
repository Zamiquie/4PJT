using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Id { get; set; }
        [BsonElement("Sexe")]
        public Sexe Sexe { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("BirthDay")]
        public DateTime BirthdayDay { get; set; }
        [BsonElement("Adress")]
        public string Adress { get; set; }
        [BsonElement("Postal_Code")]
        public string Postal_Code { get; set; }
        [BsonElement("City")]
        public string City { get; set; }
        [BsonElement("Last_Time")]
        public DateTime Last_Time { get; set; }
        [BsonElement("AnnualFrequentation")]
        public int AnnualFrequentation { get; set; }
        [BsonElement("PanierMoyen")]
        public float PanierMoyen { get; set; }
        [BsonElement("RIB")]
        public string RIB { get; set; }
        [BsonElement("TempsMoyen")]
        public DateTime TempsMoyen { get; set; }
        [BsonElement("Photo")]
        public byte[] Photo { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }

    }
}
