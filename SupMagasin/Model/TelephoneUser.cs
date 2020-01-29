using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class TelephoneUser
    {
        public int ID { get; set; }
        public string MAC { get; set; }
        public string Marque { get; set; }
        public Customer Owner {get; set;}
    }
}
