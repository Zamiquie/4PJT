using Newtonsoft.Json;
using SupMagasin.Model;
using System;

namespace SerialiserJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer obj = new Customer();
            Console.WriteLine(JsonConvert.SerializeObject(obj));
            Console.ReadLine();
        }

       
    }
}
