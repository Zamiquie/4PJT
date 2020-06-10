using MongoDB.Bson.Serialization.Attributes;

namespace SupMagasin.Model
{
    public class SaleProduct
    {
        [BsonElement("IdProduct")]
        public string  IdProduct { get; set; }
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
        [BsonElement("UnitPrice")]
        public float UnitPrice { get; set; } // prix par produit
        [BsonElement("AmountPromo")]
        public float AmountPromo { get; set; }
    }
}