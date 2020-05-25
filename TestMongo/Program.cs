using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using SupMagasin.Utils;

namespace TestMongo
{
    class Program
    {
        static async Task Main(string[] args)
        {
        /*  #region Crawler Carrefour
             DateTime start = DateTime.Now;
             DalProduit dal = new DalProduit();
             try
             {
                 var products = await CrawlerDistribution.CarrefourAsync();
                 await dal.AddMultiProduit(products);
             }
             catch (HttpRequestException e)
             {
                 Console.WriteLine("Carrefour bloque la connexion : \n {0}",e.Message);
             }


             TimeSpan rest = DateTime.Now - start;
             Console.WriteLine("Programme exectué en {0}", rest);
            #endregion*/

        }
        


    }
}
