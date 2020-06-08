using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using SupMagasin.Model;
using SupMagasin.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    /*
     * Class Abstraite servant de base au autre. 
     * Elle est remplis de type généric afin de prévoir de nouvelle tables.
     */
    public abstract class Dal<T>
    {
        protected MongoClient Client { get; set; }
        protected IMongoDatabase Database { get; set; }
        protected IMongoCollection<T> Collection { get; set; }
        
        #region Constructeur
 
        public Dal(string collectionName)
        {
            Client = new MongoClient(ConnectionMongo.ServeurProd);
            Database = Client.GetDatabase("SupMagasin");

            //si on debug on supprime la collection
            /*if (Debugger.IsAttached)
            {
                Database.DropCollection(collectionName);
            }*/

            Collection = Database.GetCollection<T>(collectionName);
        }
        #endregion

        #region Insert
        //Insert One element
        protected async Task<string> AddElement(T newEntri)
        {
            try
            {
                await Collection.InsertOneAsync(newEntri);
                return true.ToJson();
            }
            catch (Exception e)
            {
               
                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }

        }

        //Insert MultiElement
        protected async Task<string> AddListOfElement(List<T> listOfEntrie)
        {
            try
            {
                await Collection.InsertManyAsync(listOfEntrie);
                return true.ToJson();
            }
            catch (Exception e)
            {
                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }
        }

        #endregion

        #region Query
        //Query All Element
        protected async Task<string> QueryAllElement()
        {
            try
            {
                var list = await Collection.Find(_ => true).ToListAsync();
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
               
                return list.ToJson(jsonWriterSettings);
            }
            catch (Exception e)
            {
                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return e.Message.ToJson();

            }
        }

        //Query Element By Id (a verifier)
        protected async Task<T> QueryElementById(string id)
        {
            try
            {
                var element = await Collection.Find<T>(Builders<T>.Filter.Eq("_id",id)).FirstOrDefaultAsync();
                return element;
            }
            catch (Exception e)
            {
                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return default(T);

            }
        }
        #endregion

        #region Delete
        //Delete One element
        protected async Task<string> DeleteEntry(string deleteEntris)
        {
            try
            {
                var x = await Collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id",deleteEntris));
                if (x.DeletedCount == 0) { return new string("Not element found").ToJson(); }//si auxun elem supprimer 
                return true.ToJson();
            }
            catch (Exception e)
            {
                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }

        }

        //Delete  MultiElement
        protected async Task<string> DeleteMultielement(List<T> listOfEntrie)
        {
            try
            {
                //await Collection.DeleteManyAsync<T>(listOfEntrie);
                return false.ToJson();
            }
            catch (Exception e)
            {
                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }
        }
        #endregion

        #region Update
        //Query Update 
        protected async Task<string> UdpateElement(string id,T element)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", id);
                var replaceElement = await Collection.ReplaceOneAsync(filter, element);
                if (replaceElement.IsModifiedCountAvailable) 
                {
                    return true.ToJson();
                }
                else
                {
                    return new string("Error while update!! Try Again").ToJson();
                }
            }
            catch (Exception e)
            {
                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return e.Message.ToJson();
            }
        }
        #endregion

    }
}
