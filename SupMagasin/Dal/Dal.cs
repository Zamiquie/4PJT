using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    public abstract class Dal<T>
    {
        protected MongoClient Client { get; set; }
        protected IMongoDatabase Database { get; set; }
        protected IMongoCollection<T> Collection { get; set; }

        #region Constructeur
        public Dal(string stringConnection,string dbName, string colletionName)
        {
            Client = new MongoClient();
            Database = Client.GetDatabase(dbName);
            Collection = Database.GetCollection<T>(colletionName);
        }

        public Dal(string dbName, string colletionName)
        {
            Client = new MongoClient();
            Database = Client.GetDatabase(dbName);
            Collection = Database.GetCollection<T>(colletionName);
        }

        public Dal(string collectionName)
        {
            Client = new MongoClient();
            Database = Client.GetDatabase("IaShop");
            Collection = Database.GetCollection<T>(collectionName);
        }
        #endregion

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
                Debug.WriteLine(e.Message);
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
                Debug.WriteLine(e.Message);
                return false.ToJson();

            }
        }

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
                Debug.WriteLine(e.Message);
                return false.ToJson();

            }

        }

        //Delete  MultiElement
        protected async Task<string> DeleteMultielement(List<T> listOfEntrie)
        {
            try
            {
                //await Collection.DeleteManyAsync<T>(listOfEntrie);
                return true.ToJson();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false.ToJson();

            }
        }

        //Query All Element
        protected async Task<string> QueryAllElement()
        {
            try
            {
                var list =  Collection.Find(_ => true).ToList();
                return list.ToJson();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return e.Message.ToJson();

            }
        }

        //Query Element By Id (a verifier)
        protected async Task<List<T>> QueryElementById()
        {
            try
            {
                var list = Collection.Find<T>(_ =>true).ToList();
                return list;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new List<T>();

            }
        }

        //Query Update 
        protected async Task<string> UdpateElement(string ID,T element)
        {
            try
            {
                var list = await Collection.FindAsync(x => true);
                return list.ToJson();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return e.Message.ToJson();
            }
        }

    }
}
