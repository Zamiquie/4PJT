using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Model.CustomerModel;
using SupMagasin.Model.ProductModel;
using SupMagasin.Model.ShopModel;

namespace TestMongo
{
    class Program
    {
        static async Task Main(string[] args)
        {

            DateTime start = DateTime.Now;
            DalProduit dal = new DalProduit();

            var products = await CrawlerDistribution.CarrefourAsync();

            await dal.AddMultiProduit(products);

            TimeSpan rest = DateTime.Now - start;
            Console.WriteLine("Programme exectué en {0}", rest);



        }

    }
}
