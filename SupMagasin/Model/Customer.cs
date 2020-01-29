using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Customer
    {
        public Guid Id { get; set; }
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
