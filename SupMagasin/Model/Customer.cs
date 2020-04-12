using MongoDB.Bson.Serialization.Attributes;
using SupMagasin.Model.CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Id { get; set; }
        [BsonElement("Sexe")]
        public Sexe Sexe { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
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
        [BsonElement("RIB")]
        public List<BanqDataModel> BanqData { get; set; }
        [BsonElement("Phones")]
        public List<PhoneModel> Phones { get; set; }
        [BsonElement("Photo")]
        public byte[] Photo { get; set; }
        [BsonElement("Last_Time")]
        public DateTime Last_Time { get; set; }
        [BsonElement("AnnualFrequentation")]
        public int AnnualFrequentation { get; set; }
        [BsonElement("PanierMoyen")]
        public float PanierMoyen { get; set; }

    }

}
