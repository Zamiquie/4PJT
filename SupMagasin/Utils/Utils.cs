using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    //sexe customer
    public enum Sexe
    {
        Homme = 1,
        Femme = 2,
    }
    // etat des bornes blue
    public enum EtatBorne
    {
        Allume = 1, 
        Eteint = 2, 
        Casse = 3, 
        EnStock = 4

    }
    //game de produit
    public enum Gamme
    {
        Bas = 1, 
        Moyen = 2,
        Haut = 3
    }

    //catégorie produit
    public enum Categorie
    {
        Null = 0,
        Alimentaire = 1, 
        NewTech = 2, 
        Mobilier = 3
    }

    //connection string pour la connection Mongo 
    public static class ConnectionMongo {
        public static String Desktop { get { return "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false"; } }
        public static String Laptop { get { return "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false"; } }
        public static String ServeurProd {
            get {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("------------------ATTENTION--------------");
                Console.WriteLine("---------------POINTE SUR DB DE PROD-----");
                Console.WriteLine("--------------DANGER :s -----------------");
                Console.WriteLine("-----------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                return "mongodb://ApiSupMagasin:Mar1on_BenjaM1_Valent7_Mickael8_Aym6ric@zamiquiehost.ddns.net:27017/?authSource=SupMagasin&readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false"; } }

        public static String ServeurLocal { 
            get {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("------------------ATTENTION--------------");
                Console.WriteLine("---------POINTE SUR DB LOCAL DE TEST-----");
                Console.WriteLine("--------------DANGER :s -----------------");
                Console.WriteLine("-----------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                return "mongodb://ApiSupMagasin:Mar1on_BenjaM1_Valent7_Mickael8_Aym6ric@192.168.1.95:27017/?authSource=SupMagasin&readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false"; 
            } 
        }
        
    }

    public enum Sanction
    {
        Avertissement = 1,
        Blame = 2,
        MiseaPied = 3,
        FauteGrave = 4,
        FauteLourde = 5
    }

    public enum TypeLog
    {
        MangoDb = 1,
        AspNet = 2,
        AuthenError =3,
        AuthenSuccess= 4,
        Other = 5,
        Bluethoo = 6
        
    }
}
