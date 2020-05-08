using Newtonsoft.Json;
using SupMagasin.Model;
using System;

namespace SerialiserJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Produit obj = new Produit();
            Console.WriteLine(JsonConvert.SerializeObject(obj));
            Console.ReadLine();
        }

       
    }
}
