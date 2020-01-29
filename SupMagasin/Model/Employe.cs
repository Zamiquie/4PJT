using System;

namespace SupMagasin.Model
{
    public class Employe
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime NaissanceDate { get; set; }
        public DateTime EmbaucheDate { get; set; }
        public string Email { get; set; }
        public Employe Manager { get; set; }
        public Magasin Magasin { get; set; }
        public DateTime SortieDate { get; set; }
        public int coeeficcient { get; set; }
        public bool isSanctionned { get; set; }
    }
}