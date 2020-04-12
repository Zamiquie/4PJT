using MongoDB.Bson.Serialization.Attributes;
using SupMagasin.Model.ShopModel;
using System;
using System.Collections.Generic;

namespace SupMagasin.Model
{
    public class EmployeModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string NumeroSS { get; set; }
        [BsonElement("nom")]
        public string Nom { get; set; }
        [BsonElement("prenom")]
        public string Prenom { get; set; }
        [BsonElement("naiss_date")]
        public DateTime NaissanceDate { get; set; }
        [BsonElement("embauche_date")]
        public DateTime EmbaucheDate { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("phoneNum")]
        public string PhoneNum { get; set; }
        [BsonElement("idManager")]
        public EmployeModel Manager { get; set; }
        [BsonElement("sortie_date")]
        public DateTime SortieDate { get; set; }
        [BsonElement("coef_salaire")]
        public int coeeficcient { get; set; }
        [BsonElement("Sanction")]
        public List<SanctionModel> Sanction { get; set; }
        
    }
}