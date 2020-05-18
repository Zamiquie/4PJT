
using SupMagasin.Utils;
using System;

namespace TestMail
{
    class Program
    {
        static void Main(string[] args)
        {
            Mailling mailling = new Mailling();
            mailling.SendWithHtml("aymericbaquet@gmail.com");
         
        }
    }
}
