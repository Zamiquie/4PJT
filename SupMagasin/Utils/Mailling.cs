using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SupMagasin.Model;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SupMagasin.Utils
{
    public class Mailling
    {
       
        public SmtpClient Smtp { get; }
        public IConfiguration _configuration { get; }

        public Mailling (IConfiguration configuration){
             _configuration = configuration;
           
            Smtp = new SmtpClient(_configuration["smtp:address"], 587)
            {
                UseDefaultCredentials = true,
                Credentials = new System.Net.NetworkCredential(_configuration["smtp:login"], _configuration["smtp:password"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
        }

        //fonction ayant pour but d'avertir  en cas de connexion sur l'index(si api exposé)
        public void SendAdvertissmentConnexionInd(HttpRequest request)
        {
            string contentHeader ="\n";
            foreach(var head in request.Headers)
            {
                contentHeader +=  head.Key + ":" + head.Value + "\n";
            }

            Smtp.Send(new MailMessage(_configuration["smtp:origin"], "ellinard77@gmail.com")
            {
                Subject = "[SupMagasinApi] Nouvelle connexion:" + DateTime.Now.ToString(),
                Body = "Nouvelle Connexion sur l'Api de SupMagasin. \n Client :" + contentHeader,
                IsBodyHtml = false
            }) ;
                
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


    }
}
