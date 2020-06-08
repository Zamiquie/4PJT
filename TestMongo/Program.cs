using System;
using System.Threading.Tasks;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Utils;
using System.Text.Json;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SupSales.Dal;

namespace TestMongo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //enregistrement de la vente
            DalSales dalSales = new DalSales();
     

            var listProduct = new List<SaleProduct>();
            listProduct.Add(new SaleProduct()
            {
                AmountPromo = 0,
                Quantite = 5, 
                PU = 3.99f,
                IDProduct = "Man0423555934"
            });

            var sale = new Sale()
            {
                IdCustomer = "25XPJ74",
                IdPhone = "PH01",
                IdShop = "Mag737044",
                ProduitVente = listProduct,
                TotalAmount = listProduct.Sum(pr => pr.Quantite * pr.PU),
                isPayed = true,
                VenteDate = DateTime.Now
            };


            var saleFinish = await dalSales.AddSalesAsync(sale);
            //récupération des datas liès  la vente
            DalCustomer dal = new DalCustomer();
            var customer = await dal.GetCustomerByID(sale.IdCustomer);
            DalShop dalShop = new DalShop();
            var shop = await dalShop.GetShopByID(sale.IdShop);
            //création du document
            await GenerateDocument.GenerateFacture(sale, customer, shop);
            //envois d'un email avec facture
            new Mailling().sendFactureByMail(sale,customer);

            //renvois 
           







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
