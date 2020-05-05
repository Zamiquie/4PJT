using MongoDB.Bson;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SupMagasin.Utils;
using SupMagasin.Model.ShopModel;

namespace SupMagasin.Dal
{
    public class DalShop : Dal<Shop>
    {
        public DalShop() : base("Shop"){}

        #region Insert
        //Insert One Mag
        public async Task<string> AddMagasinAsync(Shop newMagasin)
        {
            newMagasin.ID = GenerateId(newMagasin);
            return await AddElement(newMagasin);
        }

        //Insert Multi Magasin
        public async Task<string> AddMultiMagasinAsync(List<Shop> ListMagasin)
        {
            foreach (var newMagasin in ListMagasin) { newMagasin.ID = GenerateId(newMagasin); }
            return await AddListOfElement(ListMagasin);
        }

        #endregion

        #region Query

        public string GetAllShop()
        {
            return  QueryAllElement().Result;
        }

        public async Task<string> GetShopByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(mag => mag.ID == id).ToJson();
        }

        public async Task<string> GetShopByCity(string city =null,string postalCode =null)
        {
            IAsyncCursor<Shop> resul;

            if(city == null && postalCode !=null) {
                resul = await Collection.FindAsync(mag => mag.PostalCode == postalCode);
            }
            else if (city != null && postalCode == null)
            {
                resul = await Collection.FindAsync(mag => mag.City == city);
            }
            else
            {
                resul = await Collection.FindAsync(mag => mag.City == city && mag.PostalCode == postalCode);
            }

            return resul.ToJson();

        }

        public async Task<string> GetEmployesByShop(string idMagasin)
        {
            var result =  await Collection.Find(mag => mag.ID == idMagasin).FirstOrDefaultAsync();
            return result.Employes.ToJson();
        }

        public async Task<string> GetRayonByShop(string idMagasin)
        {
            var result = await Collection.Find(mag => mag.ID == idMagasin).FirstOrDefaultAsync();
            return result.Rayons.ToJson();
        }

        public async Task<string> GetBorneByShop(string idMagasin)
        {
            var result = await Collection.Find(mag => mag.ID == idMagasin).FirstOrDefaultAsync();
            return result.Bornes.ToJson();
        }
        #endregion

        #region Update
        public async Task<string> UpdateMagasin(Shop currentMagasin)
        {
            return await UdpateElement(currentMagasin.ID.ToString(), currentMagasin);

        }
        #endregion

        #region Delete

        public async Task<string> RemoveMagasin(string id)
        {
            return await DeleteEntry(id);
        }
        public async Task<string> RemoveLotMagasin(List<Shop> MagasinsToDelete)
        {
            return await DeleteMultielement(MagasinsToDelete);
        }
        #endregion

        #region Enfants

        public async Task<string> AddEmployes(string id, EmployeModel employe)
        {
            //on filtre et on met
            var filter = Builders<Shop>.Filter.Eq("_id", id);
            var update = Builders<Shop>.Update.Push(mag => mag.Employes, employe);

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
        public async Task<string> AddBornes(string id, BorneModel borne)
        {
            //on filtre et on met
            var filter = Builders<Shop>.Filter.Eq("_id", id);
            var update = Builders<Shop>.Update.Push(mag => mag.Bornes, borne);

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
        public async Task<string> AddEmployes(string id, RayonModel rayon)
        {
            //on filtre et on met
            var filter = Builders<Shop>.Filter.Eq("_id", id);
            var update = Builders<Shop>.Update.Push(mag => mag.Rayons, rayon);

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

        private string GenerateId(Shop newMagasin)
        {
            return newMagasin.Enseigne.Substring(0, 3) + newMagasin.PostalCode.Substring(0,2) + newMagasin.PhoneNum.Substring(5, 4);
        }

        #endregion
    }
}
