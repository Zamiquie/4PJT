using Microsoft.VisualBasic.CompilerServices;
using MongoDB.Bson.Serialization.IdGenerators;
using SupMagasin.Dal;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupMagasin.Utils
{
    //Generateur et decription du QrCode pour la generation des produits
    public class QrCodeHandler
    {
        

        //appelle de la methode publique pour la generation du qrCode
        public static string GenerateStringQrCode(Produit produit,string idMagasin)
        {
            string qrCodeString = Encrypte(produit,idMagasin);
            return qrCodeString;

        }
        //appelle de la methode pour le decryptage
        public static string  DecrypteStringQrCode(string qrString)
        {
            if (qrString == " " || qrString == null || qrString == "  ") throw new Exception("Qr String est null");
            return Decrypte(qrString);
        }


        /*
         * le resultat doit être = "[nombreAleatoire]"+$+data+$+data+n...+[nombreAleatoire]  
         */
        private static string Encrypte(Produit produit,string idMagasin)
        {
            //on recupera la chaine à chiffrer
            string encrypte = "$" + idMagasin + "$" + produit.Designation + "$" + produit.ID + "$" + produit.Lots.First(lot => lot.Stock != 0) ??"0" + "$";// ?? ternaire pour signaler qu'il n'y pas de lots
            // on instancie 2 variables pour mettre les nombre aléatoires;
            int m1 = 0;
            int m2 = 0;

            //on fait un Qr code de 707 characters
            if (encrypte.Length < 300)
            {
                //on calcule la dif avec le string obtenu avec la concet
                int diff = 300 - encrypte.Length;

                //on prend en compe la division par deux
                if (diff % 2 == 1)
                {
                    m1 = diff / 2 + 1;
                    m2 = diff / 2;
                }
                else
                {
                    m1 = diff / 2;
                    m2 = diff / 2;
                }
                //on ajoute les nombes à l'entrée du string
                for (int x = 0; x < m1; x++) { encrypte = new Random().Next(1, 9).ToString() + encrypte; }
                //on ajourte les nombres en fin du string
                for (int x = 0; x < m2; x++) { encrypte += new Random().Next(1, 9).ToString(); }

            }
            //on leve une exception si le message est trop long.
            else
            {
                throw new Exception("Message trop Long 300 caractère Max :( .\n votre data fait " + encrypte.Length + " Caractères");
            }

            return encrypte;
        }

        //on décrypte le qrCode et on renvois les données sous forme de dictionnaires
        private static string Decrypte(string qrData)
        {
            try
            {
                var data = qrData.Split('$');
                Dictionary<string, string> element = new Dictionary<string, string>();
                element.Add("idMagasin", data[1]);
                element.Add("NomProduit", data[2]);
                element.Add("IdProduit", data[3]);
                element.Add("NumeroLot", data[4]);

                DalProduit dal = new DalProduit();
                var product = dal.GetProduitByID(element["idMagasin"]).Result;
                return product;
            }
            //si données manquantes
            catch (IndexOutOfRangeException e)
            {
                var errorLecture = "Erreur 500 :"+e.Message;      
                return errorLecture;
            }
        }
    }
}