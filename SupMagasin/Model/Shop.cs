using MongoDB.Bson.Serialization.Attributes;
using SupMagasin.Model.ShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Shop
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("Enseigne")]
        public string Enseigne { get; set; }
        [BsonElement("adress")]
        public string Adress { get; set; }
        [BsonElement("postal_code")]
        public string PostalCode { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("PhoneNum")]
        public string PhoneNum { get; set; }
        [BsonElement("idResponsable")]
        public int IdResponsable { get; set; }
        [BsonElement("Rayons")]
        public List<RayonModel> Rayons { get; set; }
        [BsonElement("date_creation")]
        public DateTime DateCreation { get; set; }
        [BsonElement("Employees")]
        public List<EmployeModel> Employes { get; set; }
        [BsonElement("Bornes")]
        public List<BorneModel> Bornes { get; set; }
    }
}
