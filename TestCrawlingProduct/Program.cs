using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestCrawlingProduct
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quel Magasin ? \t 'exit' pour quitter");
            bool continu = true;
            while (continu)
            {
                switch (Console.ReadLine().ToLower())
                {
                    case "carrefour":
                        await CrawlerDistribution.CarrefourAsync();
                        break;

                    case "auchan":
                        await CrawlerDistribution.AuchanAsync();
                        break;
                    case "leclerc":
                        await CrawlerDistribution.LeclercAsync();
                        break;
                    case "exit":
                        continu = false;
                        break;
                    default:
                        Console.WriteLine("J'ai pas compris");
                        break;

                }
            }
            Console.WriteLine("Salut à la prochaine");
        }

        

        
    }
}
