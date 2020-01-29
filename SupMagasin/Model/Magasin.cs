using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Magasin
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Postal_Code { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public Employe IdResponsable { get; set; }
        public int NbRayon { get; set; }
        public DateTime DateCreation { get; set; }


    }
}
