using MongoDB.Bson;
using SupMagasin.Dal;
using SupMagasin.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    public class Dal_Produit : Dal<Produit>
    {

        public Dal_Produit() : base("Produit"){}


        public async Task<string> AddProduitAsync(Produit newProduit)
        {
            return await AddElement(newProduit);
        }

        public async Task<string> GetAllProduit()
        {
            return await QueryAllElement();
        }

        public async Task<string> GetProduitByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(mag => mag.ID == id).ToJson();
        }
        public async Task<string> UpdateProduit(Produit currentProduit)
        {
            return await UdpateElement(currentProduit.ID.ToString(), currentProduit);

        }

        #region Delete
        public async Task<string> RemoveProduit(string id)
        {
            return await DeleteEntry(id);
        }
        public async Task<string> RemoveLotProduit(List<Produit> ProduitsToDelete)
        {
            return await DeleteMultielement(ProduitsToDelete);
        }
        #endregion
    }
}
