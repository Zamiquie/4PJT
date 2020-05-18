using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Model.CustomerModel;
using SupMagasin.Model.ProductModel;
using SupMagasin.Model.ShopModel;
using SupMagasin.Utils;

namespace SupMagasin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            const string corsURLKEYS = "Security:Cors:Url";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false; // seulement le HTTPS
                    option.SaveToken = true; // On ssave le token
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true, // verification du issuer
                        ValidateAudience = true, // verification de l'Audiance 
                        ValidateLifetime = true, // Verification de la durée de vie
                        ValidateIssuerSigningKey = true, // vérification de al clé
                        ValidIssuer = Configuration["jwt:issuer"], // qui est l'emmeteur ?
                        ValidAudience = Configuration["jwt:audiance"], //  qui est le recepteur ?
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])) // quel est la clé ?
                    };
                });

            services.AddCors(option =>
            {
                string url = corsURLKEYS;
                option.AddPolicy("AllowSpecificOrigin",
                           builder => builder.WithOrigins(url).AllowCredentials());
                option.AddPolicy("PolicyFrontEnd", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "ApiSupMagasin",Version = "v1"});

           });
        }

    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });


            if (env.EnvironmentName == "Development")
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            app.UseCors();
            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api SupMagasin v1");
            });

            FirstImplementation();

            //si configuration sur vrai. Pour ne pas alerter à chaque test
            if (Configuration["Alert:Coworker"] == "true")
            {
                AlertCoworker();
            }
        }


        // Function pour creer la database si elle n'existe pas
        private static async void FirstImplementation()
        {
            //Creation de la collection Magasin
            DalShop dalMagasin = new DalShop();
            //Ajout d'un Magasin Test
            var d = dalMagasin.GetAllShop();
            var x =  BsonSerializer.Deserialize<List<Shop>>(d);
            if ( x.Count != 0) return ; // --> si data existe on ne recree pas la base

            await dalMagasin.AddMagasinAsync(new Shop()
            {
                ID = "M01",
                Enseigne = "Magasin Test",
                Adress = "26 rue des test",
                City = "TestVille",
                PostalCode = "73058",
                DateCreation = DateTime.Now,
                Email = "magasintest@test.com",    
                PhoneNum = "0908070441",
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
                Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Supinf0!"))),
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


        //Function pour prévenir en cas de nouveau déploiement
        private void AlertCoworker()
        {
            new Mailling().SendNewDeployment();
        }
    }
}
