using System;

namespace SupMagasin.Model
{
    public class Stock
    {
        public int ID { get; set; }
        public DateTime PeremptionDelay { get; set; }
        public int Quantite { get; set; }
        public DateTime EntryDate { get; set; }
    }
}