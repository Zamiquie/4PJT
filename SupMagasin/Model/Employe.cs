using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SupMagasin.Model
{
    public class Employe
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }
        [BsonElement("nom")]
        public string Nom { get; set; }
        [BsonElement("prenom")]
        public string Prenom { get; set; }
        [BsonElement("num_SS")]
        public string NumeroSS { get; set; }
        [BsonElement("naiss_date")]
        public DateTime NaissanceDate { get; set; }
        [BsonElement("embauche_date")]
        public DateTime EmbaucheDate { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("idManager")]
        public Employe Manager { get; set; }
        [BsonElement("idMagasin")]
        public Magasin Magasin { get; set; }
        [BsonElement("sortie_date")]
        public DateTime SortieDate { get; set; }
        [BsonElement("coef_salaire")]
        public int coeeficcient { get; set; }
        [BsonElement("isSanctionned")]
        public bool isSanctionned { get; set; }
    }
}