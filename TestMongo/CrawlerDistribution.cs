using HtmlAgilityPack;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestMongo
{
    class CrawlerDistribution
    {
        public static async Task<List<Produit>> CarrefourAsync()
        {
            var produitToAdd = new List<Produit>();

            for (int x = 1; x < 36831 / 60; x++)
            {
                Console.Write("{0} ", x);
                if(x%25 == 0)
                {
                    Console.WriteLine("");
                }
               
                var url = "https://www.carrefour.fr/r?noRedirect=1&page=" + x.ToString();
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url); // on recupere le contenu de la page
                var htmlDocument = new HtmlDocument(); // on creer un nouvel objet HtmlDocument
                htmlDocument.LoadHtml(html);
                var divs = htmlDocument.DocumentNode.Descendants("article").ToList();

                //var product = new List<Product>();
                foreach (var div in divs)
                {
                    string PriceKG = "";
                    var Denomination = div.Descendants("a").FirstOrDefault().InnerText.ToLower().Replace("\n", "").Replace(" ", "_").Remove(0, 10);
                    var Price = div.Descendants("span").Where(
                        pr => pr.GetAttributeValue("class", "") == "ds-title ds-title--m product-card-price__price--final"
                        || pr.GetAttributeValue("class", "") == "ds - title ds - title--m product - card - price__price--final product - card - price__discount--fid"
                        || pr.GetAttributeValue("class", "") == "ds-title ds-title--m product-card-price__price--final product-card-price__discount--fid"
                        || pr.GetAttributeValue("class", "") == "ds-title ds-title--m product-card-price__price--final product-card-price__discount--promo"
                        )
                        .FirstOrDefault().InnerText.ToLower().Replace("\n", "").Replace(" ", "_").Replace("€", "").Remove(0, 2);
                    var PriceKGHtml = div.Descendants("div").Where(kg => kg.GetAttributeValue("class", "") == "ds-body-text ds-body-text--size-s ds-body-text--color-standard-3 product-card-per-unit-label").FirstOrDefault();
                    if (PriceKGHtml != null) { PriceKG = PriceKGHtml.InnerHtml.ToLower().Replace("\n", "").Replace(" ", "_").Remove(0, 5); }

                   /* product.Add(new Product()
                    {
                        Denomination = Denomination,
                        Price = Price,
                        PriceKG = PriceKG
                    });*/

                    produitToAdd.Add(new Produit()
                    {
                        Designation = Denomination,
                        SalePrice = float.Parse(Price),
                        Weight  = new Random().Next(1,9),
                        Categorie = Categorie.Alimentaire,
                        Gamme = Gamme.Moyen,
                        Description = Denomination

                    });
                  /*  foreach (Product prod in product)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Crawler Test sur : {0}", "carrefour.fr");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" Pr: {0}", prod.Denomination);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" P: {0}", prod.Price);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" PKg: {0} \n", prod.PriceKG);
                    }*/
                }

            }

            return produitToAdd;


        }
        public static async Task LeclercAsync()
        {

        }

        public static async Task AuchanAsync()
        {

        }

    }

    public class Product
    {
        public string Denomination { get; set; }
        public string Price { get; set; }
        public string PriceKG { get; set; }
    }
}

