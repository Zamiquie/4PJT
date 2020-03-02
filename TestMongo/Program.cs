using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TestMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            //INJECTION MANGO DB NORMAL
            //Install Client
            var client = new MongoClient();

            //connect to Database
            var database = client.GetDatabase("testMongo");

            //Remove Collection
            database.DropCollection("testMongo");
            database.DropCollection("testMongoClass");

            //Connet to Collection or create if not exist
            var collection = database.GetCollection<Model>("testMongo");
            var adress = database.GetCollection<Models>("testMongoClass");


            //Add Data

            List<Models> list = new List<Models>();
            list.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 100).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });
            list.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 100).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });
            list.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 100).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });

            collection.InsertOne(new Model("105", "Aymeric", "Baquet", 75.50f, list));

            //Add Multiple Data 
            //var documents = Enumerable.Range(0, 70).Select(i => new Model(i.ToString(), "Aymeric_" + i.ToString(), "Baquet_" + i.ToString(), i * 5f, list));
            //adress.InsertMany(list);
            //collection.InsertMany(documents);
            

            //Query 
            var documentSearch = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine(documentSearch.ToJson());

            //Query with research Element
            var documentbyID = collection.Find(doc => doc.ID == "8").FirstOrDefault();
            Console.WriteLine(documentbyID.ToJson());

            //Query Many
            //var documentWithcity = collection.AsQueryable().Where(x => x.Adress == "1").ToList();
            //Console.WriteLine(documentWithcity.ToJson());

        }
    }
}
