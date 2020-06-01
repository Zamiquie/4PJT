using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SupMagasin.Model;
using SupMagasin.Model.ModelService;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SupMagasin.Utils
{
    public class Mailling
    {

        public SmtpClient Smtp { get; }
        public IConfiguration _configuration { get; }

        public Mailling(IConfiguration configuration)
        {
            _configuration = configuration;

            Smtp = new SmtpClient(_configuration["smtp:address"], 587)
            {
                UseDefaultCredentials = true,
                Credentials = new System.Net.NetworkCredential(_configuration["smtp:login"], _configuration["smtp:password"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
        }

        //Constructeur par default pour test
        public Mailling()
        {
            Console.WriteLine("Attention Constructeur mail par default. A ne pas utiliser en prod");
            Smtp = new SmtpClient("ssl0.ovh.net", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new System.Net.NetworkCredential("contact_supmagasin@zamiquie.ovh", "SupMagasin7791"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
        }

        //fonction ayant pour but d'avertir  en cas de connexion sur l'index(si api exposé)
        public void SendAdvertissmentConnexionInd(HttpRequest request)
        {
            string contentHeader = "\n";
            foreach (var head in request.Headers)
            {
                contentHeader += head.Key + ":" + head.Value + "\n";
            }

            Smtp.Send(new MailMessage(_configuration["smtp:origin"], "aymericbaquet@gmail.com")
            {
                Subject = "[SupMagasinApi] Nouvelle connexion:" + DateTime.Now.ToString(),
                Body = "Nouvelle Connexion sur l'Api de SupMagasin. \n Client :" + contentHeader,
                IsBodyHtml = false
            });

        }
        //mail pour les nouveaux clients
        public void MailToNewCustomer(Customer customer)
        {
            Smtp.Send(new MailMessage(_configuration["smtp:origin"], customer.Email)
            {
                Subject = "[Bienvenue] " + customer.FirstName + " sur SupMagasin",
                Body = "Bienvenue, \n" +
                " Nouveau client. Venez depenser votre fric chez nous afin de lutter contre le coronavirus.\n" +
                "Et sachez que pour un euro dépensé, un chaton est sauvé du virus alors dépensé sans compter \n" +
                "" +
                "PS: si vous aller chez carrefour, on vous casse les jambes \n" +
                " La Direction de SupMag",
                IsBodyHtml = false
            });


        }

        public void SendWithHtml(string destinatary)
        {
            var template = "";

            using (var streamReader = new StreamReader(new FileStream(Directory.GetCurrentDirectory() + @"/../../../../SupMagasin/Asset/EmailTemplate/welcome/welc.html", FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                template = streamReader.ReadToEnd();
            }

            template = template.Replace("{{Name}}", destinatary);
            Smtp.Send(new MailMessage("contact@test.de", destinatary)
            {
                Subject = "[Bienvenue] sur le SupMagasin",
                Body = template,
                IsBodyHtml = true
            });

        }

        public void SendAlertWithDocCSV(List<StockAlert> AlertesProduit)
        {


            var file = Directory.GetCurrentDirectory()+"/temps/temp_rupture.csv";
            //creation du document
            using (var streamRide = new StreamWriter(new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite), Encoding.UTF8))
            {
                streamRide.WriteLine("{0};{1};{2};{3};{4};{5}", "date", "idProduit", "Designation", "StockAlert", "StockRestant", "Differenciel");

                int total = 0;
                foreach (StockAlert stock in AlertesProduit)
                {
                    if (stock.isRupture)
                    {
                        total++;
                        streamRide.WriteLine("{0};{1};{2};{3};{4};{5}", DateTime.Now, stock._id, stock.Designation, stock.StAlert, stock.TotalStock, stock.TotalStock - stock.StAlert);
                    }
                }
                streamRide.WriteLine("{0};{1}", "Total Produit:", total);
                streamRide.Flush();
                streamRide.Close();
            }

            //Creation de la PJ du mail 
            Attachment csvRupture = new Attachment(file, MediaTypeNames.Application.Octet);

            //creation du mail 
            MailMessage alerte = new MailMessage(/*_configuration["smtp:origin"]*/"testServiceAlert@test.de", "282555@supinfo.com")
            {
                Subject = "Alerte Quotidienne Approvisionnement " + DateTime.Now.ToShortTimeString(),
                Body = "Alerte Automatisé des réapovisionnement a effectuer",
                IsBodyHtml = false,
            };
            alerte.Attachments.Add(csvRupture);

            //Envoie du mail
            Smtp.Send(alerte);
            csvRupture.Dispose();
            File.Delete(file);

        }

        //mail pour prévenir l'équipe d'un nouveau deploiement
        public void SendNewDeployment()
        {
            string[] mailEquipe = { "224563@supinfo.com", "282555@supinfo.com", "292107@supinfo.com", "293204@supinfo.com", "214637@supinfo.com" };

            foreach (string courriel in mailEquipe)
            {
                Smtp.Send(new MailMessage("alertApi@supmagasin.com", courriel)
                {

                    Subject = "[API SupMagasin] nouveau deploiment",
                    Body = "Un nouveau deploiement de l'api supMagasin à été faite à " + DateTime.Now.ToString() +
                "\n\n  L'équipe SupMagasin \n We are the best (on va niquer le game, et .....)"

                });
            }
        }

        //mail pour l'envois des factures lors de la vente
        public void sendFactureByMail(Sale sale,Customer customer)
        {
            string pathPj = Directory.GetCurrentDirectory() + "/Asset/Factures/" + sale.IdShop + "/" + "f" + sale.ID + ".pdf";
            //Création de la PJ
            Attachment facturePj = new Attachment(pathPj, MediaTypeNames.Application.Octet);

            //création du mail 
            MailMessage newMail = new MailMessage("supmag@supmagasin.com", customer.Email)
            {
                Subject = "[SupMagasin] facture n°f" + sale.ID + " du " + sale.VenteDate,
                Body = "Bonjour " + customer.FirstName + " " + customer.Name + ","
                + "\n veuillez trouver-joint à ce mail  la facture de vos achats dans notre magasin."+
                " \n en esperant vous revoir très bientôt"+
                "\n Cordialement"
                +"\n l'équipe SupMagasin"+
                "Ps: On rembourse pas",
            };
            newMail.Attachments.Add(facturePj);

            Smtp.Send(newMail);
  
        }
    }
}
