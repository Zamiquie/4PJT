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
        Alimentaire = 1, 
        NewTech = 2, 
        Mobilier = 3
    }

    //connection string pour la connection Mongo 
    public static class ConnectionMongo {
        public static String Desktop { get { return "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false"; } }
        public static String Laptop { get { return "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false"; } }
        public static String Serveur { get { return "nothing"; } }
        
    }
}
