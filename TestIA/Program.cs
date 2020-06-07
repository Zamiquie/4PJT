using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupSales.Dal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using static SupMagasin.Utils.classDataSoft;

namespace TestIA
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var url = "https://data.opendatasoft.com/api/records/1.0/search/?dataset=donnees-du-repertoire-national-des-elus%40public&facet=code_sexe&facet=date_de_naissance&facet=libelle_commune&facet=code_departement&facet=nom_dept&facet=nom_epci";
            HttpRequestMessage httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url); 

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.SendAsync(httpRequest);
            var content = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(await response.Content.ReadAsStringAsync());


            Console.WriteLine(content);







            /*  Dictionary<string, string> MacIdCustomer = new Dictionary<string, string>();
              using (var streamRide = new StreamReader(new FileStream(@"C:\Moi\bluCust.txt", FileMode.OpenOrCreate, FileAccess.Read), Encoding.UTF8))
              {
                  var file = streamRide.ReadToEnd();
                  file = file.Replace("\r", String.Empty);
                  file = file.Replace("[", String.Empty);
                  file = file.Replace("]", String.Empty);
                  var listMac = new List<string>(file.Split('\n'));
                  listMac.RemoveAt(file.Split(',').Length - 1);

                  MacIdCustomer = listMac.ToDictionary(x => x.Split(',')[0], x => x.Split(',')[0]);

              }

              if (MacIdCustomer.ContainsKey("7E:7D:83:8A:62:60"))
              {
                  var macCurrent = MacIdCustomer["7E:7D:83:8A:62:60"];

                  var customer = new DalCustomer().GetCustomerByMacTelephone(macCurrent).Result;

                  if (customer == null)
                  {
                      Console.WriteLine("Client n'existe pas");

                  }
                  else
                  {
                      AlgoPromo(customer);
                  }
              }
              else
              {

              }*/

        }

        public static async void AlgoPromo(Customer customer)
        {

            List<Sale> salesByCustomer = System.Text.Json.JsonSerializer.Deserialize<List<Sale>>(await new DalSales().GetSalesByIdUser(customer.Id));

            if (salesByCustomer.Count == 0)
            {
                Console.WriteLine("Pas de ventes pour ce client");
            }
            else
            {
                var ProduitPlusFrequent = salesByCustomer.Select(pr => pr.ProduitVente.GroupBy(pv => pv.IDProduct).OrderByDescending(pr => pr.Count()).FirstOrDefault());
                var ProduitLePlusAchete = salesByCustomer.Select(pr => pr.ProduitVente.GroupBy(pv => pv.Quantite).OrderByDescending(pr => pr.Count()).FirstOrDefault());
                var ProduitLePlusCherAchete = salesByCustomer.Select(pr => pr.ProduitVente.Max(pv => pv.PU)).First();
            }

        }
    }
}
