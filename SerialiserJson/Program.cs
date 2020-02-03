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
        }

        public class Customer
        {
           
            public int Id { get; set; }
    
            public Sexe Sexe { get; set; }
        
            public string Name { get; set; }
            
            public string FirstName { get; set; }
            
            public DateTime BirthdayDay { get; set; }
        
            public string Adress { get; set; }
      
            public string Postal_Code { get; set; }
       
            public string City { get; set; }
         
            public DateTime Last_Time { get; set; }
         
            public int AnnualFrequentation { get; set; }
         
            public float PanierMoyen { get; set; }
        
            public string RIB { get; set; }
         
            public DateTime TempsMoyen { get; set; }
          
            public byte[] Photo { get; set; }
         
            public string Email { get; set; }

        }
    }
}
