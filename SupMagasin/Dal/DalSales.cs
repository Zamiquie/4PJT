using MongoDB.Bson;
using MongoDB.Driver;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupSales.Dal
{
    public class DalSales : Dal<Sale> {

        public DalSales() : base("Sales") { }

        #region Insert
        //Insert One Mag
        public async Task<string> AddSalesAsync(Sale newSales)
        {
            newSales.ID = GenerateId(newSales);
            return await AddElement(newSales);
        }

        //Insert Multi Sales
        public async Task<string> AddMultiSalesAsync(List<Sale> ListSales)
        {
            foreach (var newSales in ListSales) { newSales.ID = GenerateId(newSales); }
            return await AddListOfElement(ListSales);
        }

        #endregion

        #region Query

        public string GetAllSale()
        {
            return QueryAllElement().Result;
        }

        public async Task<string> GetSaleByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(sal => sal.ID == id).ToJson();
        }

        public async Task<string> GetSalesByIdUser(string idUser)
        {
            var result = await Collection.Find(sal => sal.IdCustomer == idUser).ToListAsync();
            return result.ToJson();
        }

        public async Task<string> GetSalesByIdProduit(string idProduit)
        {
            var result = await Collection.Find(sal => sal.ProduitVente.Any(i => i.IDProduct == idProduit)).ToListAsync();
            return result.ToJson();
        }

        public async Task<string> GetSalesByIdMagasin(string idShop)
        {
            var result = await Collection.Find(sal => sal.IdShop == idShop).ToListAsync();
            return result.ToJson();
        }

        #endregion

        #region Update
        public async Task<string> UpdateSales(Sale currentSales)
        {
            return await UdpateElement(currentSales.ID.ToString(), currentSales);

        }
        #endregion

        #region Delete

        public async Task<string> RemoveSales(string id)
        {
            return await DeleteEntry(id);
        }
        public async Task<string> RemoveLotSales(List<Sale> SalesToDelete)
        {
            return await DeleteMultielement(SalesToDelete);
        }
        #endregion

        #region Enfants

        public async Task<string> AddSaleProduct(string id, SaleProduct productSale)
        {
            //on filtre et on met
            var filter = Builders<Sale>.Filter.Eq("_id", id);
            var update = Builders<Sale>.Update.Push(sal => sal.ProduitVente, productSale);

            try
            {
                await Collection.UpdateOneAsync(filter, update);
                return true.ToJson();
            }
            catch (MongoException e)
            {

                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();
            }
        }
        
        #endregion

        #region Private 

        private string GenerateId(Sale newSales)
        {
            return newSales.VenteDate.ToString("dd_MM_yy_HH_mm") + newSales.IdCustomer+newSales.IdShop;
        }

        #endregion
    }

}

