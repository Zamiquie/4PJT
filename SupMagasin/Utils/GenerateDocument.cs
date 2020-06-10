using Microsoft.AspNetCore.Routing.Template;
using SupMagasin.Dal;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SupMagasin.Utils
{
    public class GenerateDocument
    {
        public static string Path = Directory.GetCurrentDirectory() + @"/Asset/DocumentTemplate/BillTemplate.html"; 


        public static async Task GenerateFacture(Sale sale,Customer customer,Shop shop)
        {
            //template de la facture
            var template = "";
            using (var streamReader = new StreamReader(new FileStream(Path,FileMode.Open,FileAccess.Read),Encoding.UTF8))
            {
                template = streamReader.ReadToEnd(); 
            }

            /*Changement des balistes*/

            //Changement Entete Facture
            //(Magasin information)
            template = template.Replace("[[nom_shop]]", shop.Enseigne);
            template = template.Replace("[[adress_shop]]", shop.Adress);
            template = template.Replace("[[postal_code]]", shop.PostalCode);
            template = template.Replace("[[city_shop]]", shop.City);

            //(Client Infomation)
            template = template.Replace("[[nom,prenom]]", customer.FirstName + " " + customer.Name);
            template = template.Replace("[[address_customer]]",customer.Adress);
            template = template.Replace("[[postal_code_customer]]", customer.Postal_Code);
            template = template.Replace("[[city_customer]]",customer.City);
            template = template.Replace("[[customer_email]]", customer.Email);

            //(jour et id de la date)
            template = template.Replace("[[id_facture]]", sale.ID);
            template = template.Replace("[[date_facture]]", sale.SaleDate.ToString(""));

            //Detail Facture
            DalProduit dal = new DalProduit();
            foreach (SaleProduct produit in sale.ProduitsSales)
            {
               var currendProd = await dal.GetProduitByID(produit.IdProduct);
                template = template.Replace("[[detail_produit]]",
                    "<tr>" +
                        "<td>" +produit.IdProduct+"</td>" +
                        "<td>" +currendProd.Designation +"</td>" +
                        "<td>" +produit.Quantity+ "</td>" +
                        "<td>" +produit.UnitPrice +"</td>" +
                        "<td>" +produit.AmountPromo +"</td>" +
                        "<td>" +produit.Quantity*(produit.UnitPrice-produit.AmountPromo)+"</td>" +
                    "</tr>" +
                    "[[detail_produit]]"
                    );
            }
            template = template.Replace("[[detail_produit]]", "");

            template = template.Replace("[[produit_totaux]]", sale.ProduitsSales.Sum(pr => pr.Quantity).ToString());
            template = template.Replace("[[total_price]]", sale.TotalAmount.ToString());


            //Conversion Html to Pdf
            var Render = new IronPdf.HtmlToPdf();

            var Pdf = Render.RenderHtmlAsPdf(template);
            //on creer le repertoire si il n'existe pas 
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Asset/Factures/" + shop.ID))
             {
                 Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Asset/Factures/" + shop.ID);
             }

            string Outpath = Directory.GetCurrentDirectory() + "/Asset/Factures/" + shop.ID+"/"+"f"+sale.ID+".pdf";
            Pdf.SaveAs(Outpath);
        }
    }
}
