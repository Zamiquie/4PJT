using MongoDB.Bson.Serialization.Attributes;

namespace SupMagasin.Model
{
    public class SaleProduct
    {
        [BsonElement("IdProduct")]
        public string  IDProduct { get; set; }
        [BsonElement("Quantity")]
        public int Quantite { get; set; }
        [BsonElement("UnitPrice")]
        public float PU { get; set; } // prix par produit
        [BsonElement("AmountPromo")]
        public float AmountPromo { get; set; }
    }
}