using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SupMagasin.Model;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

            Smtp.Send(new MailMessage(_configuration["smtp:origin"], "ellinard77@gmail.com")
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


        //mail pour prévenir l'équipe d'un nouveau deploiement
        public void SendNewDeployment()
        {
            string[] mailEquipe = { "224563@supinfo.com","282555@supinfo.com", "292107@supinfo.com", "293204@supinfo.com","214637@supinfo.com" };

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
    }
}
