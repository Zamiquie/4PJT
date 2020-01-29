namespace SupMagasin.Model
{
    public class ProduitVente
    {
        public Vente Vente { get; set; }
        public Produit Produit { get; set; }
        public int Quantite { get; set; }
        public float PU { get; set; } // prix par produit
    }
}