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

            DalShop dal = new DalShop();


            await dal.AddMagasinAsync(new Shop()
            {
                Enseigne = "ShopTest",
                Adress = "25 rue des coquelicots",
                Bornes = new List<BorneModel>
                {
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.Allume,
                        Position = "Au dessus du frigo"
                    },
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.Casse,
                        Position = "Au dessus du frigo"
                    },
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.EnStock,
                        Position = "Au dessus du frigo"
                    },
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.Eteint,
                        Position = "Au dessus du frigo"
                    },
                },
                City = "Carcassonne",
                PostalCode = new Random().Next(10000, 95999).ToString(),
                DateCreation = DateTime.Now,
                Email = "test@test.com",

                Employes = new List<EmployeModel>
                {
                   new EmployeModel() {
                        NumeroSS = "12425485845412",
                        Prenom = "Baquet",
                        Nom = "Aymeric",
                        NaissanceDate = DateTime.Parse("06/04/1994"),
                        EmbaucheDate = DateTime.Parse("01/09/2018"),
                        Email ="aymericbaquet@test.ovh",
                        coeeficcient = 142 ,
                        PhoneNum = "0102030405",
                        Sanction = new List<SanctionModel>
                        {
                            new SanctionModel()
                            {
                                fdate = DateTime.Parse("01/12/2018"),
                                 Nature = "Exibitionisme",
                                 Commentary = "Surpris entrain d'embrasser un manequin de présentation dans la réserve",
                                 Sanction = Sanction.Avertissement
                            }
                        }
                    }
                },
                PhoneNum = "5515145414",
                Rayons = null

            });

            List<Shop> list = new List<Shop>();

            for (int x = 0; x < 1500; x++)
            {
                list.Add(new Shop()
                {
                    Enseigne = "ShopTest",
                    Adress = "25 rue des coquelicots",
                    Bornes = new List<BorneModel>
                {
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.Allume,
                        Position = "Au dessus du frigo"
                    },
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.Casse,
                        Position = "Au dessus du frigo"
                    },
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.EnStock,
                        Position = "Au dessus du frigo"
                    },
                    new BorneModel()
                    {
                        EtatBorne = EtatBorne.Eteint,
                        Position = "Au dessus du frigo"
                    },
                },
                    City = "Carcassonne",
                    PostalCode = new Random().Next(10000, 95999).ToString(),
                    DateCreation = DateTime.Now,
                    Email = "test@test.com",
                    Employes = new List<EmployeModel> {
                    new EmployeModel() {
                        NumeroSS = "12425485845412",
                        Prenom = "Baquet",
                        Nom = "Aymeric",
                        NaissanceDate = DateTime.Parse("06/04/1994"),
                        EmbaucheDate = DateTime.Parse("01/09/2018"),
                        Email ="aymericbaquet@test.ovh",
                        coeeficcient = 142 ,
                        PhoneNum = "0102030405",
                        Sanction = new List<SanctionModel>
                        {
                            new SanctionModel()
                            {
                                fdate = DateTime.Parse("01/12/2018"),
                                 Nature = "Exibitionisme",
                                 Commentary = "Surpris entrain d'embrasser un manequin de présentation dans la réserve",
                                 Sanction = Sanction.Avertissement
                            }
                        }
                    }
                },
                    PhoneNum = "011514" + new Random().Next(1000, 9999).ToString(),
                    Rayons = null
                    
            });

            };
            
            await dal.AddMultiMagasinAsync(list);
            //Console.ReadLine();
        }

        //Test Mango DB
        private static void TestMongo()
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
            List<Models> listAdr = new List<Models>();
            listAdr.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 100).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });
            listAdr.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 100).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });
            listAdr.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 100).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });

            collection.InsertOne(new Model("105", "Aymeric", "Baquet", 75.50f, listAdr));

            //Add Multiple Data
            var documents = new List<Model>();
            for (int x = 0; x < 100; x++)
            {
                List<Models> list = new List<Models>();
                list.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 1547).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });
                list.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 1547).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });
                list.Add(new Models() { adress = "26 rue du corps", city = "Allevard", ID = new Random().Next(0, 1547).ToString(), Postal_Code = new Random().Next(0, 99).ToString() + "000" });

                documents.Add(new Model(x.ToString(), "Aymeric_" + x.ToString(), "Baquet_" + x.ToString(), x * 5f, list));
            }
            //var document = Enumerable.Range(0, 70).Select(i => new Model(i.ToString(), "Aymeric_" + i.ToString(), "Baquet_" + i.ToString(), i * 5f, list));

            adress.InsertMany(listAdr);
            collection.InsertMany(documents);


            //QUERY //


            // var documentSearch = collection.Find(new BsonDocument()).FirstOrDefault();
            //Console.WriteLine(documentSearch.ToJson());

            //Query with research Element
            var documentbyID = collection.Find(doc => doc.ID == "8").FirstOrDefault();
            Console.WriteLine(documentbyID.ToJson());

            //Query with research Element on Children
            var result = collection.AsQueryable<Model>()
                .Where(chi => chi.Adress.Any<Models>(enf => enf.Postal_Code.StartsWith("79"))).ToList();

            Console.WriteLine("\n query with children : \n {0}", result.ToJson());


            //Query Many
            //var documentWithcity = collection.AsQueryable().Where(x => x.Adress == "1").ToList();
            //Console.WriteLine(documentWithcity.ToJson());


            // UPDATE // 

            //Insert Children in Parent
            var filter = Builders<Model>.Filter.Eq("_id", "0");

            var update = Builders<Model>.Update.Push<Models>(enf => enf.Adress, new Models
            {
                adress = "15 rue des paquerettes",
                city = "Lorgnant",
                Postal_Code = "4585"
            });
            collection.UpdateOne(filter, update);





            Console.ReadLine();

        }

        //Function de Creation de la database
        private static async void TestApiMongo()
        {
            //Creation de la collection Magasin
            DalShop dalMagasin = new DalShop();
            //Ajout d'un Magasin Test
            await dalMagasin.AddMagasinAsync(new Shop()
            {
                ID = "M01",
                Enseigne = "Magasin Test",
                Adress = "26 rue des test",
                City = "TestVille",
                PostalCode = "73058",
                DateCreation = DateTime.Now,
                Email = "magasintest@test.com",
                Bornes = new List<BorneModel> {
                    new BorneModel(){
                        EtatBorne = EtatBorne.Allume,
                        ID = "B01",
                        Position = "Au dessus de l'entreé"
                    }
                },
                Employes = new List<EmployeModel> {
                    new EmployeModel() {
                        NumeroSS = "12425485845412",
                        Prenom = "Baquet",
                        Nom = "Aymeric",
                        NaissanceDate = DateTime.Parse("06/04/1994"),
                        EmbaucheDate = DateTime.Parse("01/09/2018"),
                        Email ="aymericbaquet@test.ovh",
                        coeeficcient = 142 ,
                        PhoneNum = "0102030405",
                        Sanction = new List<SanctionModel>
                        {
                            new SanctionModel()
                            {
                                fdate = DateTime.Parse("01/12/2018"),
                                 Nature = "Exibitionisme",
                                 Commentary = "Surpris entrain d'embrasser un manequin de présentation dans la réserve",
                                 Sanction = Sanction.Avertissement
                            }
                        }
                    }
                },
                Rayons = new List<RayonModel>
                {
                   new RayonModel()
                   {
                       ID = "R01",
                       NomRayon ="Fruit & Legume"
                   }
                }
            });

            //Creation de la Collection User 
            DalCustomer dalCustomer = new DalCustomer();
            //Ajout d'un Utilisateur Test 
            await dalCustomer.AddCustomerAsync(new Customer()
            {
                Id = "C01",
                FirstName = "Jean",
                Name = "Patrick",
                BirthdayDay = DateTime.Parse("01/05/2000"),
                City = "TestCotage",
                Postal_Code = "05747",
                Adress = "25 rue des coquelicots",
                Email = "jean.patrick@free.luc",
                Phones = new List<PhoneModel>
                {
                    new PhoneModel()
                    {
                        ID = "PH01",
                        MAC = "54654534-jgdfj",
                        Marque = "Apple",
                        Model = "Beurk :p"
                    }
                },
                AnnualFrequentation = 15,
                Last_Time = DateTime.Now.AddMonths(-5),
                BanqData = new List<BanqDataModel>
                {
                    new BanqDataModel()
                    {
                        CardNum ="1111222233334444",
                        Key = "987",
                        Owner ="Martin Kevin",
                        PeremptionDate = DateTime.Now.AddMonths(-15)
                    },
                    new BanqDataModel()
                    {
                        CardNum ="9999777788885555",
                        Key = "147",
                        Owner ="Durant Patricia",
                        PeremptionDate = DateTime.Now.AddMonths(12)
                    },

                },
                PanierMoyen = 45.12f,
                Sexe = Sexe.Homme
            });


            //Creation de la collection Produit
            DalProduit dalProduit = new DalProduit();
            //Ajout d'un produit Test
            await dalProduit.AddProduitAsync(new Produit()
            {
                ID = "P01",
                Designation = "Mangue",
                Description = "Bon Produit qui se mangen avec les doigts",
                BuyPrice = 0.5f,
                SalePrice = 1.5f,
                Categorie = Categorie.Alimentaire,
                Gamme = Gamme.Moyen,
                Fournisseur = new Fournisseur()
                {
                    Denomination = "Les Bons fruits de Bamako",
                    Adress = "25 rue de l'alloutette",
                    City = "Rungis",
                    PostalCode = "95014"
                },
                IsSaling = true,
                Weight = 0.2f,
                Lots = new List<LotsModel>
                    {
                        new LotsModel()
                        {
                            ArrivalDate = DateTime.Now.AddDays(-15),
                            PeremptionDate = DateTime.Now.AddDays(5),
                            Stock = 15474,
                            ID ="1547-20-11-258"
                        }

                    },
                Commentaire = new List<Commentaire>
                    {
                        new Commentaire()
                        {
                            fdate = DateTime.Now.AddYears(-1),
                            IDClient = "C01",
                            Note = 5,
                            Comment = "Fruit délicieux, pulpe sucrée. Je recommande"
                        },
                        new Commentaire()
                        {
                            fdate = DateTime.Now.AddMonths(-4),
                            IDClient = "C03",
                            Note = 1,
                            Comment = "Pas reçu à le manger. La peau se mache mal et le noyau est tellement dur que je me suis cassé une dent."
                        },
                        new Commentaire()
                        {
                            fdate = DateTime.Now.AddDays(-15),
                            IDClient = "C02",
                            Note = 3,
                            Comment = "Cuit c'est pas très bon, mais cru ça passe."
                        }
                    }
            });


        }
    }
}
