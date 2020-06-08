using SupMagasin.Dal;
using SupMagasin.Model;
using SupSales.Dal;
using Swashbuckle.AspNetCore.Swagger;
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
            DalCustomer dal = new DalCustomer();
            var pro = dal.GetCustomerByEmail("jean.patrick@free.luc").Result;
            AlgoPromo(pro);

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

            List<Sale> salesByCustomer = await new DalSales().GetSalesByIdUser(customer.Id);

            if (salesByCustomer.Count == 0)
            {
                PromoZeroVente(customer);
            }
            else
            {
                //receptacle des id des produits
                var produitPromos = new List<string>();
                //var de test
                var ProduitPlusFrequent = salesByCustomer.Select(pr => pr.ProduitVente.GroupBy(pv => pv.IDProduct).OrderByDescending(pr => pr.Count()).FirstOrDefault());
                var ProduitLePlusAchete = salesByCustomer.Select(pr => pr.ProduitVente.GroupBy(pv => pv.Quantite).OrderByDescending(pr => pr.Count()).FirstOrDefault());
                var ProduitLePlusCherAchete = salesByCustomer.Select(pr => pr.ProduitVente.Max(pv => pv.PU)).First();



                produitPromos.Add("Man0423555934");
                produitPromos.Add("bag1457249632");

                CreatePromotion(customer, null, produitPromos);

                Console.WriteLine("{0}\n{1}", ReadPromoFile(customer.Id)[0],ReadPromoFile(customer.Id)[1]);
            }

        }
        //function pour gerer si il n'y à pas de vente
        public static void PromoZeroVente(Customer customer)
        {
            //on determine l'age
            int ageClient = DateTime.Now.Year - customer.BirthdayDay.Year;

            //on affecte la categorie
            switch (ageClient)
            {
                //on creer la promotion
                case int _ when (ageClient <= 25):
                    CreatePromotion(customer, Categorie.NewTech);
                    break;
                case int a when (ageClient > 25 && ageClient <= 50):
                    CreatePromotion(customer, Categorie.Mobilier);
                    break;
                case int b when (ageClient > 50):
                    CreatePromotion(customer, Categorie.Alimentaire);
                    break;
            }

        }

        //function pour crere les promotions
        private static void CreatePromotion(Customer customer, Categorie? categorie = null , List<string>? idProduits = null)
        {
            //si le produit est non null
            if (idProduits != null || idProduits.Count != 0)
            {
                //stream du fichier
                using (var streamWriter = new StreamWriter(new FileStream(@"C:/Moi/promo.temp"/*Directory.GetCurrentDirectory() + @"/temps/promo.temp"*/, FileMode.OpenOrCreate, FileAccess.Write), Encoding.UTF8))
                {
                    string ligne_Promo = "["+customer.Id+",{";

                    foreach (string prod in idProduits)
                    {
                        ligne_Promo = ligne_Promo + prod + "|0.2;";
                    }

                    ligne_Promo =  ligne_Promo + "}]";

                    
                    streamWriter.WriteLine(ligne_Promo);
                    streamWriter.Close();
                }

            }
        }

        private static string[] ReadPromoFile(string customerId){

            Dictionary<string, string[]> promoDictionary = new Dictionary<string,string[]>();
            using (var streamRide = new StreamReader(new FileStream(@"C:/Moi/promo.temp"/*Directory.GetCurrentDirectory() + @"/temps/promo.temp"*/, FileMode.OpenOrCreate, FileAccess.Read), Encoding.UTF8))
            {
                var file = streamRide.ReadToEnd();
                file = file.Replace("\r", String.Empty);
                file = file.Replace("[", String.Empty);
                file = file.Replace("]", String.Empty);

                //on retransforme ne tableau
                var listPromo = new List<string>(file.Split('\n'));
                //on supprime la dernière entrée pour les bugs
                listPromo.RemoveAt(listPromo.Count - 1);


                //on retransforme en promoDictionary
                promoDictionary = listPromo.ToDictionary(x => x.Split(',')[0], x => x.Split(',')[1].Replace("{",String.Empty).Replace("}",String.Empty).Split(';'));

                return promoDictionary[customerId];

            }
            

        }
    }
}
