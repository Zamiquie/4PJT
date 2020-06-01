using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Model.CustomerModel;
using SupMagasin.Model.ProductModel;
using SupMagasin.Model.ShopModel;
using SupMagasin.Utils;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestMongo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DalProduit dal = new DalProduit();
            var produit = JsonSerializer.Deserialize<Produit>(await dal.GetProduitByID("bag1485290176"));

            var qrCode = QrCodeHandler.GenerateStringQrCode(produit, "Mag5");

            Console.WriteLine("QrCode : ", qrCode);

            var produitAfter = QrCodeHandler.DecrypteStringQrCode(qrCode);

            Console.WriteLine("Produit : {0}", produitAfter.ToJson());








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
