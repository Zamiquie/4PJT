using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class BorneTelephone
    {
        public Borne Borne { get; set; }
        public TelephoneUser TelephoneUser {get; set;}
        public DateTime LastConnection { get; set; }
    }
}
