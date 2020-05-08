using System;
using System.Collections.Generic;
using System.Text;

namespace SupMagasin.Utils
{
    //Generateur et decription du QrCode pour la generation des produits
    class QRCodeHandler
    {
        public string IdMagasin { get; set; }
        public string NomProduit { get; set; }
        public string IdProduit { get; set; }
        public string NumeroLot { get; set; }
        public int Key { get; set; }



        public QRCodeHandler()
        {
            IdMagasin = "99";
            NomProduit = "ProduitTest";
            IdProduit = "73";
            NumeroLot = "145555954-1545";
            Key = 5;
        }

        /// Construction de l'Object QrHandleerpour la création et le déchifrement du string du QrCode 
        public QRCodeHandler(string idMagasin, string nomProduit, string idProduit, string numeroLot, int key = 5)
        {
            IdMagasin = idMagasin;
            NomProduit = nomProduit;
            IdProduit = idProduit;
            NumeroLot = numeroLot;
            Key = key;
        }

        //appelle de la methode publique pour la generation du qrCode
        public string GenerateStringQrCode()
        {
            string qrCodeString = Encrypte();

            return qrCodeString;

        }

        //appelle de la methode pour le decryptage
        public IDictionary<string, string> DecrypteStringQrCode(string qrString)
        {
            if (qrString == " " || qrString == null || qrString == "  ") throw new Exception("Qr String est null");
            return Decrypte(qrString);
        }


        /*
         * le resultat doit être = "[nombreAleatoire]"+$+data+$+data+n...+[nombreAleatoire]  
         */
        private string Encrypte()
        {
            //on recupera la chaine à chiffrer
            string encrypte = "$" + IdMagasin + "$" + NomProduit + "$" + IdProduit + "$" + NumeroLot + "$";
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
        private IDictionary<string, string> Decrypte(string qrData)
        {
            try
            {
                var data = qrData.Split('$');
                Dictionary<string, string> element = new Dictionary<string, string>();
                element.Add("idMagasin", data[1]);
                element.Add("NomProduit", data[2]);
                element.Add("IdProduit", data[3]);
                element.Add("NumeroLot", data[4]);

                return element;
            }
            //si données manquantes
            catch (IndexOutOfRangeException e)
            {
                var errorLecture = new Dictionary<string, string>();
                errorLecture.Add("ERROR", "Données Manquantes");
                return errorLecture;
            }
        }
    }
}